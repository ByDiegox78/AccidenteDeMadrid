using AccidenteDeMadrid.Models;

namespace AccidenteDeMadrid.Service;

public interface IAccidenteService {
    void ConsultasLinq(IEnumerable<Accidente> accidentes);
    void ConsultasDataFrame();
}