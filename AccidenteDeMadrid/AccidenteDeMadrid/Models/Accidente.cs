using AccidenteDeMadrid.Enums;

namespace AccidenteDeMadrid.Models;

public record Accidente {
    public string NumExpediente { get;  init; } = string.Empty;
    public DateOnly Fecha {  get;  init; } 
    public TimeOnly Hora {  get;  init; }
    public string? Localizacion { get;  init; } = string.Empty;
    public int CodigoDistrito { get;  init; }
    public string Distrito { get;  init; } = string.Empty;
    public TipoAccidente TipoAccidente { get;  init; }
    public string EstadoMetereologico { get;  init; } = string.Empty;
    public string TipoVehiculo { get;  init; } = string.Empty;
    public string TipoPersona { get;  init; } = string.Empty;
    public string RangoEdad { get;  init; } = string.Empty;
    public Sexo Sexo { get;  init; }
    public int CodLesividad { get;  init; }
    public string Lesividad { get;  init; } = string.Empty;
    public double CoordenadaXUtm { get;  init; }
    public double CoordenadaYUtm { get;  init; }
    public bool PositivoAlcohol { get;  init; }
    public bool PositivoDroga  {  get;  init; }
}