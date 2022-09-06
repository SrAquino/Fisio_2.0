using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
// using System.IO;
// using System;

public class Player : MonoBehaviour
{
    public Transform aimTarget; //alvo para onde a bolinha será lançada para o lado do bot
    
    float speed = 15f; //velocidade da raquete que será multiplicada pela posição - 15 - 0.125f - 0.5f
    float force = 30; //15
    bool hitting;

    public Transform ball;
    //public Rigidbody playerRb;

    Vector3 aimTargetPosition;

    //posição aleatoria de rebate da bolinha 
    public Transform[] targets;

    //Comunicação serial
    // SerialPort porta;

    public static bool flagDificuldade;

    public static int numAcertos;

    float deslocamentoAnterior = -1.0f;

   
    void Start()
    {
        //playerRb = this.GetComponent<Rigidbody>();
        aimTargetPosition = aimTarget.position;
        // porta = new SerialPort("/dev/ttyACM0", 115200);
        // porta.Open();
        // porta.ReadTimeout = -1; //InfiniteTimeout = -1
        // porta.DiscardInBuffer();
        transform.position = new Vector3(43.04f, -12.91f, 0.0f);
        numAcertos = -1;
    }

    // void FixedUpdate()
    void Update()
    {
        if(configCalibragem.porta.IsOpen){
            try{
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

    //função para mover o avatar usando como entrada de dados o teclado
    void movimentaUsandoTeclado(){
        
        //capturar posição se pra frente, pra trás, pra direita ou pra esquerda
        float h = Input.GetAxisRaw("Horizontal"); //direita = 1 esquerda = -1
        float v = Input.GetAxisRaw("Vertical");

        if(h != 0 || v != 0){ //movimenta avatar
            //time.deltatime evita alta taxa de atualização de quadros
            // transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //se não colocar uma velocidade o movimento do player é muito rapidamente
            // playerRb.velocity = new Vector3(0, 0, h) * speed;
            // Debug.Log(Time.deltaTime);
            if(h == 1f){ //direita
                // Debug.Log("Posição Atual D = ");
                // Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime); //Time.deltatime = fazer o movimento c/ velocidade constante - retorna 0.02
                // Debug.Log("Posição Depois D = ");
                // Debug.Log(transform.position.z);
            }else{ //esquerda
                // Debug.Log("Posição Atual E = ");
                // Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                // Debug.Log("Posição Depois E = ");
                // Debug.Log(transform.position.z);
            }
        }
    }

    //função para retornar aleatoriamente uma posição p/ rebate da bolinha pelo player - 3 opções por enquanto
    Vector3 PickTarget(){
        int randomValue =  Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    //função para tratar colisões com a raquete -> bola + raquete 
    private void OnCollisionEnter(Collision other){
    //private void OnTriggerEnter(Collider other) {
        //verifica se a colisão foi com a bola
        if(other.gameObject.CompareTag("Ball")){
            Vector3 dir;
            //movimentando target usando teclado
            if(flagDificuldade == false){
                dir = aimTarget.position - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            }else{
                //movimentando target usando aleatoriedade
                dir = PickTarget() - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            }
            other.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 8f, 0); //bolinha - 6

            Vector3 ballDir = ball.position - transform.position;
            aimTarget.position = aimTargetPosition;

            ball.GetComponent<Ball>().hitter = "player"; //modificando uma var. publica da class ball
            ball.GetComponent<Ball>().playing = true;
            numAcertos++;
            // Ball.numRebates++;
        }
    }

    
    //seta posição quando erra jogada
    // public void Reset(){
    //     if(servedRight){
    //         transform.position = serveLeft.position;
    //     }else{
    //         transform.position = serveRight.position;
    //     }

    //     servedRight = !servedRight;

    // }

}
