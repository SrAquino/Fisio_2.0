using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.Globalization;
using System.IO;
using System;

public class configCalibragem : MonoBehaviour
{
    //Cria variável para a Comunicação serial
    public static SerialPort porta;

    //GameObject que contém o texto de saída se direita ou esquerda
    public GameObject txtInstrucao;
    //GameObject que contém o txt para indicar posição
    public GameObject txtMover;
    //GameObject com o ícone indicativo para onde mover o sensor
    public GameObject iconeIndicativoEsquerda;
    //GameObject com o ícone indicativo para onde mover o sensor
    public GameObject iconeIndicativoDireita;
    //Variável para controlar para onde será feita a calibragem
    public static int posicaoCalibragem;
    //GameObject com o btn das inicializacoes das calibragens
    public GameObject botaoInit;
    //GameObject com controle das saidas de quando for concluida as calibragens
    public GameObject txtSaidaDireita;
    public GameObject txtSaidaEsquerda;
    //GameObject com o btn ao final da calibragem para ir para a configuração da partida
    public GameObject botaoSeguir;

    //Variáveis para animar indicador de calibragem
    int indicaCalib = 0;

    void Start()
    {
        string [] portasDisponiveis = SerialPort.GetPortNames(); //coleta porta serial disponivel
        Debug.Log(portasDisponiveis[1]); //seleciona a primeira que é a que está conectado c/ comun serial c/ arduino
        //Cria porta e abre 
        //porta = new SerialPort("/dev/ttyACM1", 115200);
        porta = new SerialPort(portasDisponiveis[1], 115200);
        Debug.Log("Abrir porta..");
        porta.Open();
        Debug.Log("Porta aberta!");


        porta.ReadTimeout = -1; //InfiniteTimeout = -1
        posicaoCalibragem = -1; //inicializa flag para detectar extremo de calibragem
    }

    void Update()
    {
        if(indicaCalib == 1){
            Debug.Log("Posição x: " + iconeIndicativoDireita.transform.position.x );
            Debug.Log("Posição y: " + iconeIndicativoDireita.transform.position.y );
            Debug.Log("Posição z: " + iconeIndicativoDireita.transform.position.z );
            if(iconeIndicativoDireita.transform.position.x >= 65f){
                iconeIndicativoDireita.transform.position = new Vector3(65f, -21.85f, 90f);
            }else{
                iconeIndicativoDireita.transform.position += new Vector3(1 * 15f * Time.deltaTime, 0, 0);
            }
        }else if(indicaCalib == 2){
            if(iconeIndicativoEsquerda.transform.position.x <= -65f){
                iconeIndicativoEsquerda.transform.position = new Vector3(-65f, -21.85f, 90f);
            }else{
                iconeIndicativoEsquerda.transform.position += new Vector3(-1 * 15f * Time.deltaTime, 0, 0);
            }
        }
        
        if(porta.IsOpen){
            try{
                //Verifica se está sendo recebido dados 
                //Debug.Log(porta.BytesToRead);
                if(porta.BytesToRead == 0){ //Não está sendo recebido dados no buffer
                    //Debug.Log(porta.BytesToRead);
                    //  Debug.Log("Sem recebimento de dados A!");
                }else{ //Iniciou o recebimento de dados no buffer
                    // Debug.Log("Entrada recebida!");
                    int lePorta = porta.ReadByte();
                    porta.DiscardInBuffer();
                    //Debug.Log(lePorta);
                    if(lePorta == 54){ //calibragem sera iniciada 
                        if(posicaoCalibragem == 0){ //calibragem para a direita
                            indicaCalib = 1;
                            // Debug.Log("Calibragem para a direita INICIADA!");
                            botaoInit.SetActive(false);
                            txtMover.SetActive(true);
                            iconeIndicativoDireita.SetActive(true);
                            iconeIndicativoEsquerda.SetActive(false);
                        }else if(posicaoCalibragem == 1){ //calibragem para a esquerda
                            // Debug.Log("Calibragem para a esquerda INICIADA!");
                            indicaCalib = 2;
                            botaoInit.SetActive(false);
                            txtMover.SetActive(true);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(true);
                        }
                        
                    }else if(lePorta == 55){ //calibragem concluida 
                        if(posicaoCalibragem == 0){
                            //renderizar elementos p/ iniciar a calibragem para a esquerda
                            // Debug.Log("Calibragem para a direita CONCLUIDA!");
                            txtSaidaDireita.GetComponent<Text>().text = "CALIBRAGEM PARA A DIREITA  ✓ ";
                            txtSaidaEsquerda.GetComponent<Text>().text = "CALIBRAGEM PARA A ESQUERDA  ✘ ";
                            txtInstrucao.GetComponent<Text>().text = "PARA A ESQUERDA";
                            txtMover.GetComponent<Text>().text = "REALIZE O MOVIMENTO DO SENSOR PARA A ESQUERDA";
                            botaoInit.SetActive(true);
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
                        }else{
                            txtSaidaEsquerda.GetComponent<Text>().text = "CALIBRAGEM PARA A ESQUERDA  ✓ ";
                            // Debug.Log("Calibragem para a esquerda CONCLUIDA!");
                            txtInstrucao.GetComponent<Text>().text = "CALIBRAGEM CONCLUÍDA";
                            botaoSeguir.SetActive(true);
                            botaoInit.SetActive(false);
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
                        }
                    }else if(lePorta == 65){//movimento válido
                        if(posicaoCalibragem == 0){ //calibragem para a direita
                            // indicaCalib = 1;
                            // Debug.Log("Calibragem para a direita INICIADA!");
                            botaoInit.SetActive(false);
                            txtMover.GetComponent<Text>().text = "AGUARDE...";
                            iconeIndicativoDireita.SetActive(true);
                            iconeIndicativoEsquerda.SetActive(false);
                        }else if(posicaoCalibragem == 1){ //calibragem para a esquerda
                            // Debug.Log("Calibragem para a esquerda INICIADA!");
                            // indicaCalib = 2;
                            botaoInit.SetActive(false);
                            txtMover.GetComponent<Text>().text = "AGUARDE...";
                            // txtMover.SetActive(true);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(true);
                        }
                    }else if(lePorta == 57){ //repetir movimento
                        if(posicaoCalibragem == 0){ //calibragem para a direita
                            indicaCalib = 1;
                            // Debug.Log("Calibragem para a direita INICIADA!");
                            txtInstrucao.GetComponent<Text>().text = "REFAÇA A CALIBRAGEM - DIREITA";
                            botaoInit.SetActive(true);
                            // txtMover.GetComponent<Text>().text = "REFAÇA A CALIBRAGEM";
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
                            posicaoCalibragem = -1;
                            iconeIndicativoDireita.transform.position = new Vector3(0f, -21.85f, 90f);
                        }else if(posicaoCalibragem == 1){ //calibragem para a esquerda
                            // Debug.Log("Calibragem para a esquerda INICIADA!");
                            indicaCalib = 2;
                            txtInstrucao.GetComponent<Text>().text = "REFAÇA A CALIBRAGEM - ESQUERDA";
                            botaoInit.SetActive(true);
                            // txtMover.GetComponent<Text>().text = "REFAÇA A CALIBRAGEM";
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
                            posicaoCalibragem = 0;
                            iconeIndicativoEsquerda.transform.position = new Vector3(0f, -21.85f, 90f);
                        }
                    }

                    //Teste de recebimento de informações
                    // if(moveVerdade == 1){
                    //     string dadoNoSensor = porta.ReadTo("\n");
                    //     porta.DiscardInBuffer();

                    //     porta.Write("2");
                    //     porta.DiscardOutBuffer();

                    //     Debug.Log("Valor recebido: " + dadoNoSensor);
                    // }

                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void TesteComunicacao(int opcao){
        if(porta.IsOpen){
            try{
                if(opcao == 0){
                    porta.Write("0");
                }else if(opcao == 1){
                    porta.Write("1");
                }else if(opcao == 2){
                    porta.Write("2");
                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void Calibragem(){
        posicaoCalibragem += 1;
        if(porta.IsOpen){
            try{
                if(posicaoCalibragem == 0){
                    //direita
                    porta.Write("0");
                    // Debug.Log("Calibragem direita");
                }else if(posicaoCalibragem == 1){
                    //esquerda
                    porta.Write("1");
                    // Debug.Log("Calibragem esquerda");
                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void TesteMovimento(){
        if(porta.IsOpen){
            try{
                porta.Write("2");
            }catch(System.Exception){
                throw;
            }
        }
    }

}
