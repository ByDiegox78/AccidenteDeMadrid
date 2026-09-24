using AccidenteDeMadrid.Models;
using Microsoft.Data.Analysis;

namespace AccidenteDeMadrid.Service;

public interface IAccidenteService {
    void ConsultasLinq(IEnumerable<Accidente> accidentes);
    void ConsultasDataFrame(IEnumerable<Accidente> datos);
}