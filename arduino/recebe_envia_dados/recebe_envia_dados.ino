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
  Serial2.begin(115200); //TX2, RX2 
  Serial.flush();
  Serial2.flush();
  //led apenas para testes de recebimento/envio de dados
  pinMode(53, OUTPUT);
  pinMode(13, OUTPUT);
}

void loop() {
  
  if(Serial2.available()){

    //Usando readString()

    String leituraBuffer = Serial2.readStringUntil('\n');

    int tamanhoString = leituraBuffer.length();

//    Serial.print("Tamanho: ");
//    Serial.println(tamanhoString);

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
    
    //Usando serialByte()
//    int rlen = Serial2.readBytes(buf, BUFFER_SIZE);
//    Serial2.flush();
//
//    //prints the received data
////    Serial.print("Tamanho do buffer: ");
////    Serial.println(rlen);
////    for(int i = 0; i < rlen; i++){
////      Serial.print(buf[i]);
////    }
//    if(rlen == 3){
//      if(buf[0] == '3'){
//      digitalWrite(53, HIGH);
//      delay(100);
//      digitalWrite(53, LOW);
//        Serial2.write("4");
//      }else{
//        Serial.write(buf[0]);
//      }
//    }else if(rlen == 4){ 
//        Serial.write("P;\n");
//    }else if(rlen == 7){
////        Serial.write(buf[0] + buf[1] + buf[2] + buf[3] + buf[4] + "\n");
//        if(buf[0] == 'E'){
//           Serial.println("E;" + buf[2, 3, 4]);
//        }else if(buf[0] == 'D'){
//          Serial.println("D;" + buf[2, 3, 4]);
//        }
//    }
//    
    //Usando read() 
//    int valorRecebidoSmartphone = Serial2.read();
//    if(valorRecebidoSmartphone == '3'){ //conexão estabelecida
//      digitalWrite(53, HIGH);
//      delay(100);
//      digitalWrite(53, LOW);
//      Serial2.write("4");
//    }else if(valorRecebidoSmartphone == '6'){ //aviso de inicio da calibragem 
//      Serial.write("6");
//    }else if(valorRecebidoSmartphone == '7'){ //calibragem concluida
//      Serial.write("7");
//    }else{
//      Serial.write(valorRecebidoSmartphone);
//    }
    

  //Parte de código que funciona com apenas D, E, P
//    else if(valorRecebidoSmartphone == 68){ //direita -68
//      Serial.write(valorRecebidoSmartphone);
//      digitalWrite(53, HIGH);
//      delay(100);
//      digitalWrite(53, LOW);
//      delay(100);
//      digitalWrite(53, HIGH);
//      delay(100);
//      digitalWrite(53, LOW);
//    }else if(valorRecebidoSmartphone == 69){ //esquerda
//      Serial.write(valorRecebidoSmartphone);
//    }else if(valorRecebidoSmartphone == 80){ //parado
//      Serial.write(valorRecebidoSmartphone);
//    }


  }
  
  if(Serial.available()){ 
    //valor recebido da unity    
    int valorRecebidoUnity = Serial.read();
 
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
