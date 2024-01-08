using AutoMapper;
using Azure.Core;
using eSales.Model.Requests.Proizvodi;
using eSales.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSales.Services.ProizvodiStateMachine
{
    public class DraftProductState : BaseState
    {
        public DraftProductState(EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override async Task<Model.Proizvodi> Update(int id, ProizvodiUpdateRequest request)
        {
            var set = context.Set<Database.Proizvodi>();

            var entity = await set.FindAsync(id);

            mapper.Map(request, entity);

            await context.SaveChangesAsync();

            return mapper.Map<Model.Proizvodi>(entity);
        }

        public override async Task<Model.Proizvodi> Activate(int id)
        {
            var set = context.Set<Database.Proizvodi>();

            var entity = await set.FindAsync(id);

            entity.StateMachine = "active";

            await context.SaveChangesAsync();

            return mapper.Map<Model.Proizvodi>(entity);
        }
    }
}
