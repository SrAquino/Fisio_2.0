using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

public class PlayerDemo : MonoBehaviour
{
    float speed = 15f; //velocidade da raquete que será multiplicada pela posição - 15 - 0.125f - 0.5f
    float deslocamentoAnterior = -1.0f;

    //Doxygen - usa a forma de comentário do Java 
    
    /**
    * @param 
    **/

    int start = 0;
    
    void Start()
    {
        transform.position = new Vector3(43.04f, -12.91f, 0.0f);
        start = 1;
        if(configCalibragem.porta.IsOpen){
            try{
                configCalibragem.porta.Write("2");
            }catch(System.Exception){
                throw;
            }
        }
            
    }

    // void FixedUpdate()
    void Update()
    {
        if(configCalibragem.porta.IsOpen){
            try{
                // //envia pedido de dados de movimento na primeira vez depois é o controle do jogo que emite
                // if(start == 1){
                //     configCalibragem.porta.Write("2");
                //     start = 0;
                // }

                 if(configCalibragem.porta.BytesToRead == 0){ //Não está sendo recebido dados no buffer
                     //Debug.Log(porta.BytesToRead);
                    //   Debug.Log("Sem recebimento de dados B!");
                 }else{
                    //Trecho comentado que funciona apenas quando enviado APENAS UM BYTE
                    // int dadoNoSensor = configCalibragem.porta.ReadByte();
                    // configCalibragem.porta.DiscardInBuffer();
                    // configCalibragem.porta.Write("2");
                    //Trecho de código para coletar o que é recebido pela Unity quando é solicitado valores de movimento
                    string dadoNoSensor = configCalibragem.porta.ReadTo("\n");
                    configCalibragem.porta.DiscardInBuffer();

                    configCalibragem.porta.Write("2");
                    configCalibragem.porta.DiscardOutBuffer();

                    // Debug.Log("Valor recebido: " + dadoNoSensor);
                    // Debug.Log("Tamanho: " + dadoNoSensor.Length);

                    //verifica se não houve um erro de leitura do buffer e leu vazio
                    if(dadoNoSensor != "" && (dadoNoSensor.Length == 4 || dadoNoSensor.Length == 7)){
                        //realiza a separação dos valores de direção e posição recebidos do nó sensor
                        string[] separandoDados = dadoNoSensor.Split(';');

                        string direcao = separandoDados[0]; 
                        float posicao = 0f;
                        
                        //lê dados de posição recebidos do nó sensor - direita e esquerda apenas 
                        if(direcao == "E" || direcao == "D"){
                            posicao = float.Parse(separandoDados[1], CultureInfo.InvariantCulture);
                            
                            //gerar posição negativa quando a direção é para a esquerda
                            //pq o app flutter envia os valores em módulo, sempre positivo
                            if(direcao == "E"){
                                posicao = posicao * (-1.0f);
                            }
                        }
                        
                        // moveAvatar(direcao, posicao);

                        float z = transform.position.z;
                        // Debug.Log("Posição atual do avatar em z é igual a: " + z);
                        
                        if(deslocamentoAnterior == posicao){
                            // Debug.Log("Valor repetido recebido!");
                        }else{

                            float deslocamento = 0.0f;

                            if(z == 0 || (z > -1 && z < 1)){
                                deslocamento = z + posicao;
                            }else if(posicao > z){
                                deslocamento = posicao - z;
                            }else if(z > posicao){
                                deslocamento = z - posicao;
                            }
                            
                            deslocamentoAnterior = deslocamento;

                            // Debug.Log("Nova posição igual a: " + deslocamento);

                            //Faz o módulo do deslocamento para realizar corretamente a movimentação no eixo z
                            if(deslocamento < 0){
                                deslocamento = deslocamento * (-1.0f);
                            }

                            float andandoMesa = 0.0f;

                            if(direcao == "D"){
                                while(andandoMesa < deslocamento && transform.position.z <= 8f){
                                    // Debug.Log(1 * speed * Time.deltaTime);
                                    transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime);
                                    andandoMesa += (1 * speed * Time.deltaTime); 
                                }
                            }else if(direcao == "E"){
                                while(andandoMesa < deslocamento && transform.position.z >= (-8f)){
                                    // Debug.Log(-1 * speed * Time.deltaTime);
                                    transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                                    andandoMesa += (1 * speed * Time.deltaTime); 
                                }
                            }else if(direcao == "P"){
                                // transform.position += new Vector3(0, 0, 0);
                            }
                        }

                    }
                
                //     //Trecho de código que funciona apenas com UM BYTE - somente D, E, P sem valores de deslocamento

                //      // if(dadoNoSensor == 68 || dadoNoSensor == 69 || dadoNoSensor == 80){ //se for dados de movimento
                //     //  Debug.Log("Recebimento de dados de MOVIMENTAÇÃO!");
                //     //  configCalibragem.porta.Write("2");
                //     //  //     //moveAvatar(dadoNoSensor);
                //     //      if(dadoNoSensor == 68){ //direita
                //     //          Debug.Log("Direita");
                //     //          transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime);
                //     //      }else if(dadoNoSensor == 69){ //esquerda
                //     //          Debug.Log("Esquerda");
                //     //          transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                //     //      }else if(dadoNoSensor == 80){ //parado
                //     //          Debug.Log("Parado");
                //     //          transform.position += new Vector3(0, 0, 0 * speed * Time.deltaTime);
                //     //      }
                //     //  // }
                 }
             }catch(System.Exception){
                 throw;
             }
         }    
        // movimentaUsandoTeclado();
    }

}
