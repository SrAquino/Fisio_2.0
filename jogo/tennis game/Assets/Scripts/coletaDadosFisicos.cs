using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class coletaDadosFisicos : MonoBehaviour
{
    //GameObjects para a coleta de dados físicos    
    public GameObject pressaoArterial;
    public GameObject batimentosCardiacos;
    public GameObject oxigenacaoSangue;
    public GameObject observacoes;

    //Flag para saber qual coleta de dados é: antes ou após partida
    public static int opcao;

    //Group para capturar se irá ou não coletar os dados físicos do paciente
    public ToggleGroup escolheColeta;

    //Utilizado para controlar se foi ou não inserido dados físicos 
    int dadosFNecessario = 0;
    
    //GameObject responsável pelo aparecimento da entrada dos dados físicos do paciente
    public GameObject painelEntrada;

    void Start(){
        painelEntrada.SetActive(false);
    }

    void Update(){
        if(escolheColeta.AnyTogglesOn() == true){
            Toggle coletar = escolheColeta.ActiveToggles().FirstOrDefault();
            string opcaoColeta = coletar.GetComponentInChildren<Text>().text;
            // Debug.Log("Opção marcada é: " + opcaoColeta);
            if(opcaoColeta == "SIM"){ //mostra painel de entrada dos dados
                painelEntrada.SetActive(true);
                dadosFNecessario = 1;
            }else if(opcaoColeta == "NÃO"){ //registra como não necessário os dados físicos
                painelEntrada.SetActive(false);
                dadosFNecessario = 0;
            }
        }
        // Debug.Log(escolheColeta.AnyTogglesOn());
    }

    //Função para salvar os dados físicos do paciente
    public void SalvarDadosFisicos(){
        if(opcao == 1 && dadosFNecessario == 1){ //Antes da partida
            string escrita;
            escrita = "#\nA\n" + pressaoArterial.GetComponent<Text>().text + "\n" + batimentosCardiacos.GetComponent<Text>().text + "\n" + oxigenacaoSangue.GetComponent<Text>().text + "\n" + observacoes.GetComponent<Text>().text + "\n ##";
            // dadosJogo.pressaoArterialAntes = pressaoArterial.GetComponent<Text>().text;
            // dadosJogo.batimentosCardiacosAntes = batimentosCardiacos.GetComponent<Text>().text;
            // dadosJogo.oxigenacaoSangueAntes = oxigenacaoSangue.GetComponent<Text>().text;
            // dadosJogo.observacoesAntes = observacoes.GetComponent<Text>().text;
            dadosJogo.Salvar(escrita);
        }else if(opcao == 1 && dadosFNecessario == 0){
            string escrita;
            escrita = "#\nA\n0\n0\n0\n0\n##";
            dadosJogo.Salvar(escrita);
        }else if(opcao == 2 && dadosFNecessario == 1){ //Após a partida
            // dadosJogo.pressaoArterialDepois = pressaoArterial.GetComponent<Text>().text;
            // dadosJogo.batimentosCardiacosDepois = batimentosCardiacos.GetComponent<Text>().text;
            // dadosJogo.oxigenacaoSangueDepois = oxigenacaoSangue.GetComponent<Text>().text;
            // dadosJogo.observacoesDepois = observacoes.GetComponent<Text>().text;
            // dadosJogo.CreateText();
            string escrita;
            escrita = "###\nD\n" + pressaoArterial.GetComponent<Text>().text + "\n" + batimentosCardiacos.GetComponent<Text>().text + "\n" + oxigenacaoSangue.GetComponent<Text>().text + "\n" + observacoes.GetComponent<Text>().text + "\n ####";
            dadosJogo.Salvar(escrita);
        }else if(opcao == 2 && dadosFNecessario == 0){
            string escrita;
            escrita = "###\nD\n0\n0\n0\n0\n####";
            // escrita = "Dados físicos depois da partida: \n Não necessários";
            dadosJogo.Salvar(escrita);
        }
    }

}
