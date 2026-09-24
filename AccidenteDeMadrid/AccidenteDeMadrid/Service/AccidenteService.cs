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
        var crono = new Stopwatch();
        crono.Start();

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
            .GroupBy(a => a.TipoAccidente)
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
            });
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
            .Select(g => new { TipoAccidente = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total);
        foreach (var item in lesion) {
            WriteLine($"{item.TipoAccidente} : {item.Total} accidentes");
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
            .Where(a => a.TipoPersona == "Peatón");
        foreach (var item in peatones.Take(5)) {
            WriteLine($"Expediente: {item.NumExpediente} | Fecha: {item.Fecha:dd/MM/yyyy} {item.Hora} | Distrito: {item.Distrito} | Lesión: {item.Lesividad}");
        }
        
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
            .OrderByDescending(a => a.Total);
        foreach (var item in distrito) {
            WriteLine($"{item.Distrito} : {item.Total} atropellos");
        }
        WriteLine("17: Fin de semana vs entre semana");
        
        WriteLine("18: Media de accidentes por dia");
        var media = accidentes
            .GroupBy(a => a.Fecha.Day)
            .Average(g => g.Count());
        WriteLine($"Media de accidentes por día: {media}");
        
        WriteLine("19: Accidentes por alcohol y drogra");
        var conjunto = accidentes
            .Where(a => a.PositivoAlcohol && a.PositivoDroga);
        foreach (var item in conjunto) {
            WriteLine($"Expediente: {item.NumExpediente} | Fecha: {item.Fecha:dd/MM/yyyy} {item.Hora} | Distrito: {item.Distrito} | Tipo: {item.TipoAccidente}");
        }
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
            .GroupBy(a => a.Fecha.Year)
            .Select(g => new {
                Año = g.Key,
                TotalPeatones = g.Count(a => a.TipoPersona == "Peatón"),
                TotalGeneral = g.Count()
            })
            .OrderBy(x => x.Año);

        foreach (var item in peatonesPorAño) {
            WriteLine($"Año {item.Año} -> {item.TotalPeatones} peatones de {item.TotalGeneral} implicados");
        }
        
    }
    public void ConsultasDataFrame(IEnumerable<Accidente> datos) {
        var df = ConstruirDataFrame(datos);
        WriteLine("1: Total de accidentes");
        var total = df.Rows.Count;
        WriteLine($"Total de accidentes: {total}");
        
        WriteLine("2: Top 5 de accidentes por distrito");
        var distrito2 = df
            .GroupBy("Distrito")
            .Count("numExpediente")
            .OrderByDescending("numExpediente")
            .Head(5);
        
        WriteLine("3: Accidente por tipos");
        var tipo = df
            .GroupBy("TipoAccidente")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        WriteLine("4: Accidente por estado meteorologico");
        var meteo = df
            .GroupBy("EstadoMeteorologico")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        WriteLine("5: Accidente por sexo");
        var sexo = df
            .GroupBy("Sexo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        WriteLine("6: Accidente por rango de edad");
        var rango = df
            .GroupBy("RangoEdad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        WriteLine("7: Positivo en Alcohol");
        var row = (BooleanDataFrameColumn)df.Columns["PositivoAlcohol"];
        var cantidad = row.Count(row => row == true);
        
        WriteLine("8: Positivo en Drogas");
        var row2 = (BooleanDataFrameColumn)df.Columns["PositivoDroga"];
        var cantidad2 = row2.Count(row => row == true);
        
        WriteLine("9: Accidente por dia de la semana");
        
        
        
        WriteLine("10: Accidente por mes");
       
        
        
        WriteLine("11: Hora con mas accidentes");
        
        WriteLine("12 Lesiones mas frecuentes");
        var lesion = df
            .GroupBy("Lesividad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        
        WriteLine("13: Tipo de vehiculo mas implicado");
        var tipoAccidente = df
            .GroupBy("TipoVehiculo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente")
            .Head(1);
        
        WriteLine("14: Accidentes con peatones");
        var columna = (StringDataFrameColumn)df["TipoPersona"];
        var filtrados = df.Filter(
            columna.ElementwiseEquals("Peatón")
        );
        
        WriteLine("15: Proporcion Hombre/Mujer");
        var proporcionHombreMujer =df
            .GroupBy("Sexo")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        WriteLine("16: Distrito con mas peatones");
        var persona16 = (StringDataFrameColumn)df["TipoPersona"];
        var peatones16 = df.Filter(persona16.ElementwiseEquals("Peatón"));
        var distrito = peatones16
            .GroupBy("Distrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente")
            .Head(1);
        
        
        WriteLine("17: Fin de semana vs entre semana");
        var fechas = (PrimitiveDataFrameColumn<DateOnly>)df["Fecha"];
        var dias = fechas.ToList().Distinct().Count();
        var media = (double)df.Rows.Count / dias;
        WriteLine($"Media: {media}");
        
        WriteLine("19: Accidentes por alcohol y drogra");
        var alcohol = ((BooleanDataFrameColumn)df["PositivoAlcohol"])
            .ElementwiseEquals(true);
        var droga = ((BooleanDataFrameColumn)df["PositivoDroga"])
            .ElementwiseEquals(true);
        var conjunto = df.Filter(alcohol & droga);
        
        
        WriteLine("20: Rangos de edad mas vulnerables");
        var persona = (StringDataFrameColumn)df["TipoPersona"];
        var peatones = df.Filter(
            persona.ElementwiseEquals("Peatón")
        );
        var rangos = peatones
            .GroupBy("RangoEdad")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");

        
        WriteLine("21: Distritos con mas positivos en alcohol");
        var positivo = ((BooleanDataFrameColumn)df["PositivoAlcohol"])
            .ElementwiseEquals(true);
        var distritosAlcohol =  df
            .Filter(positivo) 
            .GroupBy("Distrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
       
        WriteLine("22:Accidentes por codigo de distrito");
        var codigo = df
            .GroupBy("CodDistrito")
            .Count("NumExpediente")
            .OrderByDescending("NumExpediente");
        
        
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
        
        
        WriteLine("24: Evolucion mensual por año");
        
       
        
        
        WriteLine("25: Distrito con mas accidentes por año");
        
        
        WriteLine("26: Tendencia de Alcohol por año");
       
        
        
        WriteLine("27: Comparativa fin de semana vs entre semana por año");
        
        WriteLine("28: Hora pico por año");
       
        
        WriteLine("29: Lesión más frecuente por año");
       
        
        WriteLine("30: Evolución de peatones por año");
        

        
        
        
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