using eSales.Model.Requests.Korisnici;

namespace eSales.Services.Interfaces
{
    public interface IKorisniciService : ICRUDService<Model.Korisnici, Model.SearchObjects.KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>
    {
    }
}
