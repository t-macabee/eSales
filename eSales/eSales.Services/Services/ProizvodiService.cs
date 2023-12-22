using eSales.Model;
using eSales.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSales.Services.Services
{
    public class ProizvodiService : IProizvodiService
    {
        List<Proizvodi> proizvodi = new List<Proizvodi>()
        {
            new Proizvodi()
            {
                ProizvodId = 1,
                Naziv = "Laptop"
            }
        };

        public IList<Proizvodi> Get()
        {
            return proizvodi;
        }
    }
}
