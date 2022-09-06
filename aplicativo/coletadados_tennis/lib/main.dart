import 'package:coletadados_tennis/testeCalibragem.dart';
import 'package:flutter/material.dart';
import 'package:coletadados_tennis/selecionaModulo.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(
          brightness: Brightness.light, primaryColor: Color(0xFF2E5889)),
      home: MyHomePage(),
      debugShowCheckedModeBanner: false,
    );
  }
}

class MyHomePage extends StatefulWidget {
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: new AppBar(
      //   title: Text("TennisGame Physio"),
      //   centerTitle: true,
      // ),
      body: ListView(
        children: [
          Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.fromLTRB(
                0, 25, 0, 0), //left, top, right, bottom)
            child: SizedBox(
              width: 500,
              height: 250,
              child: Image.asset('images/LogoTCC.png'),
            ),
          ),
          Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.fromLTRB(
                0, 75, 0, 0), //left, top, right, bottom)
            child: SizedBox(
              width: 150,
              height: 45,
              child: (ElevatedButton(
                child: Text("INICIAR CAPTURA"),
                onPressed: () {
                  //redireciona para a tela de selecionar módulo hc-05
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) {
                        return PaginaLista();
                      },
                    ),
                  );
                  //Navigator.push(
                  //  context,
                  //  MaterialPageRoute(builder: (context) => Calibragem()),
                  //);
                },
                style: ElevatedButton.styleFrom(
                  primary: Color(0xFF4D7DB4),
                  textStyle: TextStyle(fontWeight: FontWeight.w600),
                ),
              )),
            ),
          ),
          Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.fromLTRB(
                0, 30, 0, 0), //left, top, right, bottom)
            child: SizedBox( 
              width: 150,
              height: 45,
              child: (ElevatedButton(
                child: Text("SAIR"),
                onPressed: () {},
                style: ElevatedButton.styleFrom(
                  primary: Color(0xFF4D7DB4),
                  textStyle: TextStyle(fontWeight: FontWeight.w600),
                ),
              )),
            ),
          ),
        ],
      ),
    );
  }
}
