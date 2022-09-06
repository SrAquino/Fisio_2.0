using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class dadosJogo : MonoBehaviour
{
    //Dados fisioterapeuta
    public static string nomeFisioterapeuta;
    //Dados paciente
    public static string nomePaciente;
    public static string nivelAmputacao;
    public static string idade;
    public static string sexo;
    //Dados fisicos do paciente antes da partida
    public static string pressaoArterialAntes;
    public static string batimentosCardiacosAntes;
    public static string oxigenacaoSangueAntes;
    public static string observacoesAntes;
    //Dados fisicos do paciente apos partida
    public static string pressaoArterialDepois;
    public static string batimentosCardiacosDepois;
    public static string oxigenacaoSangueDepois;
    public static string observacoesDepois;
    //Dados da partida
    public static int numeroJogadas;
    
    //Lista que guarda o nome dos fisioterapeutas
    public static List<string> fisioterapeutas = new List<string>();
    //Lista que guarda o nome dos pacientes
    public static List<string> pacientes = new List<string>();

    //Lista que guarda os dados cadastrais do paciente solicitado

    public static List<string> dadosCadastraisPaciente = new List<string>();

    //Lista que guarda os dados físicos do paciente antes e após a sessão
    public static List<string> dadosFisicosPaciente = new List<string>();

    
    //Funçao para salvar dados em arquivo 
    public static void CreateText(){

        //Path of the file
        string path = Application.dataPath + "/Log.txt";
        //Create file if doesn't exist
        if(!File.Exists(path)){
            File.WriteAllText(path, "Logs TennisGame Physio \n\n");
        }
        //Add some to text to it
        string logger = "Log date:" + System.DateTime.Now + "\n";
        string dadosCadastrais = "Fisioterapeuta: " + nomeFisioterapeuta + " Paciente: " + nomePaciente + " Nível Amputação: " + nivelAmputacao + " Idade: " + idade + " Sexo: " + sexo + "\n";
        string dadosFisicosAntes = "Dados físicos antes da sessão: " + "Pressão Arterial: " + pressaoArterialAntes + "Batimentos cardíacos: " + batimentosCardiacosAntes + "Oxigenação do sangue: " + oxigenacaoSangueAntes + "Observações: " + observacoesAntes + "\n";
        string dadosFisicosDepois = "Dados físicos depois da sessão: " + "Pressão Arterial: " + pressaoArterialDepois + "Batimentos cardíacos: " + batimentosCardiacosDepois + "Oxigenação do sangue: " + oxigenacaoSangueDepois + "Observações: " + observacoesDepois + "\n";
        string escrever = logger + dadosCadastrais + dadosFisicosAntes + dadosFisicosDepois;
        File.AppendAllText(path, escrever);
    }

    public static void Salvar(string linha){
        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        if (!File.Exists(nomeArquivo)){
            File.Create(nomeArquivo).Close();
        }

        TextWriter arquivo = File.AppendText(nomeArquivo);
        arquivo.WriteLine(linha);
        arquivo.Close();
    }

    //Leitura de todo o arquivo e salva pacientes e fisioterapeutas
    public static void Leitura(){
        Debug.Log("Lendo arquivo...");
        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            Debug.Log("Data de execução: " + sr.ReadLine());
                            string nomeFisio = sr.ReadLine();
                            //Verifica se já existe o fisioterapeuta na lista, se sim, não adiciona
                            if (fisioterapeutas.IndexOf(nomeFisio) >= 0){
                                //Element found in list.
                                Debug.Log("Exists!");
                            }else{
                                fisioterapeutas.Add(nomeFisio);
                                Debug.Log("Not Exists!");
                            }
                            Debug.Log("Fisioterapeuta: " + nomeFisio);
                            string nomePaciente = sr.ReadLine();
                            //Verifica se já existe o paciente na lista, se sim, não adiciona
                            if (pacientes.IndexOf(nomePaciente) >= 0){
                                //Element found in list.
                                Debug.Log("Exists!");
                            }else{
                                pacientes.Add(nomePaciente);
                                Debug.Log("Not Exists!");
                            }
                            Debug.Log("Paciente: " + nomePaciente);
                            Debug.Log("Idade do Paciente: " + sr.ReadLine());
                            Debug.Log("Nível da amputação: " + sr.ReadLine());
                            Debug.Log("Sexo do paciente: " + sr.ReadLine());
                       }
                       else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A"){
                                Debug.Log("Dados físicos do paciente antes da partida: " );
                                Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                        
                        //     Debug.Log("-------------DF--------------");
                       }else if(linha == "##"){
                            linha = sr.ReadLine();
                            if(linha == "P"){
                                Debug.Log("Dados da partida: " );
                                string[] separa = sr.ReadLine().Split(',');
                                Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                Debug.Log("Total de rebates: " + sr.ReadLine());
                                Debug.Log("Número de acertos: " + sr.ReadLine());
                                Debug.Log("Número de erros: " + sr.ReadLine());
                                Debug.Log("Efetividade: " + sr.ReadLine());
                                Debug.Log("Desempenho: " + sr.ReadLine());
                                Debug.Log("Modo da partida: " + sr.ReadLine());
                                Debug.Log("Duração da partida: " + sr.ReadLine());
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            if(linha == "P"){
                                Debug.Log("Dados da partida: " );
                                string[] separa = sr.ReadLine().Split(',');
                                Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                Debug.Log("Total de rebates: " + sr.ReadLine());
                                Debug.Log("Número de acertos: " + sr.ReadLine());
                                Debug.Log("Número de erros: " + sr.ReadLine());
                                Debug.Log("Efetividade: " + sr.ReadLine());
                                Debug.Log("Desempenho: " + sr.ReadLine());
                                Debug.Log("Modo da partida: " + sr.ReadLine());
                                Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D"){
                                    Debug.Log("Dados físicos do paciente depois da partida: " );
                                    Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                    Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                    Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                                } 
                            }
                       }else if(linha == "####"){
                           Debug.Log("Fim do log");
                       }
                    }
                    sr.Close();
                }
            }catch{
                Debug.Log("ERRO");
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
    }


    //Realiza busca das sessões do paciente e seus aproveitamentos
    public static void BuscaSessao(string buscaPaciente){

        dadosCadastraisPaciente = new List<string>();

        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        int validaNome = 0;

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            string dataExecute = sr.ReadLine();
                            // Debug.Log("Data de execução: " + sr.ReadLine());
                            string nameFisio = sr.ReadLine();
                            string namePaciente = sr.ReadLine();

                            if(namePaciente == buscaPaciente){
                                validaNome = 1;
                                // Debug.Log("Sessão " + dataExecute);
                                // Debug.Log("Paciente: " + nomePaciente);
                             
                                string idade = sr.ReadLine();
                                string amputacao = sr.ReadLine();
                                string sexo = sr.ReadLine();

                                // Debug.Log("Idade do Paciente: " + idade);
                                // Debug.Log("Nível da amputação: " + amputacao);
                                // Debug.Log("Sexo do paciente: " + sexo);


                                if(idade != "0" && amputacao != "0" && sexo != "0"){
                                    // dadosSessoesAproveitamento.Add("Dados Cadastrais");
                                    // dadosSessoesAproveitamento.Add("Idade: " + idade);
                                    // dadosSessoesAproveitamento.Add("Nível da amputação: " + amputacao);
                                    // dadosSessoesAproveitamento.Add("Sexo: " + sexo);
                                }
                                dadosCadastraisPaciente.Add("Sessão: " + dataExecute);
                                // dadosCadastraisPaciente.Add(nomePaciente);
                            }else{
                                validaNome = 0;
                            }
                       }else if(linha == "##"){
                            // Debug.Log("Primeira! E nome é: " + validaNome);
                            linha = sr.ReadLine();
                            if(linha == "P" && validaNome == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosCadastraisPaciente.Add("Dados da partida");
                                // dadosCadastraisPaciente.Add("Modo da partida: " + modo);
                                // dadosCadastraisPaciente.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosCadastraisPaciente.Add("Total de rebates: " + rebate);
                                // dadosCadastraisPaciente.Add("Número de acertos: " + acertos);
                                // dadosCadastraisPaciente.Add("Número de erros: " + erros);
                                dadosCadastraisPaciente.Add("Efetividade: " + efetividade + "%");
                                dadosCadastraisPaciente.Add("Desempenho: " + desempenho + "%");
                                // dadosCadastraisPaciente.Add("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            // Debug.Log("Segundo! E nome é: " + validaNome);
                            if(linha == "P" && validaNome == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosCadastraisPaciente.Add("Dados da partida");
                                // dadosCadastraisPaciente.Add("Modo da partida: " + modo);
                                // dadosCadastraisPaciente.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosCadastraisPaciente.Add("Total de rebates: " + rebate);
                                // dadosCadastraisPaciente.Add("Número de acertos: " + acertos);
                                // dadosCadastraisPaciente.Add("Número de erros: " + erros);
                                dadosCadastraisPaciente.Add("Efetividade: " + efetividade + "%");
                                dadosCadastraisPaciente.Add("Desempenho: " + desempenho + "%");
                                // dadosCadastraisPaciente.Add("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + sr.ReadLine());
                                // Debug.Log("Número de acertos: " + sr.ReadLine());
                                // Debug.Log("Número de erros: " + sr.ReadLine());
                                // Debug.Log("Aproveitamento: " + sr.ReadLine());
                                // Debug.Log("Modo da partida: " + sr.ReadLine());
                                // Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D" && validaNome == 1){
                                    string pressao = sr.ReadLine();
                                    string batimentos =  sr.ReadLine();
                                    string oxigenacao = sr.ReadLine();

                                    if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                        // dadosCadastraisPaciente.Add("Dados Físicos");
                                        // dadosCadastraisPaciente.Add("Depois da partida");
                                        // dadosCadastraisPaciente.Add("Pressão Arterial: " + pressao);
                                        // dadosCadastraisPaciente.Add("Batimentos Cardíacos: " + batimentos);
                                        // dadosCadastraisPaciente.Add("Oxigenação do Sangue");
                                    }else{
                                        // dadosCadastraisPaciente.Add("Dados Físicos");
                                        // dadosCadastraisPaciente.Add("Depois da partida: não inseridos");
                                    }

                                    // Debug.Log("Dados físicos do paciente depois da partida: " );
                                    // Debug.Log("Pressão Arterial: " + pressao);
                                    // Debug.Log("Batimentos Cardiacos: " + batimentos);
                                    // Debug.Log("Oxigenação do sangue: " + oxigenacao);
                                } 
                            }
                       }else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A" && validaNome == 1){

                                string pressao = sr.ReadLine();
                                string batimentos =  sr.ReadLine();
                                string oxigenacao = sr.ReadLine();

                                if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                    // dadosCadastraisPaciente.Add("Dados Físicos");
                                    // dadosCadastraisPaciente.Add("Antes da partida:");
                                    // dadosCadastraisPaciente.Add("Pressão Arterial: " + pressao);
                                    // dadosCadastraisPaciente.Add("Batimentos Cardíacos: " + batimentos);
                                    // dadosCadastraisPaciente.Add("Oxigenação do Sangue");
                                }else{
                                    // dadosCadastraisPaciente.Add("Dados Físicos");
                                    // dadosCadastraisPaciente.Add("Antes da partida: não inseridos");
                                }

                                // Debug.Log("Dados físicos do paciente antes da partida: " );
                                // Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                // Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                // Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                       }
                       
                    }
                    sr.Close();
                }
            }catch{
                // Debug.Log("ERRO");
                throw;
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
    }

// Realiza a busca do paciente e os dados da sessão
   public static List<string> BuscaPartida(string buscaPaciente, string nomeSessao){
       //Lista para salvar os dados de aproveitamento das sessões e do paciente solicitados
        List<string> dadosPartida = new List<string>();

        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        int validaNome = 0;
        int validaSessao = 0;

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            string dataExecute = "Sessão: " + sr.ReadLine();
                            if(dataExecute.Contains(nomeSessao)){
                                validaSessao = 1;
                            }else{ 
                                validaSessao = 0;
                            }
                            // Debug.Log("Data de execução: " + sr.ReadLine());
                            string nameFisio = sr.ReadLine();
                            string namePaciente = sr.ReadLine();

                            if(namePaciente == buscaPaciente && validaSessao == 1){
                                validaNome = 1;
                                // Debug.Log("Sessão " + dataExecute);
                                // Debug.Log("Paciente: " + nomePaciente);
                             
                                string idade = sr.ReadLine();
                                string amputacao = sr.ReadLine();
                                string sexo = sr.ReadLine();

                                // Debug.Log("Idade do Paciente: " + idade);
                                // Debug.Log("Nível da amputação: " + amputacao);
                                // Debug.Log("Sexo do paciente: " + sexo);


                                if(idade != "0" && amputacao != "0" && sexo != "0"){
                                    dadosPartida.Add("Dados Cadastrais");
                                    dadosPartida.Add("Idade: " + idade);
                                    dadosPartida.Add("Nível da amputação: " + amputacao);
                                    dadosPartida.Add("Sexo: " + sexo);
                                }
                                dadosPartida.Add(dataExecute);
                            }else{
                                validaNome = 0;
                            }
                       }else if(linha == "##"){
                            // Debug.Log("Primeira! E nome é: " + validaNome);
                            linha = sr.ReadLine();
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                dadosPartida.Add("Dados da partida");
                                dadosPartida.Add("Modo da partida: " + modo);
                                dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                dadosPartida.Add("Total de rebates: " + rebate);
                                dadosPartida.Add("Número de acertos: " + acertos);
                                dadosPartida.Add("Número de erros: " + erros);
                                dadosPartida.Add("Efetividade: " + efetividade + "%");
                                dadosPartida.Add("Desempenho: " + desempenho + "%");
                                dadosPartida.Add("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            // Debug.Log("Segundo! E nome é: " + validaNome);
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                dadosPartida.Add("Dados da partida");
                                dadosPartida.Add("Modo da partida: " + modo);
                                dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                dadosPartida.Add("Total de rebates: " + rebate);
                                dadosPartida.Add("Número de acertos: " + acertos);
                                dadosPartida.Add("Número de erros: " + erros);
                                dadosPartida.Add("Efetividade: " + efetividade + "%");
                                dadosPartida.Add("Desempenho: " + desempenho + "%");
                                dadosPartida.Add("Duração da partida: " + duracao);


                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + sr.ReadLine());
                                // Debug.Log("Número de acertos: " + sr.ReadLine());
                                // Debug.Log("Número de erros: " + sr.ReadLine());
                                // Debug.Log("Aproveitamento: " + sr.ReadLine());
                                // Debug.Log("Modo da partida: " + sr.ReadLine());
                                // Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D" && validaNome == 1){
                                    string pressao = sr.ReadLine();
                                    string batimentos =  sr.ReadLine();
                                    string oxigenacao = sr.ReadLine();

                                    if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                        dadosPartida.Add("Dados Físicos");
                                        dadosPartida.Add("Depois da partida");
                                        dadosPartida.Add("Pressão Arterial: " + pressao);
                                        dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                        dadosPartida.Add("Oxigenação do Sangue");
                                    }else{
                                        dadosPartida.Add("Dados Físicos");
                                        dadosPartida.Add("Depois da partida: não inseridos");
                                    }

                                    // Debug.Log("Dados físicos do paciente depois da partida: " );
                                    // Debug.Log("Pressão Arterial: " + pressao);
                                    // Debug.Log("Batimentos Cardiacos: " + batimentos);
                                    // Debug.Log("Oxigenação do sangue: " + oxigenacao);
                                } 
                            }
                       }else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A" && validaNome == 1){

                                string pressao = sr.ReadLine();
                                string batimentos =  sr.ReadLine();
                                string oxigenacao = sr.ReadLine();

                                if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                    dadosPartida.Add("Dados Físicos");
                                    dadosPartida.Add("Antes da partida:");
                                    dadosPartida.Add("Pressão Arterial: " + pressao);
                                    dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                    dadosPartida.Add("Oxigenação do Sangue");
                                }else{
                                    dadosPartida.Add("Dados Físicos");
                                    dadosPartida.Add("Antes da partida: não inseridos");
                                }

                                // Debug.Log("Dados físicos do paciente antes da partida: " );
                                // Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                // Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                // Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                       }
                       
                    }
                    sr.Close();
                    
                }
            }catch{
                // Debug.Log("ERRO");
                throw;
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
        return dadosPartida;
    }

    public static List<string> BuscaEfetividade(string buscaPaciente, string nomeSessao){
       //Lista para salvar os dados de aproveitamento das sessões e do paciente solicitados
        List<string> dadosEfetividade = new List<string>();

        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        int validaNome = 0;
        int validaSessao = 0;

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            string dataExecute = "Sessão: " + sr.ReadLine();
                            if(dataExecute.Contains(nomeSessao)){
                                validaSessao = 1;
                            }else{ 
                                validaSessao = 0;
                            }
                            // Debug.Log("Data de execução: " + sr.ReadLine());
                            string nameFisio = sr.ReadLine();
                            string namePaciente = sr.ReadLine();

                            if(namePaciente == buscaPaciente && validaSessao == 1){
                                validaNome = 1;
                                // Debug.Log("Sessão " + dataExecute);
                                // Debug.Log("Paciente: " + nomePaciente);
                             
                                string idade = sr.ReadLine();
                                string amputacao = sr.ReadLine();
                                string sexo = sr.ReadLine();

                                // Debug.Log("Idade do Paciente: " + idade);
                                // Debug.Log("Nível da amputação: " + amputacao);
                                // Debug.Log("Sexo do paciente: " + sexo);


                                if(idade != "0" && amputacao != "0" && sexo != "0"){
                                    // dadosPartida.Add("Dados Cadastrais");
                                    // dadosPartida.Add("Idade: " + idade);
                                    // dadosPartida.Add("Nível da amputação: " + amputacao);
                                    // dadosPartida.Add("Sexo: " + sexo);
                                }
                                // dadosPartida.Add(dataExecute);
                            }else{
                                validaNome = 0;
                            }
                       }else if(linha == "##"){
                            // Debug.Log("Primeira! E nome é: " + validaNome);
                            linha = sr.ReadLine();
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosPartida.Add("Dados da partida");
                                // dadosPartida.Add("Modo da partida: " + modo);
                                // dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosPartida.Add("Total de rebates: " + rebate);
                                // dadosPartida.Add("Número de acertos: " + acertos);
                                // dadosPartida.Add("Número de erros: " + erros);
                                // dadosPartida.Add("Aproveitamento: " + aproveitamento + "%");
                                // dadosPartida.Add("Duração da partida: " + duracao);

                                dadosEfetividade.Add(efetividade);
                                // dadosEfetividade.Add(desempenho);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            // Debug.Log("Segundo! E nome é: " + validaNome);
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosPartida.Add("Dados da partida");
                                // dadosPartida.Add("Modo da partida: " + modo);
                                // dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosPartida.Add("Total de rebates: " + rebate);
                                // dadosPartida.Add("Número de acertos: " + acertos);
                                // dadosPartida.Add("Número de erros: " + erros);
                                // dadosPartida.Add("Aproveitamento: " + aproveitamento + "%");
                                // dadosPartida.Add("Duração da partida: " + duracao);

                                dadosEfetividade.Add(efetividade);
                                // dadosEfetividade.Add(desempenho);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + sr.ReadLine());
                                // Debug.Log("Número de acertos: " + sr.ReadLine());
                                // Debug.Log("Número de erros: " + sr.ReadLine());
                                // Debug.Log("Aproveitamento: " + sr.ReadLine());
                                // Debug.Log("Modo da partida: " + sr.ReadLine());
                                // Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D" && validaNome == 1){
                                    string pressao = sr.ReadLine();
                                    string batimentos =  sr.ReadLine();
                                    string oxigenacao = sr.ReadLine();

                                    if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                        // dadosPartida.Add("Dados Físicos");
                                        // dadosPartida.Add("Depois da partida");
                                        // dadosPartida.Add("Pressão Arterial: " + pressao);
                                        // dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                        // dadosPartida.Add("Oxigenação do Sangue");
                                    }else{
                                        // dadosPartida.Add("Dados Físicos");
                                        // dadosPartida.Add("Depois da partida: não inseridos");
                                    }

                                    // Debug.Log("Dados físicos do paciente depois da partida: " );
                                    // Debug.Log("Pressão Arterial: " + pressao);
                                    // Debug.Log("Batimentos Cardiacos: " + batimentos);
                                    // Debug.Log("Oxigenação do sangue: " + oxigenacao);
                                } 
                            }
                       }else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A" && validaNome == 1){

                                string pressao = sr.ReadLine();
                                string batimentos =  sr.ReadLine();
                                string oxigenacao = sr.ReadLine();

                                if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                    // dadosPartida.Add("Dados Físicos");
                                    // dadosPartida.Add("Antes da partida:");
                                    // dadosPartida.Add("Pressão Arterial: " + pressao);
                                    // dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                    // dadosPartida.Add("Oxigenação do Sangue");
                                }else{
                                    // dadosPartida.Add("Dados Físicos");
                                    // dadosPartida.Add("Antes da partida: não inseridos");
                                }

                                // Debug.Log("Dados físicos do paciente antes da partida: " );
                                // Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                // Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                // Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                       }
                       
                    }
                    sr.Close();
                    
                }
            }catch{
                // Debug.Log("ERRO");
                throw;
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
        return dadosEfetividade;
    }

    public static List<string> BuscaDesempenho(string buscaPaciente, string nomeSessao){
       //Lista para salvar os dados de aproveitamento das sessões e do paciente solicitados
        List<string> dadosDesempenho = new List<string>();

        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        int validaNome = 0;
        int validaSessao = 0;

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            string dataExecute = "Sessão: " + sr.ReadLine();
                            if(dataExecute.Contains(nomeSessao)){
                                validaSessao = 1;
                            }else{ 
                                validaSessao = 0;
                            }
                            // Debug.Log("Data de execução: " + sr.ReadLine());
                            string nameFisio = sr.ReadLine();
                            string namePaciente = sr.ReadLine();

                            if(namePaciente == buscaPaciente && validaSessao == 1){
                                validaNome = 1;
                                // Debug.Log("Sessão " + dataExecute);
                                // Debug.Log("Paciente: " + nomePaciente);
                             
                                string idade = sr.ReadLine();
                                string amputacao = sr.ReadLine();
                                string sexo = sr.ReadLine();

                                // Debug.Log("Idade do Paciente: " + idade);
                                // Debug.Log("Nível da amputação: " + amputacao);
                                // Debug.Log("Sexo do paciente: " + sexo);


                                if(idade != "0" && amputacao != "0" && sexo != "0"){
                                    // dadosPartida.Add("Dados Cadastrais");
                                    // dadosPartida.Add("Idade: " + idade);
                                    // dadosPartida.Add("Nível da amputação: " + amputacao);
                                    // dadosPartida.Add("Sexo: " + sexo);
                                }
                                // dadosPartida.Add(dataExecute);
                            }else{
                                validaNome = 0;
                            }
                       }else if(linha == "##"){
                            // Debug.Log("Primeira! E nome é: " + validaNome);
                            linha = sr.ReadLine();
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosPartida.Add("Dados da partida");
                                // dadosPartida.Add("Modo da partida: " + modo);
                                // dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosPartida.Add("Total de rebates: " + rebate);
                                // dadosPartida.Add("Número de acertos: " + acertos);
                                // dadosPartida.Add("Número de erros: " + erros);
                                // dadosPartida.Add("Aproveitamento: " + aproveitamento + "%");
                                // dadosPartida.Add("Duração da partida: " + duracao);

                                // dadosDesempenho.Add(efetividade);
                                dadosDesempenho.Add(desempenho);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            // Debug.Log("Segundo! E nome é: " + validaNome);
                            if(linha == "P" && validaNome == 1 && validaSessao == 1){
                                // Debug.Log("Dados da partida: " );
                                
                                string[] separa = sr.ReadLine().Split(',');
                                string rebate = sr.ReadLine();
                                string acertos = sr.ReadLine();
                                string erros = sr.ReadLine();
                                string efetividade = sr.ReadLine();
                                string desempenho = sr.ReadLine();
                                string modo = sr.ReadLine();
                                string duracao = sr.ReadLine();

                                // dadosPartida.Add("Dados da partida");
                                // dadosPartida.Add("Modo da partida: " + modo);
                                // dadosPartida.Add("Placar da partida " + buscaPaciente + ": [" + separa[0] + "][" + separa[1] + "]: Bot");
                                // dadosPartida.Add("Total de rebates: " + rebate);
                                // dadosPartida.Add("Número de acertos: " + acertos);
                                // dadosPartida.Add("Número de erros: " + erros);
                                // dadosPartida.Add("Aproveitamento: " + aproveitamento + "%");
                                // dadosPartida.Add("Duração da partida: " + duracao);

                                // dadosDesempenho.Add(efetividade);
                                dadosDesempenho.Add(desempenho);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + rebate);
                                // Debug.Log("Número de acertos: " + acertos);
                                // Debug.Log("Número de erros: " + erros);
                                // Debug.Log("Aproveitamento: " + aproveitamento);
                                // Debug.Log("Modo da partida: " + modo);
                                // Debug.Log("Duração da partida: " + duracao);

                                // Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                // Debug.Log("Total de rebates: " + sr.ReadLine());
                                // Debug.Log("Número de acertos: " + sr.ReadLine());
                                // Debug.Log("Número de erros: " + sr.ReadLine());
                                // Debug.Log("Aproveitamento: " + sr.ReadLine());
                                // Debug.Log("Modo da partida: " + sr.ReadLine());
                                // Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D" && validaNome == 1){
                                    string pressao = sr.ReadLine();
                                    string batimentos =  sr.ReadLine();
                                    string oxigenacao = sr.ReadLine();

                                    if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                        // dadosPartida.Add("Dados Físicos");
                                        // dadosPartida.Add("Depois da partida");
                                        // dadosPartida.Add("Pressão Arterial: " + pressao);
                                        // dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                        // dadosPartida.Add("Oxigenação do Sangue");
                                    }else{
                                        // dadosPartida.Add("Dados Físicos");
                                        // dadosPartida.Add("Depois da partida: não inseridos");
                                    }

                                    // Debug.Log("Dados físicos do paciente depois da partida: " );
                                    // Debug.Log("Pressão Arterial: " + pressao);
                                    // Debug.Log("Batimentos Cardiacos: " + batimentos);
                                    // Debug.Log("Oxigenação do sangue: " + oxigenacao);
                                } 
                            }
                       }else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A" && validaNome == 1){

                                string pressao = sr.ReadLine();
                                string batimentos =  sr.ReadLine();
                                string oxigenacao = sr.ReadLine();

                                if(pressao != "0" && batimentos != "0" && oxigenacao != "0"){
                                    // dadosPartida.Add("Dados Físicos");
                                    // dadosPartida.Add("Antes da partida:");
                                    // dadosPartida.Add("Pressão Arterial: " + pressao);
                                    // dadosPartida.Add("Batimentos Cardíacos: " + batimentos);
                                    // dadosPartida.Add("Oxigenação do Sangue");
                                }else{
                                    // dadosPartida.Add("Dados Físicos");
                                    // dadosPartida.Add("Antes da partida: não inseridos");
                                }

                                // Debug.Log("Dados físicos do paciente antes da partida: " );
                                // Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                // Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                // Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                       }
                       
                    }
                    sr.Close();
                    
                }
            }catch{
                // Debug.Log("ERRO");
                throw;
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
        return dadosDesempenho;
    }

}


