import 'package:esales_admin/models/product.dart';
import 'package:esales_admin/models/search_result.dart';
import 'package:esales_admin/providers/product_provider.dart';
import 'package:esales_admin/screens/product_details_screen.dart';
import 'package:esales_admin/utils/util.dart';
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
  SearchResult<Product>? result;
  TextEditingController _ftsController = TextEditingController();
  TextEditingController _sifraController = TextEditingController();

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
          children: [_buildSearch(), _buildDataListView()],
        ),
      ),
    );
  }

  Widget _buildSearch() {
    return Padding(
        padding: const EdgeInsets.all(8.0),
        child: Row(
          children: [
            Expanded(
              child: TextField(
                decoration:
                    InputDecoration(labelText: "Naziv ili šifra proizvoda"),
                controller: _ftsController,
              ),
            ),
            SizedBox(
              width: 8,
            ),
            Expanded(
              child: TextField(
                decoration: InputDecoration(labelText: "Šifra proizvoda"),
                controller: _sifraController,
              ),
            ),
            ElevatedButton(
                onPressed: () async {
                  var data = await _productProvider.get(filter: {
                    'fts': _ftsController.text,
                    'sifra': _sifraController.text
                  });
                  setState(() {
                    result = data;
                  });
                  //print("data: ${data.result[0].naziv}");
                },
                child: Text("Pretraga")),
            SizedBox(
              width: 8,
            ),
            ElevatedButton(
                onPressed: () async {
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (context) => ProductDetailScreen(
                        product: null,
                      ),
                    ),
                  );
                },
                child: Text("Dodaj"))
          ],
        ));
  }

  Widget _buildDataListView() {
    return Expanded(
        child: SingleChildScrollView(
            child: DataTable(
                columns: [
          DataColumn(
              label: const Expanded(
            child: const Text(
              'ID',
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          )),
          DataColumn(
              label: const Expanded(
            child: const Text(
              'Sifra',
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          )),
          DataColumn(
              label: const Expanded(
            child: const Text(
              'Naziv',
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          )),
          DataColumn(
              label: const Expanded(
            child: const Text(
              'Cijena',
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          )),
          DataColumn(
              label: const Expanded(
            child: const Text(
              'Slika',
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          )),
        ],
                rows: result?.result
                        .map((Product e) => DataRow(
                                onSelectChanged: (selected) => {
                                      if (selected == true)
                                        {
                                          Navigator.of(context).push(
                                              MaterialPageRoute(
                                                  builder: (context) =>
                                                      ProductDetailScreen(
                                                          product: e)))
                                        }
                                    },
                                cells: [
                                  DataCell(
                                      Text(e.proizvodId?.toString() ?? "")),
                                  DataCell(Text(e.sifra ?? "")),
                                  DataCell(Text(e.naziv ?? "")),
                                  DataCell(Text(formatNumber(e.cijena))),
                                  DataCell(e.slika != ""
                                      ? Container(
                                          width: 100,
                                          height: 100,
                                          child:
                                              imageFromBase64String(e.slika!),
                                        )
                                      : Text(""))
                                ]))
                        .toList() ??
                    [])));
  }
}
