using eSales.Services.Database;

namespace eSales.Services.Interfaces
{
    public interface IProizvodiService
    {
       Task<List<Model.Proizvodi>> Get();
    }
}
