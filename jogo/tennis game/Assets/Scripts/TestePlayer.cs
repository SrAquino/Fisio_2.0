using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.Threading;
using System.Globalization;
//Código de teste de movimntação do avatar

public class TestePlayer : MonoBehaviour
{
    float speed = 20.0f; //velocidade da raquete que será multiplicada pela posição

    public GameObject entradaTxt; //entrada do deslocamento
    public GameObject entradaDir;
    void Start()
    {
        transform.position = new Vector3(43.04f, -12.91f, 0.0f);
    }

    void FixedUpdate()
    {
        // transform.position = Vector3.Lerp(transform.position, new Vector3(43.04f, -12.91f, 7.5f), Time.deltaTime);
        while(transform.position.z <= 7.5f){
            transform.position = Vector3.Lerp(transform.position, new Vector3(43.04f, -12.91f, 7.5f), Time.deltaTime);
        }
    }

    //função para mover o avatar usando como entrada de dados o teclado
    void movimentaUsandoTeclado(){

        
        //capturar posição se pra frente, pra trás, pra direita ou pra esquerda
        float h = Input.GetAxisRaw("Horizontal"); //direita = 1 esquerda = -1
        float v = Input.GetAxisRaw("Vertical");

        if(h != 0 || v != 0){ //movimenta avatar
            if(h == 1f){ //direita
                Debug.Log("Posição Atual D = ");
                Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime); //Time.deltatime = fazer o movimento c/ velocidade constante - retorna 0.02
                Debug.Log("Posição Depois D = ");
                Debug.Log(transform.position.z);
            }else{ //esquerda
                Debug.Log("Posição Atual E = ");
                Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                Debug.Log("Posição Depois E = ");
                Debug.Log(transform.position.z);
            }
        }
    }

    public void capturaProcessaEntrada(){
        string entradaPosicao = entradaTxt.GetComponent<Text>().text;
        float posicao = float.Parse(entradaPosicao, CultureInfo.InvariantCulture);
        string direcao = entradaDir.GetComponent<Text>().text;
        // Debug.Log("Deslocamento é igual a: " + posicao + "para a: " + direcao);

        float z = transform.position.z;
        // Debug.Log("Posição atual do avatar em z é igual a: " + z);

        float deslocamento = 0.0f;

        //gerar posição negativa quando a direção é para a esquerda
        //pq o app flutter envia os valores em módulo, sempre positivo
        if(direcao == "E"){
            posicao = posicao * (-1.0f);
        }


        if(z == 0 || (z > -1 && z < 1)){
            deslocamento = z + posicao;
        }else if(posicao > z){
            deslocamento = posicao - z;
        }else if(z > posicao){
            deslocamento = z - posicao;
        }

        // Debug.Log("Nova posição igual a: " + deslocamento);

        //Faz o módulo do deslocamento
        if(deslocamento < 0){
            deslocamento = deslocamento * (-1.0f);
        }

        float andandoMesa = 0.0f;

        if(direcao == "D"){
            while(andandoMesa <= deslocamento){
                transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime);
                Debug.Log(1 * speed * Time.deltaTime);
                andandoMesa += (1 * speed * Time.deltaTime); 
            }
        }else if(direcao == "E"){
            while(andandoMesa <= deslocamento){
                transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                Debug.Log(-1 * speed * Time.deltaTime);
                andandoMesa += (1 * speed * Time.deltaTime);
            }
        }else if(direcao == "P"){
            transform.position += new Vector3(0, 0, 0);
        }
    }

}
