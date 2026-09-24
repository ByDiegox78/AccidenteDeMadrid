# Práctica 5: Análisis de Accidentes de Madrid

## Objetivo

En esta práctica analizo los datos de accidentes de tráfico de Madrid de los años 2024, 2025 y 2026 utilizando **LINQ** y **DataFrames**.

Mi objetivo es realizar 30 consultas sobre los datos, comparar ambos métodos y analizar su rendimiento sobre más de 100.000 registros.

## Datos

Utilizo los siguientes ficheros:

```text
Data/
├── 2024_Accidentalidad.csv
├── 2025_Accidentalidad.csv
└── 2026_Accidentalidad.csv
```

Los datos proceden del portal de datos abiertos del Ayuntamiento de Madrid:

https://datos.madrid.es/dataset/300228-0-accidentes-trafico-detalle/information

## Tecnologías

* **C# 14 / .NET 10**
* **CsvHelper** para leer los ficheros CSV.
* **LINQ** para realizar las consultas sobre los accidentes.
* **Microsoft.Data.Analysis** para trabajar con DataFrames.
* **Task.WhenAll** para realizar la lectura concurrente de los ficheros.

## Estructura del proyecto

He separado el proyecto en diferentes responsabilidades:

* `Models`: contiene el modelo `Accidente`.
* `Mappers`: contiene la configuración utilizada por CsvHelper.
* `Repository`: se encarga de leer los ficheros CSV.
* `Service`: contiene las consultas LINQ y DataFrame.
* `Data`: contiene los ficheros CSV.

He realizado esta separación para evitar mezclar la lectura de datos con la lógica de análisis.

## Lectura de datos

He utilizado un `Repository` para separar el acceso a los ficheros del resto de la aplicación.

Los tres CSV son independientes, por lo que he decidido leerlos de forma concurrente utilizando `Task.WhenAll`:

```csharp
var tareas = archivos.Select(LeerAsync);
var resultados = await Task.WhenAll(tareas);
```

Después combino los resultados en una única colección mediante `SelectMany`.

He utilizado `CsvHelper` porque facilita la lectura de los CSV y la conversión de sus registros al modelo `Accidente`.

## Justificación del diseño

### LINQ

He utilizado LINQ para realizar las consultas sobre la colección de objetos `Accidente`.

He elegido este enfoque porque permite realizar filtros, agrupaciones, ordenaciones, conteos y promedios de forma sencilla y directamente sobre los objetos.

Además, los datos ya están cargados en memoria, por lo que no necesito realizar ninguna transformación adicional para comenzar las consultas.

### DataFrames

He utilizado `Microsoft.Data.Analysis` para repetir las mismas consultas utilizando una estructura orientada al tratamiento de datos tabulares.

Antes de realizar las consultas tengo que construir el `DataFrame` a partir de los objetos `Accidente`. Esto supone un coste adicional de procesamiento.

He utilizado DataFrames porque permiten trabajar con los datos organizados en columnas y son una alternativa adecuada para analizar conjuntos de datos tabulares.

### Lectura concurrente

He utilizado `Task.WhenAll` para leer los tres ficheros de forma concurrente porque son independientes entre sí.

De esta forma puedo iniciar las tres operaciones de lectura sin tener que esperar a que termine completamente un fichero antes de comenzar el siguiente.

### PLINQ

No he utilizado PLINQ para todas las consultas porque no todas las operaciones se benefician necesariamente de la paralelización.

En este caso, las consultas son tan pequeña y basicas que ma mejoria seria apenas de unos pocos milisegundos

## Comparativa de tiempos

En mi ejecución he obtenido los siguientes tiempos:

| Operación           |     Tiempo |
| ------------------- | ---------: |
| Lectura de ficheros | **1,71 s** |
| LINQ                | **0,92 s** |
| DataFrame           | **1,76 s** |



## Análisis del rendimiento

En mi prueba, LINQ ha sido más rápido que DataFrame.

LINQ tarda **0,92 segundos**, mientras que DataFrame tarda **2,29 segundos**.

Una de las razones es que LINQ trabaja directamente sobre la colección de objetos `Accidente`, mientras que DataFrame necesita construir primero su estructura de columnas.

Además, algunas consultas realizadas con DataFrame implican recorrer sus filas o realizar transformaciones adicionales, aumentando el tiempo de ejecución.

No todas las consultas tienen el mismo coste. Operaciones como `Count()` o `First()` son sencillas, mientras que consultas que utilizan `GroupBy`, `OrderBy`, `Select` o `Average` necesitan realizar un procesamiento mayor.

Por tanto, una consulta puede ser más lenta que otra dependiendo de la cantidad de datos que tenga que recorrer y de las operaciones que tenga que realizar.

## Conclusión

Con esta práctica he podido comparar LINQ y DataFrames utilizando los mismos datos y las mismas consultas.

En mis pruebas, LINQ ha obtenido un mejor tiempo de ejecución que DataFrame. La principal diferencia está en que LINQ trabaja directamente sobre los objetos cargados, mientras que DataFrame requiere una transformación previa de los datos.

También he utilizado `Task.WhenAll` para intentar mejorar la carga de los ficheros independientes y he separado la aplicación en diferentes responsabilidades para facilitar su mantenimiento.
