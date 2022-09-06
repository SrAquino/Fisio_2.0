using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coletaRendDadosPartida : MonoBehaviour
{
    //GameObject placares
    public GameObject placarPlayer;
    public GameObject placarBot;
    //GameObject referente ao ganhador colocando um trofeu em cima do placar vencedor
    public GameObject indicadorPlayer;
    public GameObject indicadorBot;
    //Gameobject referente ao número total de rebates
    public GameObject numTotalRebates;
    //GameObject referente ao número de acertos e erros e suas porcentagens
    public GameObject numAcertos;
    public GameObject numErros;
    //GameObject que demonstra qual foi o modo da partida
    public GameObject modoPartida;
    //GameObject referente ao número de partidas executadas no dia da sessão deste paciente
    public GameObject aproveitamento;
    public GameObject desempenhoPartida;
    //GameObject referente a duração da partida atual
    public GameObject duracaoPartida;

    int renderizar = 0;

    

    void Update()
    {
        if(renderizar == 0){

            string escrita;

            escrita = "P\n";

            placarPlayer.GetComponent<Text>().text = (Ball.playerScore).ToString();
            placarBot.GetComponent<Text>().text = (Ball.botScore).ToString();

            if(Ball.botScore > Ball.playerScore){
                indicadorBot.SetActive(true);
            }else if(Ball.playerScore > Ball.botScore){
                indicadorPlayer.SetActive(true);
            }


            numTotalRebates.GetComponent<Text>().text = (Ball.numRebates).ToString();

            numAcertos.GetComponent<Text>().text = (Player.numAcertos).ToString();
            numErros.GetComponent<Text>().text = (Ball.numErros).ToString();

            int pp = Ball.playerScore;
            int pb = Ball.botScore;
            int a = Player.numAcertos;
            int r = Ball.numRebates;
            // Debug.Log("Número de acertos: " + a);
            // Debug.Log("Número de rebates: " + r);
            double efetividade =(double) pp / (pp + pb);
            efetividade = efetividade * 100;
            double desempenho =(double) (a - pp) / (r - pb);
            desempenho = desempenho * 100;

            // Debug.Log("Aproveitamento: " + aproveitamentoAcertos);
            aproveitamento.GetComponent<Text>().text = efetividade.ToString("F") + "%";
            desempenhoPartida.GetComponent<Text>().text = desempenho.ToString("F") + "%";
            
            
            escrita = escrita + (Ball.playerScore).ToString() + "," + (Ball.botScore).ToString() + "\n" + (Ball.numRebates).ToString() + "\n" + (Player.numAcertos).ToString() + "\n" +  (Ball.numErros).ToString() + "\n" + efetividade.ToString("F") + "\n" + desempenho.ToString("F") + "\n";
            
            switch(controllGame.modalidadeJogo){
                case 1: //tempo
                    modoPartida.GetComponent<Text>().text = "tempo";
                    escrita = escrita + "tempo\n";
                    break;
                case 2: //fácil 
                    modoPartida.GetComponent<Text>().text = "nível fácil";
                    escrita = escrita + "nível fácil\n";
                    break;
                case 3: //médio
                    modoPartida.GetComponent<Text>().text = "nível médio";
                    escrita = escrita + "nível médio\n";
                    break;
                case 4: //difícil
                    modoPartida.GetComponent<Text>().text = "nível difícil";
                    escrita = escrita + "nível difícil\n";
                    break;
            }

            float tempoDurantePartida = controllGame.tempoTotalPartida;
            if(tempoDurantePartida > 60f){
                float min = tempoDurantePartida/60;
                // Debug.Log("Tempo decorrido é de " + min + "min");
                duracaoPartida.GetComponent<Text>().text = (min).ToString() + " min";
                escrita = escrita + (min).ToString() + "min\n*";
            }else if(tempoDurantePartida == 60f){
                // Debug.Log("Tempo decorrido é de 1 minuto");
                duracaoPartida.GetComponent<Text>().text = "1 min";
                escrita = escrita + "1 min\n*";
            }else if(tempoDurantePartida < 60f){
                // Debug.Log("Tempo decorrido é de " + tempoDurantePartida + "seg");
                duracaoPartida.GetComponent<Text>().text = (tempoDurantePartida).ToString() + "seg";
                escrita = escrita + (tempoDurantePartida).ToString() + "seg\n*";
            }
            
            dadosJogo.Salvar(escrita);
            renderizar = 1;
            
        }
    }

}
