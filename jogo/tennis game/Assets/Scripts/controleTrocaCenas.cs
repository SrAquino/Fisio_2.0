using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleTrocaCenas : MonoBehaviour
{

    public static int entradaCena;

    //Função que realiza a troca de cenas enviando o nome da cena via input do botão
    public void TrocaCena(string nomeCena){
        UnityEngine.SceneManagement.SceneManager.LoadScene(nomeCena);
    }

    public void VerificaEncaminhamento(int encaminhar){
        if(encaminhar == 0){
            coletaDadosFisicos.opcao = 2;
            entradaCena = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneColetaDadosFisicos");
        }else if(encaminhar == 1 && coletaDadosFisicos.opcao == 2){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneAcessoHistSair");
        }else if(encaminhar == 1 && coletaDadosFisicos.opcao == 1){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneConfigPartida");
        }
    }

    public void CarregaConfigPartida(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("sceneConfigPartida");
    }

    public void TrocaAcao(){
        if(entradaCena == 0){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneCalibragem");
        }else if(entradaCena == 1){
            coletaDadosFisicos.opcao = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneColetaDadosFisicos");
        }
    }

    public void SairJogo(){
         Application.Quit();
    }
}
