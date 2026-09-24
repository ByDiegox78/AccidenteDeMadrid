using System.Diagnostics;
using AccidenteDeMadrid.Enums;
using AccidenteDeMadrid.Models;
using AccidenteDeMadrid.Repository;
using Microsoft.Data.Analysis;
using static System.Console;

namespace AccidenteDeMadrid.Service;

public class AccidenteService() : IAccidenteService {
    private static readonly string[] DiasSemana = { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
    public void ConsultasLinq(IEnumerable<Accidente> accidentes) {
        var sw= Stopwatch.StartNew();

        WriteLine("1: Total de accidentes");
        var todos = accidentes.Count();
        WriteLine($"Total de total de accidentes: {todos}");
        
        WriteLine("2: Top 5 de accidentes por distrito");
        var top = accidentes
            .GroupBy(a => a.Distrito)
            .Select(a => new { b = a.Key, c = a.Count() })
            .OrderByDescending(a => a.c)
            .Take(5);
        foreach (var item in top) {
            WriteLine($"Distrito: {item.b} Accidente: {item.c}");
        }
        
        WriteLine("3: Accidente por tipos");
        var tipo = accidentes
            .GroupBy(a => a.TipoAccidente)
            .Select(g => new { Tipo = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in tipo) {
            WriteLine($"{item.Tipo}: {item.Total}");
        }
        
        WriteLine("4: Accidente por estado meteorologico");
        var accidentePorEstado = accidentes
            .GroupBy(a => a.EstadoMetereologico)
            .Select(g => new { Tipo = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in accidentePorEstado) {
            WriteLine($"{item.Tipo}: {item.Total}");
        }
        
        WriteLine("5: Accidente por sexo");
        var sexo = accidentes
            .GroupBy(a => a.Sexo)
            .Select(g => new { Tipo = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in sexo) {
            WriteLine($"{item.Tipo}: {item.Total}");
        }
        
        
        WriteLine("6: Accidente por rango de edad");
        var rango = accidentes
            .GroupBy(a => a.RangoEdad)
            .Select(g => new {
                RangoEdad = g.Key,
                Total = g.Count()
            }).OrderByDescending(x => x.Total);;
        foreach (var item in rango) { 
            WriteLine($"{item.RangoEdad}: {item.Total} accidentes");
        }
        
        WriteLine("7: Positivo en Alcohol");
        var positivoAlcohol = accidentes
            .Where(a => a.PositivoAlcohol)
            .Count();
        WriteLine($"Cantidad de positivos en alcohol: {positivoAlcohol}");
        
        WriteLine("8: Positivo en Drogas");
        var positivoDrogas = accidentes
            .Where(a => a.PositivoDroga)
            .Count();
        WriteLine($"Cantidad de positivos en alcohol: {positivoDrogas}");
        
        
        WriteLine("9: Accidente por dia de la semana");
        var dia = accidentes
            .GroupBy(a => a.Fecha.DayOfWeek)
            .Select(g => new { Dia = g.Key, Total = g.Count() });
        foreach (var item in dia) WriteLine($"{item.Dia}- {item.Total}");
        
        
        WriteLine("10: Accidente por mes");
        var mes = accidentes
            .GroupBy(a => a.Fecha.Month)
            .Select(g => new { Mes = g.Key, Total = g.Count() })
            .OrderBy(x => x.Mes);
        foreach (var item in mes) WriteLine($"{item.Mes}- {item.Total}");
        
        
        WriteLine("11: Hora con mas accidentes");
        var hora = accidentes
            .GroupBy(a => a.Hora.Hour)
            .Select(g => new { Hora = g.Key, Total = g.Count()})
            .OrderByDescending(x => x.Total)
            .First();
        WriteLine($"La hora con más accidentes es de {hora.Hora}:00 con {hora.Total} accidentes.");
        
        WriteLine("12 Lesiones mas frecuentes");
        var lesion = accidentes
            .GroupBy(a => a.Lesividad)
            .Select(g => new { Lesion = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in lesion) {
            WriteLine($"{item.Lesion}: {item.Total} accidentes");
        }
        
        WriteLine("13: Tipo de vehiculo mas implicado");
        var tipoAccidente = accidentes
            .GroupBy(a => a.TipoVehiculo)
            .Select(g => new { TipoVehiculo = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .First();
        WriteLine($"Tipo de vehículo con más accidentes: {tipoAccidente.TipoVehiculo} ({tipoAccidente.Total} accidentes)");
        
        WriteLine("14: Accidentes con peatones");
        var peatones = accidentes
            .Count(a => a.TipoPersona == "Peatón");
        WriteLine($"Total de accidentes con peatones: {peatones}");
        
        WriteLine("15: Proporcion Hombre/Mujer");
        var proporcionHombreMujer =accidentes
            .GroupBy(a => a.Sexo)
            .Select(g => new {
                Sexo = g.Key,
                Total = g.Count(),
            })
            .OrderByDescending(x => x.Total);
        foreach (var item in proporcionHombreMujer) {
            WriteLine($"{item.Sexo}: {item.Total}");
        }
        
        WriteLine("16: Distrito con mas peatones");
        var distrito = accidentes
            .Where(a => a.TipoPersona == "Peatón")
            .GroupBy(a => a.Distrito)
            .Select(g => new { Distrito = g.Key, Total = g.Count() })
            .OrderByDescending(a => a.Total)
            .First();
        WriteLine($"{distrito.Distrito} : {distrito.Total} atropellos");
        
        WriteLine("17: Fin de semana vs entre semana");
        var comparativa = accidentes
            .GroupBy(a =>
                a.Fecha.DayOfWeek == DayOfWeek.Saturday ||
                a.Fecha.DayOfWeek == DayOfWeek.Sunday
                    ? "Fin de semana"
                    : "Entre semana")
            .Select(a => new { tipo = a.Key, Acc = a.Count() });
        foreach (var com in comparativa) {
            WriteLine($"{com.tipo}: {com.Acc}");
        }
        
        WriteLine("18: Media de accidentes por dia");
        var media = accidentes
            .GroupBy(a => a.Fecha)
            .Average(g => g.Count());
        WriteLine($"Media de accidentes por día: {media}");
        
        WriteLine("19: Accidentes por alcohol y drogra");
        var conjunto = accidentes
            .Count(a => a.PositivoAlcohol && a.PositivoDroga);
        WriteLine($"Total de accidentes con alcohol y droga: {conjunto}");
        
        WriteLine("20: Rangos de edad mas vulnerables");
        var rangos = accidentes
            .Where(a => a.TipoPersona == "Peatón")
            .GroupBy(a => a.RangoEdad)
            .Select(g => new { RangoEdad = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in rangos) {
            WriteLine($"{item.RangoEdad} : {item.Total} atropellos");
        }
        WriteLine("21: Distritos con mas positivos en alcohol");
        var distritosAlcohol =  accidentes
            .Where(a => a.PositivoAlcohol) 
            .GroupBy(a => a.Distrito)
            .Select(g => new { Distrito = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in distritosAlcohol) {
            WriteLine($"{item.Distrito} : {item.Total} a kaso");
        }
        WriteLine("22:Accidentes por codigo de distrito");
        var codigo = accidentes
            .GroupBy(a => a.CodigoDistrito)
            .Select(g => new { Codigo = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in codigo.OrderBy(x => x.Codigo)) {
            WriteLine($"Distrito {item.Codigo} : {item.Total} accidentes");
        }
        
        WriteLine("23: Accidentes por año");
        var año =  accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new { Fecha = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in año) {
            WriteLine($"Año {item.Fecha} : {item.Total} accidentes");
        }
        WriteLine("24: Evolucion mensual por año");
        var evolucionMensual = accidentes
            .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
            .Select(g => new { Año = g.Key.Year, Mes = g.Key.Month, Total = g.Count() })
            .OrderBy(x => x.Año)
            .ThenBy(x => x.Mes);
        foreach (var item in evolucionMensual) {
            WriteLine($"{item.Año} (Mes {item.Mes,2}) : {item.Total,5} accidentes");
        }
        
        WriteLine("25: Distrito con mas accidentes por año");
        var distritosAño = accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new {
                Año = g.Key,
                TopDistrito = g
                    .GroupBy(a => a.Distrito)
                    .Select(gDist => new { Distrito = gDist.Key, Total = gDist.Count() })
                    .OrderByDescending(d => d.Total)
                    .First()
            })
            .OrderBy(x => x.Año);
        foreach (var item in distritosAño) { 
            WriteLine($"Año {item.Año} -> {item.TopDistrito.Distrito} con {item.TopDistrito.Total} accidentes");
        }
        
        WriteLine("26: Tendencia de Alcohol por año");
        var tendencia = accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new { Año = g.Key, PositivosAlcohol = g.Count(a => a.PositivoAlcohol), TotalAccidentes = g.Count()})
            .OrderBy(x => x.Año);
        foreach (var item in tendencia) {
            WriteLine($"Año {item.Año}: {item.PositivosAlcohol,5} positivos de {item.TotalAccidentes,6})");
        }
        
        WriteLine("27: Comparativa fin de semana vs entre semana por año");
        var compSemanaAño = accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new {
                Año = g.Key,
                FinDeSemana = g.Count(a => a.Fecha.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday),
                EntreSemana = g.Count(a => a.Fecha.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)),
                Total = g.Count()
            })
            .OrderBy(x => x.Año);
        foreach (var com in compSemanaAño) {
            WriteLine($"{com.Año}: " + $"Fin de semana: {com.FinDeSemana} | " + $"Entre semana: {com.EntreSemana}");
        }
        
        WriteLine("28: Hora pico por año");
        var pico = accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(h => new {
                Año = h.Key,
                HoraPico = h
                    .GroupBy(a => a.Hora.Hour)
                    .Select(k => new { Hora = k.Key, Total = k.Count() })
                    .OrderByDescending(x => x.Total)
                    .First()
            })
            .OrderBy(x => x.Año);
        foreach (var item in pico) {
            WriteLine($"Año {item.Año} -> Hora pico: {item.HoraPico.Hora}:00 h ({item.HoraPico.Total} accidentes)");
        }
        WriteLine("29: Lesión más frecuente por año");
        var lesionPorAño = accidentes
            .GroupBy(a => a.Fecha.Year)
            .Select(k => new {
                Año = k.Key,
                Lesion = k.GroupBy(l => l.Lesividad).OrderByDescending(l => l.Count())
                    .First()
            })
            .OrderBy(x => x.Año);
        foreach (var item in lesionPorAño) {
            WriteLine($"Año {item.Año} -> {item.Lesion.Key} personas)");
        }
        WriteLine("30: Evolución de peatones por año");
        var peatonesPorAño = accidentes
            .Where(a => a.TipoPersona == "Peatón")
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new {
                Año = g.Key,
                Total = g.Count()
            })
            .OrderBy(x => x.Año);
        foreach (var item in peatonesPorAño) {
            WriteLine($"Año {item.Año} -> {item.Total} peatones");
        }
        sw.Stop();

        WriteLine("=======================================================");
        WriteLine($"Tiempo LINQ: {sw.Elapsed.TotalSeconds:F2} segundos");
        WriteLine("=======================================================");

        
    }
    public void ConsultasDataFrame(IEnumerable<Accidente> datos) {
        var df = ConstruirDataFrame(datos);
        var sw= Stopwatch.StartNew();
        WriteLine("1: Total de accidentes");
        var total = df.Rows.Count;
        WriteLine($"Total de accidentes: {total}");
        
        WriteLine("2: Top 5 de accidentes por distrito");
        var distrito2 = df
            .GroupBy("Distrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente")
            .Head(5);
        WriteLine(distrito2);
        
        WriteLine("3: Accidente por tipos");
        var tipo = df
            .GroupBy("TipoAccidente")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(tipo);
        
        WriteLine("4: Accidente por estado meteorologico");
        var meteo = df
            .GroupBy("EstadoMeteorologico")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(meteo);
        
        WriteLine("5: Accidente por sexo");
        var sexo = df
            .GroupBy("Sexo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(sexo);
        
        WriteLine("6: Accidente por rango de edad");
        var rango = df
            .GroupBy("RangoEdad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(rango);
        
        WriteLine("7: Positivo en Alcohol");
        var row = (BooleanDataFrameColumn)df.Columns["PositivoAlcohol"];
        var cantidad = row.Count(row => row == true);
        WriteLine(cantidad);
        
        WriteLine("8: Positivo en Drogas");
        var row2 = (BooleanDataFrameColumn)df.Columns["PositivoDroga"];
        var cantidad2 = row2.Count(row => row == true);
        WriteLine(cantidad2);
        
        WriteLine("9: Accidente por dia de la semana");
        var diaSemana = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).DayOfWeek)
            .Select(g => new {
                Dia = g.Key,
                Total = g.Count()
            });
        foreach (var item in diaSemana) {
            WriteLine($"{item.Dia}: {item.Total}");
        }
        
        WriteLine("10: Accidente por mes");
        var accidenteMes = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Month)
            .Select(g => new {
                Mes = g.Key,
                Total = g.Count()
            }).OrderBy(x => x.Mes);
        foreach (var item in accidenteMes) {
            WriteLine($"{item.Mes}: {item.Total}");
        }
        
        WriteLine("11: Hora con mas accidentes");
        var first = df.Rows
            .GroupBy(row => ((TimeOnly)row[df.Columns.IndexOf("Hora")]).Hour)
            .Select(g => new {
                Hora = g.Key,
                Total = g.Count()
            })
            .OrderByDescending(x => x.Total)
            .First();

        WriteLine($"Hora: {first.Hora}:00 | Accidentes: {first.Total}");
        
        WriteLine("12 Lesiones mas frecuentes");
        var lesion = df
            .GroupBy("Lesividad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(lesion);
        
        WriteLine("13: Tipo de vehiculo mas implicado");
        var tipoAccidente = df
            .GroupBy("TipoVehiculo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente")
            .Head(1);
        WriteLine(tipoAccidente);
        
        WriteLine("14: Accidentes con peatones");
                var columna = (StringDataFrameColumn)df["TipoPersona"];
                var filter = df
                    .Filter(columna.ElementwiseEquals("Peatón"));
                WriteLine($"Total de accidentes con peatones: {filter.Rows.Count}");
                
        WriteLine("15: Proporcion Hombre/Mujer");
        var proporcionHombreMujer =df
            .GroupBy("Sexo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        foreach (var i in proporcionHombreMujer.Rows)
            WriteLine($"{i[0]}: {i[1]}");
        
        WriteLine("16: Distrito con mas peatones");
        var persona16 = (StringDataFrameColumn)df["TipoPersona"];
        var peatones16 = df.Filter(persona16.ElementwiseEquals("Peatón"));
        var distrito = peatones16
            .GroupBy("Distrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente")
            .Head(1);
        WriteLine(distrito);
        
        
        WriteLine("17: Fin de semana vs entre semana");
        var comparativa = df.Rows
            .GroupBy(row =>
                ((DateOnly)row[df.Columns.IndexOf("Fecha")]).DayOfWeek
                is DayOfWeek.Saturday or DayOfWeek.Sunday ? "Fin de semana" : "Entre semana")
            .Select(g => new {
                Tipo = g.Key,
                Total = g.Count()
            });
        foreach (var item in comparativa)
            WriteLine($"{item.Tipo}: {item.Total}");
        
        WriteLine("18: Media de accidentes por dia");
        var fechas18 = (PrimitiveDataFrameColumn<DateOnly>)df["Fecha"];
        var dias18 = fechas18
            .ToList()
            .Distinct()
            .Count();
        var media18 = (double)df.Rows.Count / dias18;

        WriteLine($"Media de accidentes por día: {media18:F2}");
        
        WriteLine("19: Accidentes por alcohol y drogra");
        var alcohol = ((BooleanDataFrameColumn)df["PositivoAlcohol"])
            .ElementwiseEquals(true);
        var droga = ((BooleanDataFrameColumn)df["PositivoDroga"])
            .ElementwiseEquals(true);
        var conjunto = df.Filter(alcohol & droga);
        WriteLine($"Total de accidentes con alcohol y droga: {conjunto.Rows.Count}");
        
        
        WriteLine("20: Rangos de edad mas vulnerables");
        var persona = (StringDataFrameColumn)df["TipoPersona"];
        var peatones = df.Filter(
            persona.ElementwiseEquals("Peatón")
        );
        var rangos = peatones
            .GroupBy("RangoEdad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(rangos);

        
        WriteLine("21: Distritos con mas positivos en alcohol");
        var positivo = ((BooleanDataFrameColumn)df["PositivoAlcohol"])
            .ElementwiseEquals(true);
        var distritosAlcohol =  df
            .Filter(positivo) 
            .GroupBy("Distrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        WriteLine(distritosAlcohol);
       
        WriteLine("22:Accidentes por codigo de distrito");
        var codigo = df
            .GroupBy("CodDistrito")
            .Count("NumExpediente")
            .OrderBy("CodDistrito");
        WriteLine(codigo);
        
        WriteLine("23: Accidentes por año");
        var fechas23 = (PrimitiveDataFrameColumn<DateOnly>)df["Fecha"];
        var años = new Dictionary<int, int>();
        foreach (var valor in fechas23) {
            var fecha = (DateOnly)valor;
            var año = fecha.Year;
            if (!años.TryAdd(año, 1))
                años[año]++;
        }
        var resultado = años
            .OrderByDescending(x => x.Value);
        foreach (var item in resultado) {
            WriteLine($"Año {item.Key} : {item.Value} accidentes");
        }
        
        
        WriteLine("24: Evolucion mensual por año");
        var evoMen = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                Mes = g
                    .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Month)
                    .Select(g => new {
                        Mes = g.Key,
                        Total = g.Count()
                    }).OrderBy(x => x.Mes)
            }).OrderBy(x => x.Año);
        foreach (var i in evoMen) {
            WriteLine($"Año: {i.Año}");
            foreach (var z in i.Mes) {
                WriteLine($"  Mes: {z.Mes} | Accidentes: {z.Total}");
            }
            WriteLine();
        }
        WriteLine("25: Distrito con mas accidentes por año");
        var accPerDis = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                Distrito = g
                    .GroupBy(row => row[df.Columns.IndexOf("Distrito")])
                    .Select(gDis => new {
                        Distrito = gDis.Key,
                        Total = gDis.Count()
                    })
                    .OrderByDescending(d => d.Total)
                    .First()
            }).OrderBy(x => x.Año);
        foreach (var i in accPerDis) {
            WriteLine(
                $"Año: {i.Año} | " +
                $"Distrito: {i.Distrito} | " +
                $"Accidentes: {i.Distrito.Total}"
            );
        }
        WriteLine("26: Tendencia de Alcohol por año");
        var tendenciaDf = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                PositivoAlcohol = g.Count(row => (bool)row[df.Columns.IndexOf("PositivoAlcohol")] == true),
                TotalAccidente = g.Count()
            })
            .OrderBy(x => x.Año);
        foreach (var i in tendenciaDf) {
            WriteLine(
                $"Año: {i.Año} | " +
                $"Positivos alcohol: {i.PositivoAlcohol} | " +
                $"Total accidentes: {i.TotalAccidente}"
            );
        }
        WriteLine("27: Comparativa fin de semana vs entre semana por año");
        var compAño = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                FinDeSemana = g.Count(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday),
                EntreSemana = g.Count(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday))
            })
            .OrderBy(x => x.Año);
        foreach (var i in compAño) {
            WriteLine(
                $"Año: {i.Año} | " +
                $"Fin de semana: {i.FinDeSemana} | " +
                $"Entre semana: {i.EntreSemana}"
            );
        }
        WriteLine("28: Hora pico por año");
        var horaPico = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                HoraPico = g
                    .GroupBy(row => ((TimeOnly)row[df.Columns.IndexOf("Hora")]).Hour)
                    .Select(h => new {
                        Hora = h.Key,
                        Total = h.Count()
                    })
                    .OrderByDescending(x => x.Total)
                    .First()
            })
            .OrderBy(x => x.Año);

        foreach (var i in horaPico) {
            WriteLine(
                $"Año: {i.Año} | " +
                $"Hora pico: {i.HoraPico.Hora}:00 | " +
                $"Accidentes: {i.HoraPico.Total}"
            );
        }
        
        WriteLine("29: Lesión más frecuente por año");
        var frecuAño = df.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                Lesion = g
                    .GroupBy(row => (row[df.Columns.IndexOf("Lesividad")]))
                    .Select(g => new {
                        Tipo = g.Key,
                        total = g.Count()
                    })
                    .OrderByDescending(x => x.total)
                    .First()
            });
        foreach (var i in frecuAño) {
            WriteLine(
                $"Año: {i.Año} | " +
                $"Lesión: {i.Lesion.Tipo} | " +
                $"Total: {i.Lesion.total}"
            );
        }
        
        WriteLine("30: Evolución de peatones por año");
        var pers = (StringDataFrameColumn)df["TipoPersona"];
        var peat = df.Filter(pers.ElementwiseEquals("Peatón"));
        var evolucion = peat.Rows
            .GroupBy(row => ((DateOnly)row[df.Columns.IndexOf("Fecha")]).Year)
            .Select(g => new {
                Año = g.Key,
                Total = g.Count()
            });
        foreach (var año in evolucion) {
            WriteLine($"Año: {año.Año} | Peatones: {año.Total}");
        }
        sw.Stop();

        WriteLine("=======================================================");
        WriteLine($"Tiempo LINQ: {sw.Elapsed.TotalSeconds:F2} segundos");
        WriteLine("=======================================================");
    }
    private DataFrame ConstruirDataFrame(IEnumerable<Accidente> datos) {
        var lista = datos.ToList();
        return new DataFrame(
            new StringDataFrameColumn("NumExpediente", lista.Select(a => a.NumExpediente)),
            new PrimitiveDataFrameColumn<DateOnly>("Fecha", lista.Select(a => a.Fecha) ),            
            new PrimitiveDataFrameColumn<TimeOnly>("Hora", lista.Select(a => a.Hora) ),            
            new StringDataFrameColumn("Localizacion", lista.Select(a => a.Localizacion ?? string.Empty)),
            new PrimitiveDataFrameColumn<int>("CodDistrito", lista.Select(a => a.CodigoDistrito)),
            new StringDataFrameColumn("Distrito", lista.Select(a => a.Distrito)),
            new StringDataFrameColumn("TipoAccidente", lista.Select(a => a.TipoAccidente.ToString())),
            new StringDataFrameColumn("EstadoMeteorologico", lista.Select(a => a.EstadoMetereologico)),
            new StringDataFrameColumn("TipoVehiculo", lista.Select(a => a.TipoVehiculo)),
            new StringDataFrameColumn("TipoPersona", lista.Select(a => a.TipoPersona)),
            new StringDataFrameColumn("RangoEdad", lista.Select(a => a.RangoEdad)),
            new StringDataFrameColumn("Sexo", lista.Select(a => a.Sexo.ToString())),
            new PrimitiveDataFrameColumn<int>("CodLesividad", lista.Select(a => a.CodLesividad)),
            new StringDataFrameColumn("Lesividad", lista.Select(a => a.Lesividad)),
            new PrimitiveDataFrameColumn<double>("CoordenadaXUtm", lista.Select(a => a.CoordenadaXUtm)),
            new PrimitiveDataFrameColumn<double>("CoordenadaYUtm", lista.Select(a => a.CoordenadaYUtm)),
            new BooleanDataFrameColumn("PositivoAlcohol", lista.Select(a => a.PositivoAlcohol)),
            new BooleanDataFrameColumn("PositivoDroga", lista.Select(a => a.PositivoDroga))
        );
    }
}