using AutoMapper;
using eSales.Model.Requests.Korisnici;
using eSales.Model.Requests.Proizvodi;

namespace eSales.Services.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()  
        {
            CreateMap<Database.Korisnici, Model.Korisnici>();
            CreateMap<KorisniciInsertRequest, Database.Korisnici>();
            CreateMap<KorisniciUpdateRequest, Database.Korisnici>();

            CreateMap<Database.Proizvodi, Model.Proizvodi>();
            CreateMap<ProizvodiInsertRequest, Database.Proizvodi>();
            CreateMap<ProizvodiUpdateRequest, Database.Proizvodi>();

            CreateMap<Database.JediniceMjere, Model.JediniceMjere>();

            CreateMap<Database.VrsteProizvodum, Model.VrsteProizvoda>();

            CreateMap<Database.KorisniciUloge, Model.KorisniciUloge>();

            CreateMap<Database.Uloge, Model.Uloge>();
        }
    }
}
