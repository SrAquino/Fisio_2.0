using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class listaTeste : MonoBehaviour
{
   //referência com a configuração pronta
    public GameObject itemTemplate;
    //Local para onde deve ser adicionado o item
    public GameObject content;

    //Lista que guarda todos os toggles adicionados na tela
    public static List<Toggle> toggleSessoes = new List<Toggle>();

    public void AddButt(){

        for(int i = 0; i<5; i++){
            //faz uma cópia do template
            var copy = Instantiate(itemTemplate);

            //adiciona copia dentro do content
            copy.transform.SetParent(content.transform, false);
        }
        
    }

    void Start(){

        for(int i = 0; i<5; i++){
            //faz uma cópia do template
            var copy = Instantiate(itemTemplate);

            //adiciona copia dentro do content
            copy.transform.SetParent(content.transform, false);

            copy.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().text = "Elemento " + i.ToString();
            toggleSessoes.Add(copy.GetComponentInChildren<Toggle>());
        }

    }

    public void AtualizarQuemSao(){
        foreach(var item in toggleSessoes){
            Debug.Log("Elemento adicionado é: " + item.GetComponentInChildren<Text>().text);
            Debug.Log("Ele está selecionado? " + item.isOn);
        }
    }

}
