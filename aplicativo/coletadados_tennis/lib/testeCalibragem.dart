import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:sensors_plus/sensors_plus.dart';
import 'dart:typed_data';
import 'dart:async';
import 'dart:convert';
import 'dart:math';

class Calibragem extends StatefulWidget {
  @override
  _CalibragemState createState() => new _CalibragemState();
}

class _CalibragemState extends State<Calibragem> {
  var _aceleracao;
  var _extremoDir;
  var _extremoEsq;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(title: Text('Calibragem')),
        body: SafeArea(
          child: Column(
            children: [
              /*Container(
                alignment: Alignment.center,
                padding: const EdgeInsets.fromLTRB(0, 10, 0, 0), //l, t, r, b
                child: Text(
                    _extremoDir == null ? "AGUARDANDO DIREITA" : _extremoDir),
              ),
              Container(
                alignment: Alignment.center,
                padding: const EdgeInsets.fromLTRB(0, 10, 0, 0), //l, t, r, b
                child: Text(
                    _extremoEsq == null ? "AGUARDANDO ESQUERDA" : _extremoEsq),
              ),*/
              Container(
                alignment: Alignment.center,
                padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
                child: ElevatedButton(
                  onPressed: () async {
                    //calibragem(1);
                  },
                  // child: Text("Ligar"),
                  child: Text("DIREITA"),
                ),
              ),
              Container(
                alignment: Alignment.center,
                padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
                child: ElevatedButton(
                  onPressed: () async {
                    calibragem(0);
                  },
                  // child: Text("Ligar"),
                  child: Text("ESQUERDA"),
                ),
              ),
            ],
          ),
        ));
  }

  void leituraSensores() async {
    //Leitura dos sensores
    //aceleração com gravidade - m/s²
    accelerometerEvents.listen((AccelerometerEvent event) {
      setState(() {
        _aceleracao = (event.x).toStringAsFixed(0) + ";";
      });
    });
  }

  void calibragem(int extremo) async {
    //valor de calibragem p/ direita maximo
    int valorDireitaMax = 0;
    //valor de calibragem p/ esquerda maximo
    int valorEsquerdaMax = 0;

    //condição para a calibragem da extrema direita
    if (extremo == 1) {
      //Função para esperar por 2 segs e depois executa o que tem dentro da funcao
      // await Future.delayed(const Duration(seconds: 5), () {
      //   print("Olá, mundo!");
      // });

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
            _extremoDir = valorDireitaMax.toString();
          });
        });

        //Cancela função aos 15 segundos
        if (numVezes == 15) {
          numVezes = 1; //reinicializa numero de vezes
          aceleration.cancel(); //cancela listen do acelerometro
          timer.cancel(); //cancela funçao de timer
        }
        numVezes++;
      });
    } else if (extremo == 0) {
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
            } else if (acel > valorEsquerdaMax) {
              valorEsquerdaMax = acel;
              //print(acel);
            } else if (acel.abs() < valorEsquerdaMax) {
              //print(valorDireitaMax);
            }
            _extremoEsq = valorEsquerdaMax.toString();
          });
        });

        //Cancela função aos 15 segundos
        if (numVezes == 15) {
          numVezes = 1; //reinicializa numero de vezes
          aceleration.cancel(); //cancela listen do acelerometro
          timer.cancel(); //cancela funçao de timer
        }
        numVezes++;
      });
    }
  }
}
