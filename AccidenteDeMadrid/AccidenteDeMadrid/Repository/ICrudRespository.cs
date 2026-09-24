namespace AccidenteDeMadrid.Repository;

public interface ICrudRespository<T> {
    Task<IEnumerable<T>> CargarAsync(string path);
    

}