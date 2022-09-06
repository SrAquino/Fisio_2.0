using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class listaFisioterapeutas : MonoBehaviour
{

    // public Text TextBox;

    string nomeFisioterapeuta;

    public GameObject msgErro;

    void Start()
    {
        dadosJogo.Leitura();
        var dropdown = transform.GetComponent<Dropdown>();

        // dropdown.options.Clear();

        // List<string> items = new List<string>();
        // items.Add("Selecione o seu nome");
        // items.Add("Item 1");
        // items.Add("Item 2");
        // items.Add("Item 3");
        // items.Add("Item 4");
        // items.Add("Item 5");
        // items.Add("Item 6");
        // items.Add("Item 7");
        // items.Add("Item 8");
        // items.Add("Item 9");
        // items.Add("Item 10");
        // items.Add("Item 11");
        
        (dadosJogo.fisioterapeutas).Sort();

        foreach(var item in dadosJogo.fisioterapeutas){
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});

    }

    void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
        // Debug.Log(dropdown.options[index].text);
        nomeFisioterapeuta = dropdown.options[index].text;
        if(nomeFisioterapeuta == "Selecione o seu nome"){
            // Debug.Log("Aguardando seleção para login");
        }else{
            
            // Debug.Log("Fisioterapeuta selecionado é: " + nomeFisioterapeuta);
            msgErro.SetActive(false);
        }

    //     TextBox.text = dropdown.options[index].text;
    }

    public void SalvarFisioterapeuta(){
        // dadosJogo.nomeFisioterapeuta = nomeFisioterapeuta.GetComponent<Text>().text;
        if(nomeFisioterapeuta != "Selecione o seu nome"){
            string escrita = "$\n" + System.DateTime.Now + "\n";
            escrita = escrita + nomeFisioterapeuta;
            dadosJogo.Salvar(escrita);
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneLoginPaciente");
        }else{
            msgErro.SetActive(true);
        }
    }


}
