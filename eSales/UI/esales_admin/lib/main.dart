import 'package:flutter/material.dart';

void main() {
  runApp(const MyMaterialApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(
          // This is the theme of your application.
          //
          // TRY THIS: Try running your application with "flutter run". You'll see
          // the application has a purple toolbar. Then, without quitting the app,
          // try changing the seedColor in the colorScheme below to Colors.green
          // and then invoke "hot reload" (save your changes or press the "hot
          // reload" button in a Flutter-supported IDE, or press "r" if you used
          // the command line to start the app).
          //
          // Notice that the counter didn't reset back to zero; the application
          // state is not lost during the reload. To reset the state, use hot
          // restart instead.
          //
          // This works for code too, not just values: Most code changes can be
          // tested with just a hot reload.
          primarySwatch: Colors.blue,
        ),
        home: LayoutExamples() //Counter(),
        );
  }
}

class MyAppBar extends StatelessWidget {
  String? title;
  MyAppBar({super.key, required this.title});

  @override
  Widget build(BuildContext context) {
    return Text(title!);
  }
}

class Counter extends StatefulWidget {
  const Counter({super.key});

  @override
  State<Counter> createState() => _CounterState();
}

class _CounterState extends State<Counter> {
  int _count = 0;

  void _incrementCounter() {
    setState(() {
      _count++;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text('You have pushed the button $_count times'),
        ElevatedButton(onPressed: _incrementCounter, child: Text("Increment")),
        ElevatedButton(
            onPressed: _incrementCounter, child: Text("Increment drugi")),
      ],
    );
  }
}

class LayoutExamples extends StatelessWidget {
  const LayoutExamples({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Container(
          height: 150,
          color: Colors.red,
          child: Center(
            child: Container(
              height: 100,
              color: Colors.blue,
              child: Text("Example text"),
              alignment: Alignment.center,
            ),
          ),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Text("Item 1"),
            Text("Item 2"),
            Text("Item 3"),
            Text("Item 3"),
          ],
        ),
        Container(
          height: 150,
          color: Colors.red,
          child: Text("Contain"),
          alignment: Alignment.center,
        ),
      ],
    );
  }
}

class MyMaterialApp extends StatelessWidget {
  const MyMaterialApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "RS II Material App",
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LoginPage(),
    );
  }
}

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Login"),
      ),
      body: Center(
        child: Container(
          constraints: BoxConstraints(maxWidth: 400, maxHeight: 400),
          child: Card(
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(children: [
                Image.network(
                  "https://www.fit.ba/content/public/images/og-image.jpg",
                  height: 100,
                  width: 100,
                ),
                TextField(
                  decoration: InputDecoration(
                      labelText: "Username", prefixIcon: Icon(Icons.person)),
                ),
                SizedBox(
                  height: 8,
                ),
                TextField(
                  decoration: InputDecoration(
                      labelText: "Password", prefixIcon: Icon(Icons.lock)),
                ),
                SizedBox(
                  height: 8,
                ),
                ElevatedButton(
                    onPressed: () {
                      print("Login proceed");
                    },
                    child: Text("Login"))
              ]),
            ),
          ),
        ),
      ),
    );
  }
}
