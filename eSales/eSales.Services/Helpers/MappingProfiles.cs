using AutoMapper;
using eSales.Model.Requests.Korisnici;

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

            CreateMap<Database.JediniceMjere, Model.JediniceMjere>();

            CreateMap<Database.VrsteProizvodum, Model.VrsteProizvoda>();
        }
    }
}
