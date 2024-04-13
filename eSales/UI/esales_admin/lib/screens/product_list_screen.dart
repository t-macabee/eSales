import 'package:esales_admin/providers/product_provider.dart';
import 'package:esales_admin/screens/product_details_screen.dart';
import 'package:esales_admin/widgets/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ProductListScreen extends StatefulWidget {
  const ProductListScreen({super.key});

  @override
  State<ProductListScreen> createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen> {
  late ProductProvider _productProvider;

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    _productProvider = context.read<ProductProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title_widget: Text("Product list"),
      child: Container(
        child: Column(
          children: [
            Text("Test"),
            SizedBox(
              height: 8,
            ),
            ElevatedButton(
                onPressed: () async {
                  print("Login successful");
                  var data = await _productProvider.get();
                  print("Data: ${data['result'][0]['naziv']}");
                },
                child: Text("Login"))
          ],
        ),
      ),
    );
  }
}
