using AutoMapper;
using eSales.Model;
using eSales.Model.Requests.Proizvodi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSales.Services.ProizvodiStateMachine
{
    public class InitialProductState : BaseState
    {
        public InitialProductState(Database.EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override async Task<Proizvodi> Insert(ProizvodiInsertRequest request)
        {
            var set = context.Set<Database.Proizvodi>();

            var entity = mapper.Map<Database.Proizvodi>(request);

            entity.StateMachine = "draft";

            set.Add(entity);

            await context.SaveChangesAsync();

            return mapper.Map<Proizvodi>(entity);
        }
    }
}
