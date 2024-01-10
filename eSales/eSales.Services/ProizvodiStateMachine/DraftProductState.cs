using AutoMapper;
using Azure.Core;
using eSales.Model.Exceptions;
using eSales.Model.Requests.Proizvodi;
using eSales.Services.Database;
using Microsoft.Extensions.Logging;

namespace eSales.Services.ProizvodiStateMachine
{
    public class DraftProductState : BaseState
    {
        protected ILogger<DraftProductState> logger;
        public DraftProductState(ILogger<DraftProductState> logger, EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
            this.logger = logger;
        }

        public override async Task<Model.Proizvodi> Update(int id, ProizvodiUpdateRequest request)
        {
            var set = context.Set<Database.Proizvodi>();

            var entity = await set.FindAsync(id);

            mapper.Map(request, entity);

            if(entity.Cijena < 0)
            {
                throw new Exception("Cijena ne moze biti ispod nule");
            }
            if (entity.Cijena < 1)
            {
                throw new UserException("Cijena ispod minimuma");
            }

            await context.SaveChangesAsync();

            return mapper.Map<Model.Proizvodi>(entity);
        }

        public override async Task<Model.Proizvodi> Activate(int id)
        {
            logger.LogInformation($"Aktivacija proizvoida: {id}");

            var set = context.Set<Database.Proizvodi>();

            var entity = await set.FindAsync(id);

            entity.StateMachine = "active";

            await context.SaveChangesAsync();

            return mapper.Map<Model.Proizvodi>(entity);
        }

        public override async Task<List<string>> AllowedActions()
        {
            var list = await base.AllowedActions();

            list.Add("Update");
            list.Add("Activate");

            return list;
        }
    }
}
