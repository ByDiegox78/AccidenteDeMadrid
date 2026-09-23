using System.Globalization;
using AccidenteDeMadrid.Enums;
using AccidenteDeMadrid.Models;
using CsvHelper.Configuration;

namespace AccidenteDeMadrid.Mappers;

public class AccidenteMapper : ClassMap<Accidente> {
    public AccidenteMapper() {
        Map(a => a.NumExpediente).Name("num_expediente");
        Map(a => a.Localizacion).Name("localizacion");
        Map(a => a.Distrito).Name("distrito");
        Map(a => a.EstadoMetereologico).Name("estado_meteorológico");
        Map(a => a.TipoVehiculo).Name("tipo_vehiculo");
        Map(a => a.TipoPersona).Name("tipo_persona");
        Map(a => a.RangoEdad).Name("rango_edad");
        Map(a => a.Lesividad).Name("lesividad");

        Map(a => a.CodigoDistrito)
            .Convert(args => int.TryParse(args.Row.GetField("cod_distrito"), out var codigo) ? codigo : 0);
        Map(a => a.CodLesividad).Convert(args => 
            int.TryParse(args.Row.GetField("cod_lesividad"), out var val) ? val : 0);

        Map(a => a.CoordenadaXUtm).Convert(args => 
            double.TryParse(args.Row.GetField("coordenada_x_utm"), out var val) ? val : 0);

        Map(a => a.CoordenadaYUtm).Convert(args => 
            double.TryParse(args.Row.GetField("coordenada_y_utm"), out var val) ? val : 0);
        Map(a => a.Sexo).Convert(args => args.Row.GetField("sexo")?.Trim().ToLowerInvariant() switch {
            "hombre" => Sexo.Hombre,
            "mujer" => Sexo.Mujer,
            _ => Sexo.NoAsignado
        });
        Map(a => a.TipoAccidente).Convert(args => args.Row.GetField("tipo_accidente")?.Trim().ToLowerInvariant() switch {
            "Colision doble" => TipoAccidente.ColisionMultiple,
            "Colisión múltiple" => TipoAccidente.ColisionMultiple,
            "Alcance" => TipoAccidente.Alcance,
            "Choque contra obstaculo o elemento de la vía" => TipoAccidente.ChoqueContraObstaculoOElemento,
            "Atropello a persona" => TipoAccidente.Atropello,
            "Vuelvo" => TipoAccidente.Vuelvo,
            "Caída" => TipoAccidente.Caida,
            "Otras causas" => TipoAccidente.OtrasCausas,
        });
        Map(a => a.PositivoAlcohol).Convert(args => args.Row.GetField("positiva_alcohol")?.Trim().ToUpperInvariant() == "S");
        Map(a => a.PositivoDroga).Convert(args => args.Row.GetField("positiva_droga")?.Trim() == "1");
        Map(a => a.Fecha).Convert(args => DateOnly.TryParseExact(args.Row.GetField("fecha"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha) ? fecha : default);
        Map(a => a.Hora).Convert(args => TimeOnly.TryParse(args.Row.GetField("hora"), out var hora) ? hora : default);


    }
}