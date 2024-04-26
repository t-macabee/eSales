import 'package:esales_admin/models/jedinice_mjere.dart';
import 'package:esales_admin/models/product.dart';
import 'package:esales_admin/models/search_result.dart';
import 'package:esales_admin/models/vrste_proizvoda.dart';
import 'package:esales_admin/providers/jedinice_mjere_provider.dart';
import 'package:esales_admin/providers/product_provider.dart';
import 'package:esales_admin/providers/vrste_proizvoda_provider.dart';
import 'package:esales_admin/widgets/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';

class ProductDetailScreen extends StatefulWidget {
  Product? product;
  ProductDetailScreen({Key? key, this.product}) : super(key: key);

  @override
  State<ProductDetailScreen> createState() => _ProductDetailScreenState();
}

class _ProductDetailScreenState extends State<ProductDetailScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};

  late JediniceMjereProvider _jediniceMjereProvider;
  late VrsteProizvodaProvider _vrsteProizvodaProvider;
  late ProductProvider _productProvider;

  SearchResult<JediniceMjere>? jediniceMjereResult;
  SearchResult<VrsteProizvoda>? vrsteProizvodaResult;

  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'sifra': widget.product?.sifra,
      'naziv': widget.product?.naziv,
      'cijena': widget.product?.cijena.toString(),
      'vrstaId': widget.product?.vrstaId?.toString(),
      'jedinicaMjereId': widget.product?.jedinicaMjereId?.toString()
    };

    _jediniceMjereProvider = context.read<JediniceMjereProvider>();
    _vrsteProizvodaProvider = context.read<VrsteProizvodaProvider>();
    _productProvider = context.read<ProductProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();

    /*
    if (widget.product != null) {
      setState(() {
        _formKey.currentState?.patchValue({'sifra': widget.product?.sifra});
      });
    }
    */
  }

  Future initForm() async {
    jediniceMjereResult = await _jediniceMjereProvider.get();
    print(jediniceMjereResult);

    vrsteProizvodaResult = await _vrsteProizvodaProvider.get();
    print(jediniceMjereResult);

    setState(() {
      isLoading = false;
    }); //!
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
        title: widget.product?.naziv ?? "Product details",
        child: Column(
          children: [
            isLoading ? Container() : _buildForm(),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Padding(
                    padding: const EdgeInsets.all(10),
                    child: ElevatedButton(
                        onPressed: () async {
                          _formKey.currentState?.saveAndValidate();
                          print(_formKey.currentState?.value);
                          print(_formKey.currentState?.value['naziv']);

                          try {
                            if (widget.product == null) {
                              await _productProvider
                                  .insert(_formKey.currentState?.value);
                            } else {
                              await _productProvider.update(
                                  widget.product!.proizvodId!,
                                  _formKey.currentState?.value);
                            }
                          } on Exception catch (ex) {
                            showDialog(
                                context: context,
                                builder: (BuildContext context) => AlertDialog(
                                      title: Text("Error"),
                                      content: Text(ex.toString()),
                                      actions: [
                                        TextButton(
                                            onPressed: () =>
                                                Navigator.pop(context),
                                            child: Text("Ok"))
                                      ],
                                    ));
                          }
                        },
                        child: Text("Sačuvaj")))
              ],
            )
          ],
        ));
  }

  FormBuilder _buildForm() {
    return FormBuilder(
      key: _formKey,
      initialValue: _initialValue,
      child: Column(children: [
        Row(
          children: [
            Expanded(
              child: FormBuilderTextField(
                decoration: InputDecoration(labelText: "Šifra"),
                name: "sifra",
              ),
            ),
            SizedBox(
              width: 10,
            ),
            Expanded(
              child: FormBuilderTextField(
                decoration: InputDecoration(labelText: "Naziv"),
                name: "naziv",
              ),
            ),
          ],
        ),
        Row(
          children: [
            Expanded(
              child: FormBuilderDropdown<String>(
                name: 'vrstaId',
                decoration: InputDecoration(
                  labelText: 'Vrsta proizvoda',
                  suffix: IconButton(
                    icon: const Icon(Icons.close),
                    onPressed: () {
                      _formKey.currentState!.fields['vrstaId']?.reset();
                    },
                  ),
                  hintText: 'Select type',
                ),
                items: vrsteProizvodaResult?.result
                        .map((item) => DropdownMenuItem(
                              alignment: AlignmentDirectional.center,
                              value: item.vrstaId.toString(),
                              child: Text(item.naziv ?? ""),
                            ))
                        .toList() ??
                    [],
              ),
            ),
            SizedBox(
              width: 10,
            ),
            Expanded(
              child: FormBuilderDropdown<String>(
                name: 'jedinicaMjereId',
                decoration: InputDecoration(
                  labelText: 'Jedinica mjere',
                  suffix: IconButton(
                    icon: const Icon(Icons.close),
                    onPressed: () {
                      _formKey.currentState!.fields['jedinicaMjereId']?.reset();
                    },
                  ),
                  hintText: 'Select measure',
                ),
                items: jediniceMjereResult?.result
                        .map((item) => DropdownMenuItem(
                              alignment: AlignmentDirectional.center,
                              value: item.jedinicaMjereId.toString(),
                              child: Text(item.naziv ?? ""),
                            ))
                        .toList() ??
                    [],
              ),
            ),
            Expanded(
              child: FormBuilderTextField(
                decoration: InputDecoration(labelText: "Cijena"),
                name: "cijena",
              ),
            ),
          ],
        )
      ]),
    );
  }
}
