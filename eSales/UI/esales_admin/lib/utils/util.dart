import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class Authorization {
  static String? username;
  static String? password;
}

Image imageFromBase64String(String base64Image) {
  return Image.memory(base64Decode(base64Image));
}

String formatNumber(dynamic) {
  var format = NumberFormat('###,00');
  if (dynamic == null) {
    return "";
  }
  return format.format(dynamic);
}
