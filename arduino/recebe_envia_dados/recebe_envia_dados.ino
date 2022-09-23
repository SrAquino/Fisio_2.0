#include <SerialCommand.h>
//Faz comunicação com o arduino enviando informação para mover para a direita ou esquerda
//Recebe a direcao
//Emite uma mensagem do sistema quando necessário enviar o log da partida
//Tudo isso usando módulo HC-05
//Posição lógica das entradas: branco, cinza [branco = TX2(RX), cinza = RX2(TX)]

const int BUFFER_SIZE = 50;
char buf[BUFFER_SIZE];

void setup() {
  //Seta porta serial de comunicação
  Serial.begin(115200);
  Serial2.begin(9600); //TX2, RX2 
  Serial.flush();
  Serial2.flush();
  //led apenas para testes de recebimento/envio de dados
  pinMode(53, OUTPUT);
  pinMode(13, OUTPUT);
}

void loop() {
  
  if(Serial2.available()){

    String leituraBuffer = Serial2.readStringUntil('\n');
    int tamanhoString = leituraBuffer.length();

    //Serial.print("Leitura: ");
    //Serial.println(leituraBuffer);

    if(tamanhoString == 2){
      if(leituraBuffer == "3\r"){
        Serial2.write("4");
      }else if(leituraBuffer == "6\r"){
        Serial.write("6");
      }else if(leituraBuffer == "7\r"){
        Serial.write("7");
      }else if(leituraBuffer == "9\r"){
        Serial.write("9");
      }else if(leituraBuffer == "A\r"){
        Serial.write("A");
      }
    }else if(tamanhoString == 3){
      Serial.write("P;\n");
    }else if(tamanhoString == 6){
      Serial.println(leituraBuffer);
    }
  }
  
  if(Serial.available()){ 
    //valor recebido da unity    
    int valorRecebidoUnity = Serial.read();

    //Serial.print("Recebido da Unity: ");
    //Serial.println(valorRecebidoUnity);
 
    if(valorRecebidoUnity == '0'){ //calibragem direita
      Serial2.write("0");
    }else if(valorRecebidoUnity == '1'){ //calibragem esquerda
      Serial2.write("1");
    }else if(valorRecebidoUnity == '2'){ //movimento
      Serial2.write("2");
    }else if(valorRecebidoUnity == '5'){ //restart
      Serial2.write("5");
    }
    
  }
       
}
