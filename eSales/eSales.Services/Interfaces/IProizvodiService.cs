using eSales.Model;
using eSales.Model.Requests.Proizvodi;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Services;

namespace eSales.Services.Interfaces
{
    public interface IProizvodiService : ICRUDService<Model.Proizvodi, ProizvodiSearchObject, ProizvodiInsertRequest, ProizvodiUpdateRequest>
    {
        Task<Model.Proizvodi> Activate(int id);
        Task<Model.Proizvodi> Hide(int id);
        Task<List<string>> AllowedActions(int id);
    }
}
