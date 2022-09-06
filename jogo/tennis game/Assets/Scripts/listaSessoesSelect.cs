using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class listaSessoesSelect : MonoBehaviour
{
    //referência com a configuração pronta
    public GameObject itemTemplate;
    //Local para onde deve ser adicionado o item
    public GameObject content;

    // //Escrever nome do paciente na tela 
    // public GameObject paciente;

    //referência para a configuração do select das sessões
    public GameObject itemSessao;
    //Lista para guardar a referencia de todas as sessões criadas via toggle
    public static List<Toggle> toggleSessoes = new List<Toggle>();

    //Mensagem de erro caso não seja selecionado nenhuma sessão
    public GameObject msgErro;
    //Lista que guarda quais sessões foram selecionadas para gerar o gráfico
    public static List<string> visualizarSessao = new List<string>();

    void Start(){

        // dadosJogo.BuscaSessao("Adalberto");
        dadosJogo.BuscaSessao(listaPacientes.nomePaciente);

        toggleSessoes = new List<Toggle>();
        visualizarSessao = new List<string>();

        //Renderiza o nome do paciente na lista
        var copy = Instantiate(itemTemplate);
        copy.transform.SetParent(content.transform, false);

        copy.GetComponentInChildren<Text>().text = "Paciente: " + listaPacientes.nomePaciente;
        copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
        
        Color colorP;
        ColorUtility.TryParseHtmlString("#1d3557", out colorP);
        copy.GetComponentInChildren<Text>().color = colorP;
        copy.SetActive(true);

        foreach(var item in dadosJogo.dadosCadastraisPaciente){
            if(item.Contains("Sessão")){  
                var section = Instantiate(itemSessao);
                section.transform.SetParent(content.transform, false);

                // string[] separar = item.Split(':'); 

                section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().text = item;
                // section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                Color color;
                ColorUtility.TryParseHtmlString("#333333", out color);
                section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().color = color;

                toggleSessoes.Add(section.GetComponentInChildren<Toggle>());
                section.SetActive(true);
                
            }else if(item.Contains("Efetividade")){

                var itemApr = Instantiate(itemTemplate);
                itemApr.transform.SetParent(content.transform, false);

                itemApr.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#ff006e", out color);
                itemApr.GetComponentInChildren<Text>().color = color;

                itemApr.SetActive(true);
            }else if(item.Contains("Desempenho")){

                var itemApr = Instantiate(itemTemplate);
                itemApr.transform.SetParent(content.transform, false);

                itemApr.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#8ac926", out color);
                itemApr.GetComponentInChildren<Text>().color = color;

                itemApr.SetActive(true);
            }
        }

    }

    public void GerarHistorico(){

        int qntSelect = 0;

        foreach(var item in toggleSessoes){
            // Debug.Log("Elemento adicionado é: " + item.GetComponentInChildren<Text>().text);
            // Debug.Log("Ele está selecionado? " + item.isOn);
            if(item.isOn){
                qntSelect++;
                visualizarSessao.Add(item.GetComponentInChildren<Text>().text);
            }
        }

        if(qntSelect == 0){
            msgErro.SetActive(true);
        }else{
            // Destroy(itemSessao.gameObject);
            // Destroy(itemTemplate.gameObject);
            // Destroy(content.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneHistorico");
        }

    }
    
}
