using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retornarCena : MonoBehaviour
{
    public void Retornar(string nomeCena){
        if(coletaDadosFisicos.opcao == 2){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneAcessoHistSair");
        }else{
            UnityEngine.SceneManagement.SceneManager.LoadScene(nomeCena);
        }
    }
    public void RetornarListaSessoes(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("sceneSelecionaSessoes");
    }

    //Controla quais gráficos irão aparecer
    public void graficoAnterior(){

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficos"){
            Debug.Log("Mantém!");
        }else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficoDesempenho"){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneGraficos");
        }

    }

    public void proximoGrafico(){
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficoDesempenho"){
            Debug.Log("Mantém!");
        }else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficos"){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneGraficoDesempenho");
        }
    }

}
