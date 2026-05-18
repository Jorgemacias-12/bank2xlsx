# Project improvements — Bank2xlsx

Resumen
- Herramienta útil para convertir exportaciones TXT bancarias mal formateadas a tablas Excel. Arquitectura modular con parsers y exportador; documentación y licencia añadidas.

Puntos fuertes
- Idea con valor práctico claro.
- Parsers separados (header/transaction/footer) y exportador modular.
- Soporte actual para ClosedXML y una implementación OpenXML/streaming en código.

Problemas y riesgos
- Parsers frágiles: asumen índices y formatos rígidos (p. ej. `lines[7]`, recortes por cantidad fija de líneas).
- Errores de compilación en `FooterParser` (inicializadores `[]`, uso de tokens). Requiere corrección inmediata.
- Escasa validación y manejo de errores (índices fuera de rango, archivos malformados).
- Normalización de fechas/números no robusta (falta `CultureInfo` y parsing seguro).
- Riesgo de alto consumo de memoria con ClosedXML en archivos grandes.
- Falta de pruebas automatizadas y ejemplos de entrada/salida.

Recomendaciones priorizadas
1. Corregir `FooterParser` y asegurar que el proyecto compile.
2. Añadir pruebas unitarias básicas para `HeaderParser`, `TransactionParser` y `FooterParser` con ejemplos representativos (anonimizados).
3. Hacer los parsers más tolerantes: detectar patrones en lugar de usar índices fijos; exponer parámetros (delimitador, líneas de encabezado ignoradas).
4. Normalizar parsing de fechas y montos usando `DateTime.TryParseExact` / `decimal.TryParse` con `CultureInfo` adecuados.
5. Habilitar y promocionar el exportador streaming (OpenXML fila-a-fila) para archivos grandes; hacerlo opción por defecto o configurable desde la UI.
6. Añadir logging y manejo de excepciones en `FileParserService` y la UI para feedback al usuario.
7. Incluir ejemplos de entrada/salida y añadir un job de pruebas en CI.

Siguientes pasos sugeridos (puedo implementar)
- Corregir `FooterParser` y validar que el proyecto compila.
- Implementar parsing cultural seguro para fechas y números.
- Añadir pruebas unitarias mínimas y ejemplos de entrada.

Si quieres, empiezo corrigiendo `FooterParser` y añadiendo pruebas básicas.  

---

Archivo generado automáticamente con observaciones e ideas de mejora.