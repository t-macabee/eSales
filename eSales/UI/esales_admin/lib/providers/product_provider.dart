import 'package:esales_admin/models/product.dart';
import 'package:esales_admin/providers/base_provider.dart';

class ProductProvider extends BaseProvider<Product> {
  ProductProvider() : super("Proizvodi");

  @override
  Product fromJson(data) {
    // TODO: implement fromJson
    return Product.fromJson(data);
  }
}
