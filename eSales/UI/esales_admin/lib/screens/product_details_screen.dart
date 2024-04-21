import 'package:esales_admin/models/product.dart';
import 'package:esales_admin/widgets/master_screen.dart';
import 'package:flutter/material.dart';

class ProductDetailScreen extends StatefulWidget {
  Product? product;
  ProductDetailScreen({Key? key, this.product}) : super(key: key);

  @override
  State<ProductDetailScreen> createState() => _ProductDetailScreenState();
}

class _ProductDetailScreenState extends State<ProductDetailScreen> {
  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: widget.product?.naziv ?? "Product details",
      child: Text("Details"),
    );
  }
}
