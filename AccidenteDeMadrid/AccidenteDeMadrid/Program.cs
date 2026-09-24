// See https://aka.ms/new-console-template for more information
using static System.Console;
using System.Diagnostics;
using AccidenteDeMadrid.Repository;
using AccidenteDeMadrid.Service;

Console.WriteLine("Hello, World!");
string BaseDirectory = AppContext.BaseDirectory;
string CsvPath = Path.Combine(BaseDirectory, "Data");
var service = new AccidenteService();
var sw= Stopwatch.StartNew();
var repo = new AccidenteRepository();
var datos = (await repo.CargarAsync(CsvPath)).ToList();

sw.Stop();
WriteLine($"Tiempo de carga: {sw.Elapsed.TotalSeconds:F2} segundos");
service.ConsultasLinq(datos);

service.ConsultasDataFrame(datos);
