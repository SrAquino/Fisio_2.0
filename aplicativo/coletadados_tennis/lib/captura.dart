import 'dart:async';
import 'dart:convert';
import 'dart:math';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';
// import 'package:sensors/sensors.dart';
import 'package:sensors_plus/sensors_plus.dart';

class AceleroPage extends StatefulWidget {
  final BluetoothDevice server;

  const AceleroPage(
      {this.server}); //recebo o end do dispositivo selecionado para conexão bluetooth

  @override
  _AceleroPage createState() => new _AceleroPage();
}

class _AceleroPage extends State<AceleroPage> {
  BluetoothConnection connection;

  bool isConnecting = true;
  bool get isConnected => connection != null && connection.isConnected;

  bool isDisconnecting = false;

  String _aceleracao;
  var _direcao;
  int _calibragemDir;
  int _calibragemEsq;
  var _acao;

  double _feDir;
  double _feEsq;

  @override
  void initState() {
    super.initState();

    BluetoothConnection.toAddress(widget.server.address).then((_connection) {
      print('Conectado ao dispositivo');
      connection = _connection;
      connection.output
          .add(utf8.encode("3" + "\r\n")); //comunicação estabelecida
      setState(() {
        isConnecting = false;
        isDisconnecting = false;
      });

      connection.input.listen(_onDataReceived).onDone(() {
        if (isDisconnecting) {
          print('Desconectando localmente!');
        } else {
          print('Desconectado remotamente!');
        }
        if (this.mounted) {
          setState(() {});
        }
      });
    }).catchError((error) {
      print('Não é possível conectar, ocorreu uma exceção');
      print(error);
    });
  }

  @override
  void dispose() {
    // Avoid memory leak (`setState` after dispose) and disconnect
    if (isConnected) {
      isDisconnecting = true;
      connection.dispose();
      connection = null;
    }

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: (isConnecting
              ? Text('Conectando a ' + widget.server.name + '...')
              : isConnected
                  ? Text('Conectado com ' + widget.server.name)
                  : Text('Log com ' + widget.server.name))),
      body: SafeArea(
        child: Column(
          children: [ 
            // Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("3");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("3"),
            //     ),
            //   ),
            // Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("6");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("6"),
            //     ),
            //   ),
            //   Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 0, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("7");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("7"),
            //     ),
            //   ),
            //   Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 0, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("P;");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("P;"),
            //     ),
            //   ),
            //   Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 0, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("E;3.2");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("E;7.2"),
            //     ),
            //   ),
            //   Container(
            //     alignment: Alignment.center,
            //     padding: const EdgeInsets.fromLTRB(0, 0, 0, 0),
            //     child: ElevatedButton(
            //       onPressed: () async {
            //         _sendMessage("D;4.6");
            //       },
            //       // child: Text("Ligar"),
            //       child: Text("D;4.6"),
            //     ),
            //   ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 100, 0, 0), //l, t, r, b
              child: Icon(
                _acao == 1 
                ? Icons.build_circle_outlined 
                : _acao == 2
                  ? Icons.compare_arrows
                  : _acao == 3
                    ? Icons.done_all
                    : Icons.loop_outlined,
                color: Color(0xFF2E5889),
                size: 280,
              ),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              child: Text(
                _acao == 1 
                ? "REALIZANDO CALIBRAGEM | DIREITA = $_calibragemDir ESQUERDA = $_calibragemEsq"
                : _acao == 2
                  ? "CAPTURANDO MOVIMENTO: $_direcao"
                  : _acao == 3
                    ? "CONEXÃO ESTABELECIDA"
                    : "AGUARDANDO CONEXÃO",
                textAlign: TextAlign.center,
                style: const TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 25,
                  color: Color(0xFF2E5889),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  //recebe dados via bluetooth
  void _onDataReceived(Uint8List data) {
    print("Recebendo uma mensagem do módulo!");

    String entrada = new String.fromCharCodes(data);

    // print(entrada);

    //variavel receptora de valores via bluetooth
    int dadosBluetooth = int.parse(entrada);

    setState(() {
      //Verifica ação necessária dependendo da entrada recebida via Bluetooth
      if (dadosBluetooth == 0 || dadosBluetooth == 1) {
        //calibragem
        // print("Calibragem");
        _sendMessage("6");//emite aviso de inicio da calibragem para o jogo
        calibragemGiroscopio(dadosBluetooth);
        _acao = 1; //realizando calibragem
      }else if(dadosBluetooth == 2){
        //movimento
        // print("Movimento");
        movimentoAvatarGiroscopio();
        //_sendMessage("Capturando");
        _acao = 2; //capturando movimento
      }else if (dadosBluetooth == 4) {
        //conexão estabelecida ok 
        // print("Conexão");
        _acao = 3;
      }
    });
  }

  //envia dados via bluetooth
  void _sendMessage(String saida) async {
    print("Enviando uma mensagem ao módulo!");
    saida = saida.trim();
    print(utf8.encode(saida + "\r\n"));
    connection.output.add(utf8.encode(saida + "\r\n"));
  }

  //Captura dados do sensor acelerömetro desprezando gravidade
  //Ou seja, apenas a aceleração do usuário sobre o smartphone
  void leituraSensores() async {
    //Leitura dos sensores
    //aceleração com gravidade - m/s²
    accelerometerEvents.listen((AccelerometerEvent event) {
      setState(() {
        _aceleracao = (event.x).toStringAsFixed(0);
        //print(_aceleracao);
        _sendMessage(_aceleracao);
      });
    });
  }

  void calibragemAcelerometro(int extremo) async {
    //valor de calibragem p/ direita maximo
    int valorDireitaMax = 0;
    //valor de calibragem p/ esquerda maximo
    int valorEsquerdaMax = 0;

    //Função para esperar por 2 segs e depois executa o que tem dentro da funcao
    //Função de delay para dar inicio a calibragem
    //await Future.delayed(const Duration(seconds: 15), () {
    //  print("Delay da calibragem executado!");
    //});
    //condição para a calibragem da extrema direita
    if (extremo == 0) {

      //variavel de controle do num. de vezes da execução da função timer
      int numVezes = 1;
      //Função que executa a a cada 1 segundo
      Timer.periodic(Duration(seconds: 1), (Timer timer) {
        print("DIREITA - Segundos correntes: $numVezes");
        var aceleration;
        aceleration = accelerometerEvents.listen((AccelerometerEvent event) {
          setState(() {
            var acel = int.parse((event.x).toStringAsFixed(0));
            if (acel == 0) {
              //print("Zero!");
            } else if (acel > valorDireitaMax) {
              valorDireitaMax = acel;
              //print(acel);
            } else if (acel.abs() < valorDireitaMax) {
              //print(valorDireitaMax);
            }
            //_calibragemDir = valorDireitaMax.toString();
            _calibragemDir = valorDireitaMax;
          });
        });

        //Cancela função aos 15 segundos
        if (numVezes == 15) {
          numVezes = 1; //reinicializa numero de vezes
          _sendMessage("7"); //envia msg de conclusão da calibragem
          aceleration.cancel(); //cancela listen do acelerometro
          timer.cancel(); //cancela funçao de timer
        }
        numVezes++;
      });
    } else if (extremo == 1) {
      //calibragem da extrema esquerda
      //variavel de controle do num. de vezes da execução da função timer
      int numVezes = 1;
      //Função que executa a a cada 1 segundo
      Timer.periodic(Duration(seconds: 1), (Timer timer) {
        print("ESQUERDA - Segundos correntes: $numVezes");
        var aceleration;
        aceleration = accelerometerEvents.listen((AccelerometerEvent event) {
          setState(() {
            var acel = int.parse((event.x).toStringAsFixed(0));
            if (acel == 0) {
              //print("Zero!");
            } else if (acel < valorEsquerdaMax) {
              valorEsquerdaMax = acel;
              //print(acel);
            } else if (acel.abs() < valorEsquerdaMax) {
              //print(valorDireitaMax);
            }
            //_calibragemEsq = valorEsquerdaMax.toString();
            _calibragemEsq = valorEsquerdaMax;
          });
        });

        //Cancela função aos 15 segundos
        if (numVezes == 15) {
          numVezes = 1; //reinicializa numero de vezes
          _sendMessage("7"); //envia msg de conclusão da calibragem
          aceleration.cancel(); //cancela listen do acelerometro
          timer.cancel(); //cancela funçao de timer
        }
        numVezes++;
      });
    }
  }

  void movimentoAvatarAcelerometro(){
        accelerometerEvents.listen((AccelerometerEvent event) {
          setState(() {
            var valorAceleracaoAtual = int.parse((event.x).toStringAsFixed(0));
            if(valorAceleracaoAtual == _calibragemDir){
              // print("DIREITA");
              // print("Aceleração = $valorAceleracaoAtual");
              // print("-----------------");
              _sendMessage("D");
            }else if(valorAceleracaoAtual == _calibragemEsq){
              // print("ESQUERDA");
              // print("Aceleração = $valorAceleracaoAtual");
              // print("-----------------");
              _sendMessage("E");
            }else{
              // print("PARADO");
              // print("Aceleração = $valorAceleracaoAtual");
              // print("-----------------");
              _sendMessage("P");
            }
            //usando valores pre-configurados
            /*if(valorAceleracaoAtual == 0){
              print("PARADO");
              print(valorAceleracaoAtual);
              print("-----------------");
            }else if(valorAceleracaoAtual == 10){
              print("DIREITA");
              print(valorAceleracaoAtual);
              print("-----------------");
            }else if(valorAceleracaoAtual == (-6)){
              print("ESQUERDA");
              print(valorAceleracaoAtual);
              print("-----------------");
            }*/
            //Usando intervalo entre valores de calibragem e maior/menor que 1/-1
            /*if(valorAcelerecaoAtual > 1 && valorAcelerecaoAtual <= _calibragemDir){
              _sendMessage("DIREITA");
              _direcao = "D";
              //print("D");
            }else if(valorAcelerecaoAtual >= _calibragemEsq && valorAcelerecaoAtual < (-1)){
              _sendMessage("ESQUERDA");
              _direcao = "E";
              //print("E");
              }else if(valorAcelerecaoAtual == 0 || valorAcelerecaoAtual == 1 || valorAcelerecaoAtual == (-1)){
              _sendMessage("PARADO");
              _direcao = "P";
              //print("P");
            } */
          });
        });
  }

  void calibragemGiroscopio(int extremo) async {
    //valor de calibragem p/ direita maximo
    int valorDireitaMax = 0;
    //valor de calibragem p/ esquerda maximo
    int valorEsquerdaMax = 0;

    //Função para esperar por 2 segs e depois executa o que tem dentro da funcao
    //Função de delay para dar inicio a calibragem
    //await Future.delayed(const Duration(seconds: 15), () {
    //  print("Delay da calibragem executado!");
    //});
    
    // //condição para a calibragem da extrema direita
    if (extremo == 0) {

      //variavel de controle do num. de vezes da execução da função timer
      int numVezes = 1;
      //cria variavel do tipo stream para controle do listen
      final giroscopio = gyroscopeEvents.listen(null);

      //Função que executa a a cada 1 segundo
      Timer.periodic(Duration(seconds: 1), (Timer timer) {
        giroscopio.onData((data) {
          setState(() {
            var giro = int.parse((data.z).toStringAsFixed(0));
            print("Giro Dir = $giro");
            if (giro == 0) {
              //print("Zero!");
            } else if (giro < valorDireitaMax) {
                  valorDireitaMax = giro;
                  // print("Valor máximo da direita é: $valorDireitaMax");
                  //print(acel);
                  _sendMessage("A");
                  _calibragemDir = valorDireitaMax;
                } else if (giro.abs() < valorDireitaMax) {
                  //print(valorDireitaMax); 
                }
                //_calibragemDir = valorDireitaMax.toString();
                  // // Cancela função aos 7 segundos
                if (numVezes == 7) {
                  print("Tempo finalizado!");
                  // print(DateTime.now().second);
                  numVezes = 1; //reinicializa numero de vezes
                    if(_calibragemDir == 0 || _calibragemDir == null){ //necessário repetir calibragem
                      _sendMessage("9");
                      timer.cancel(); //cancela funçao de timer
                      giroscopio.pause(); //cancela listen do giroscópio
                    }else{//calibragem ok
                      _sendMessage("7"); //envia msg de conclusão da calibragem
                      timer.cancel(); //cancela funçao de timer
                      giroscopio.pause(); //cancela listen do giroscópio
                    }
                }
          });
        }); 
        numVezes++;
      });
    } else if (extremo == 1) {
      //calibragem da extrema esquerda
      //variavel de controle do num. de vezes da execução da função timer
      int numVezes = 1;

      final giroscopio = gyroscopeEvents.listen(null);

      //Função que executa a a cada 1 segundo
      Timer.periodic(Duration(seconds: 1), (Timer timer) {
        // print("ESQUERDA - Segundos correntes: $numVezes");
        // var giroscopio;
        giroscopio.onData((data) {
          setState(() {
            var giro = int.parse((data.z).toStringAsFixed(0));
            print("Giro Esq = $giro");
            if (giro == 0) {
              //print("Zero!");
            } else if (giro > valorEsquerdaMax) {
              valorEsquerdaMax = giro;
              // print("Valor máximo da esquerda é: $valorEsquerdaMax");
              _sendMessage("A");
              _calibragemEsq = valorEsquerdaMax;
            } else if (giro.abs() < valorEsquerdaMax) {
              //print(valorDireitaMax);
            }
            //_calibragemEsq = valorEsquerdaMax.toString();
            //Cancela função aos 7 segundos
            if (numVezes == 7) {
              print("Tempo finalizado");
              numVezes = 1; //reinicializa numero de vezes
              if(_calibragemEsq == 0 || _calibragemEsq == null){ //necessário repetir calibragem
                _sendMessage("9");
                timer.cancel(); //cancela funçao de timer
                giroscopio.pause();
              }else{
                _sendMessage("7"); //envia msg de conclusão da calibragem
                timer.cancel(); //cancela funçao de timer
                giroscopio.pause(); //cancela listen dLineo giroscópio
                calculaFatorEscala();
              }
            }
          });
        });
        // _sendMessage("E");
        numVezes++;
      });
    }
  }

  void movimentoAvatarGiroscopio(){
    var giroscopioMove;
    giroscopioMove = gyroscopeEvents.listen((GyroscopeEvent event) {
      // print(int.parse((event.z).toStringAsFixed(0)));

      // int valorGiroscopioAtual = int.parse((event.z).toStringAsFixed(1));
      double valorGiroscopioAtual = double.parse((event.z).toStringAsFixed(1));
      // setState(() {
        if(valorGiroscopioAtual > 0.2 && valorGiroscopioAtual <= _calibragemEsq){
        // if(valorGiroscopioAtual >= 1 && valorGiroscopioAtual <= 8){
          double posicao = _feEsq * valorGiroscopioAtual;
          // double posicao = (-1.3) * valorGiroscopioAtual;
          posicao = posicao.abs();
          String movimento = "E" + ";" +  posicao.toStringAsFixed(1);
          _sendMessage(movimento);
          // _sendMessage("E");
          _direcao = "E";
          print(valorGiroscopioAtual);
          print(movimento);
          print("------------------------------------");
        // }else if(valorGiroscopioAtual >= (-8) && valorGiroscopioAtual <= (-1)){
        }else if(valorGiroscopioAtual >= _calibragemDir && valorGiroscopioAtual < (-0.2)){
          double posicao = _feDir * valorGiroscopioAtual;
          // double posicao = (1.3) * valorGiroscopioAtual;
          posicao = posicao.abs();
          String movimento = "D" + ";" +  posicao.toStringAsFixed(1);
          _sendMessage(movimento);
        // _sendMessage("D");
          _direcao = "D";
          print(valorGiroscopioAtual);
          print(movimento);
          print("------------------------------------");
        }else if(valorGiroscopioAtual >= (-0.2) && valorGiroscopioAtual <= 0.2){
          // _sendMessage("P;");
          _sendMessage("P;");
          _direcao = "P";
          print("P");
          print("------------------------------------");
        }
        giroscopioMove.cancel();
      // });
    });
  }

  void calculaFatorEscala(){
    //Esses valores de extrema direita e extrema esquerda são valores já conhecidos da Unity
    int extDir = 8; //extrema direita da mesa
    int extEsq = -8; //estrema esquerda da mesa
    
    _feDir = extDir / _calibragemDir.abs();
    _feDir = double.parse((_feDir).toStringAsFixed(1));
    _feEsq = extEsq / _calibragemEsq.abs();
    _feEsq = double.parse((_feEsq).toStringAsFixed(1));
    print("Fatores de escala calculados com sucesso! CAL_ESQ = $_calibragemEsq  FE_ESQ = $_feEsq CAL_DIR = $_calibragemDir FE_DIR = $_feDir");
    
  }

}
