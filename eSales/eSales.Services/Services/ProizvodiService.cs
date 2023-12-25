using AutoMapper;
using eSales.Services.Database;
using eSales.Services.Interfaces;


namespace eSales.Services.Services
{
    public class ProizvodiService : IProizvodiService
    {
        private EProdajaContext context { get; set; }
        public IMapper mapper { get; set; }

        public ProizvodiService(EProdajaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Model.Proizvodi> Get()
        {
            var entityList = context.Proizvodis.ToList();
            return mapper.Map<List<Model.Proizvodi>>(entityList);
        }
    }
}
