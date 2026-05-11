# Bank2xlsx

Una herramienta para convertir exportaciones de transacciones bancarias en TXT mal formateadas en tablas Excel bien estructuradas. Este repositorio nació a petición de un compañero que necesitaba transformar los archivos que descarga del banco de la empresa en una hoja de cálculo utilizable.

---

## Descripción del proyecto

Es una pequeña aplicación WPF/.NET que lee archivos exportados por bancos (habitualmente TXT desordenados), los parsea en filas y columnas, y exporta los datos a formatos compatibles con Excel. El objetivo principal es recuperar y normalizar los datos de las transacciones para que puedan analizarse en una hoja de cálculo.

## Por qué existe este proyecto

Un compañero descarga regularmente historiales de transacciones del banco de la compañía. Las exportaciones del banco son inconsistentes y con frecuencia aparecen como texto plano mal formateado. Convertir esos datos manualmente a Excel era lento y propenso a errores, por lo que se desarrolló este proyecto para automatizar la conversión y obtener un resultado tabular y limpio.

## Parsers (qué hacen)

El núcleo del proyecto son parsers específicos por formato que:

- Detectan el formato de exportación del banco y sus particularidades (delimitadores, columnas de ancho fijo, líneas de encabezado/pie).
- Normalizan fechas y campos numéricos (montos, saldos), manejando diferencias de localidad.
- Limpian caracteres extraños, unen líneas partidas de descripciones multilínea y separan campos combinados en columnas estructuradas.
- Producen una representación tabular en memoria que se entrega al exportador.

El componente `ExcelExporter` recibe la tabla limpia y genera el `.xlsx`. Los parsers están pensados para ser extensibles y facilitar la incorporación de nuevos formatos bancarios.

## Cómo funciona (alto nivel)

1. Iniciar la aplicación (interfaz WPF).
2. Cargar el archivo TXT exportado desde el banco.
3. La aplicación selecciona el parser según el contenido del archivo.
4. El parser produce un modelo tabular normalizado (filas/columnas).
5. Exportar a Excel con el exportador.


## Añadir o mejorar parsers

- Crear un parser nuevo para manejar el formato de exportación de un banco diferente.
- Enfocarse en la extracción robusta de campos, normalización de fechas/números y en limpiar descripciones partidas.
- Añadir pruebas unitarias (si es posible) para los casos límite encontrados en exportaciones reales.

## Contribuciones

Se aceptan contribuciones. Si agregas un parser, incluye archivos de ejemplo (anonimizados) y la salida esperada para que el comportamiento sea reproducible.


## Uso básico 

abrir la aplicación, cargar el TXT, el programa detectará el formato, limpiará los datos y exportará a Excel.