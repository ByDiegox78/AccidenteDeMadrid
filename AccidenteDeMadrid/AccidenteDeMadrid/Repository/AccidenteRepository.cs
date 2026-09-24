using System.Globalization;
using AccidenteDeMadrid.Mappers;
using AccidenteDeMadrid.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace AccidenteDeMadrid.Repository;

public class AccidenteRepository : IAccidenteRepository {
    
    private readonly CsvConfiguration _config = new(CultureInfo.InvariantCulture) {
        Delimiter = ";",
        HasHeaderRecord = true,
        MissingFieldFound = null,
        HeaderValidated = null,
        TrimOptions = TrimOptions.Trim
    };    
    public async Task<IEnumerable<Accidente>> CargarAsync(string path) {
        ComrpobarFichero(path);
        var archivos = Directory.GetFiles(path, "*.csv");
        var tareas = archivos.Select(LeerAsync);
        var resultados = await Task.WhenAll(tareas);
        return resultados
            .SelectMany(x => x)
            .ToList();
    }
    private async Task<List<Accidente>> LeerAsync(string archivo) {
        using var reader = new StreamReader(archivo);
        using var csv = new CsvReader(reader, _config);
        csv.Context.RegisterClassMap<AccidenteMapper>();
        var accidentes = new List<Accidente>();
        await foreach (var accidente in csv.GetRecordsAsync<Accidente>()) 
            accidentes.Add(accidente);
        return accidentes;
    }
    private void ComrpobarFichero(string path) {
        if (!Directory.Exists(path)) {
            throw new DirectoryNotFoundException("No existe el directorio");
        }
        return;
    }
}