using AutoMapper;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


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

        public async Task<List<Model.Proizvodi>> Get()
        {
            var entityList = await context.Proizvodis.ToListAsync();
            return mapper.Map<List<Model.Proizvodi>>(entityList);
        }
    }
}
