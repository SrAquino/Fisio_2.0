using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listaPacientes : MonoBehaviour
{

    public static string nomePaciente;

    public GameObject msgErro;

    void Start()
    {
        // dadosJogo.Leitura();
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

        (dadosJogo.pacientes).Sort();

        foreach(var item in dadosJogo.pacientes){
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});

    }

    void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
        nomePaciente = dropdown.options[index].text;

        if(nomePaciente == "Selecione o nome do paciente"){
            // Debug.Log("Aguardando seleção do paciente");
        }else{
            // Debug.Log("Paciente selecionado é: " + nomePaciente);
            msgErro.SetActive(false);
        }

        // Debug.Log(dropdown.options[index].text);
    //     TextBox.text = dropdown.options[index].text;
    }

    public void SalvarPaciente(){

        if(nomePaciente != "Selecione o nome do paciente"){
            string escrita = nomePaciente + "\n0\n0\n0"; 
            dadosJogo.Salvar(escrita);
            coletaDadosFisicos.opcao = 1;
            controleTrocaCenas.entradaCena = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneProximaAcao");
        }else{
            msgErro.SetActive(true);
        }
    }

}
