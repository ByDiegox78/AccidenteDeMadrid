// See https://aka.ms/new-console-template for more information

using AccidenteDeMadrid.Repository;
using AccidenteDeMadrid.Service;

Console.WriteLine("Hello, World!");
string BaseDirectory = AppContext.BaseDirectory;
//string CsvPath = Path.Combine(BaseDirectory, "Data");
string CsvPath = @"C:\Users\oms04\Desktop\AccidenteDeMadrid\AccidenteDeMadrid\AccidenteDeMadrid\Data";
var repo = new AccidenteRepository();
var datos = await repo.CargarAsync(CsvPath);
var service = new AccidenteService();
service.ConsultasLinq(datos);
service.ConsultasDataFrame(datos);