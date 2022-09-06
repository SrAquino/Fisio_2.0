using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    // Animator animator;
    public Transform ball;
    public Transform aimTarget;

    Vector3 targetPosition;

    //configura velocidade e força de rebate da bolinha
    public static float speed; //25
    public static float force; //15
    

    public Transform[] targets;

    public static bool flagDificuldade;
    
     //variável para fazer ball direcionar p/ direita, p/ meio, p/esquerda quando modo fácil
    int posicaoBall = 0; //0-M, 1-D, 2-E
    //Variável responsável por variar o erro de rebate 
    int variacaoErro;

    // Start is called before the first frame update
    void Start()
    {
        variacaoErro = 0;
        posicaoBall = 0;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if( variacaoErro % 2 == 0){
            Move();
        }
        variacaoErro++;
    }

    //mover em direção a alvo aleatório
    void Move(){
        targetPosition.z = ball.position.z;
        //movimenta bot entre posição atual e targetPosition(no caso é a pos da ball) com a velocidade especificada
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    //função para retornar aleatoriamente uma posição p/ rebate da bolinha pelo bot - 3 opções por enquanto
    Vector3 PickTarget(){
        int randomValue =  Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    //Função para retornar D, M, E nesta ordem o rebate da bolinha para que o paciente mova o coto nessas direçoes
    Vector3 DirecionaBallFacil(){
        if(posicaoBall == 3){
            posicaoBall = 0;
        }
        return targets[posicaoBall].position;
    }

    private void OnTriggerEnter(Collider other){
        //verifica se a colisão foi com a bola
        if(other.CompareTag("Ball")){
            Vector3 dir;

            if(flagDificuldade == false){
                dir = DirecionaBallFacil() - transform.position;
                posicaoBall++;
                // if(posicaoBall < 3){
                //     posicaoBall++;
                // }

                // if(posicaoBall == 3){
                //     posicaoBall = 0;
                // }
            }else{
                dir = PickTarget() - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            }
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0); //6
            Vector3 ballDir = ball.position - transform.position;
            ball.GetComponent<Ball>().hitter = "bot";
            ball.GetComponent<Ball>().playing = true;
            Ball.numRebates++;
        }
    }
    
}
