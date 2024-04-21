import 'package:json_annotation/json_annotation.dart';

part 'product.g.dart';

@JsonSerializable()
class Product {
  int? proizvodId;
  String? naziv;
  String? sifra;
  double? cijena;
  String? slika;

  Product(this.proizvodId, this.naziv, this.sifra, this.cijena, this.slika);

  factory Product.fromJson(Map<String, dynamic> json) =>
      _$ProductFromJson(json);

  Map<String, dynamic> toJson() => _$ProductToJson(this);
}

/*{
  "cijena": 0,
  "vrstaId": 0,
  "jedinicaMjereId": 0,  
  "slikaThumb": "string",
  "status": true,
  "stateMachine": "string"
}*/