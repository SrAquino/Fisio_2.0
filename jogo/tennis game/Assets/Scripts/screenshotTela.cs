using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using UnityEngine.UI;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;

public class screenshotTela : MonoBehaviour
{

    string _caminho;

    void Start()
    {

        _caminho = Application.dataPath + "/graficos/";
        if(!Directory.Exists(_caminho)){
            Directory.CreateDirectory(_caminho);
        }

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficos"){
            Debug.Log("Foto tirada efetividade!");
            string nomeImagem = _caminho + "GraficoEfetividade" + ".png";
            ScreenCapture.CaptureScreenshot(nomeImagem);
        }else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "sceneGraficoDesempenho"){
            Debug.Log("Foto tirada desempenho!");
            string nomeImagem = _caminho + "GraficoDesempenho" + ".png";
            ScreenCapture.CaptureScreenshot(nomeImagem);

            //Gerar arquivo PDF com todos os dados das partidas selecionados e os respectivos gráficos
            //listaHistorico.gerarSessoes //acesso aos dados
            //foreach(var item in listaHistorico.gerarSessoes)

            Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
            doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string caminhoSalvar = "/home/liss/Documentos/" + "Relatorio_" + listaPacientes.nomePaciente + ".pdf";

            //criando o arquivo pdf embranco, passando como parametro a variavel doc criada acima e a variavel caminho
            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoSalvar, FileMode.Create));

            doc.Open();

            //criando uma string vazia
            string dados="";

            iTextSharp.text.Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);
            iTextSharp.text.Font fonteT = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 20);

            //criando a variavel para paragrafo
            Paragraph paragrafo = new Paragraph(dados, fonteT);
            //etipulando o alinhamneto
            paragrafo.Alignment = Element.ALIGN_CENTER;
            
            //Alinhamento Justificado
            
            //adicioando texto
            paragrafo.Add("RELATÓRIO TENNISGAME PHYSIO \n Paciente: " + listaPacientes.nomePaciente);
            paragrafo.Add("\n");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo);

            foreach(var item in listaHistorico.dadosSessao){
                Paragraph dadosP = new Paragraph(dados, fonte);
                //etipulando o alinhamneto
                dadosP.Alignment = Element.ALIGN_JUSTIFIED;
                //Alinhamento Justificado
                if(item.Contains("Dados da partida")){
                    dadosP.Add("\n");
                }
                dadosP.Add(item);
                dadosP.Add("\n");
                doc.Add(dadosP);
            }

            //Adicionando imagens 

            string caminhoImg = _caminho + "GraficoEfetividade.png";

            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(caminhoImg);

            gif.ScalePercent(40f);
            gif.Alignment = Element.ALIGN_CENTER;

            Paragraph graphE = new Paragraph("\n Gráfico de Efetividade \n", fonte);
            graphE.Alignment = Element.ALIGN_CENTER;

            doc.Add(graphE); //rotulo do gráfico
            doc.Add(gif);

            caminhoImg = _caminho + "GraficoDesempenho.png";

            gif = iTextSharp.text.Image.GetInstance(caminhoImg);

            gif.ScalePercent(40f);
            gif.Alignment = Element.ALIGN_CENTER;

            Paragraph graphD = new Paragraph("\n Gráfico de Desempenho \n", fonte);
            graphD.Alignment = Element.ALIGN_CENTER;

            doc.Add(graphD); //rotulo do gráfico
            doc.Add(gif);

            //fechando documento para que seja salva as alteraçoes.
            doc.Close();

        }
        
    }

}
