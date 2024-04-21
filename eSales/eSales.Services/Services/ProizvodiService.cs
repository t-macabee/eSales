using AutoMapper;
using eSales.Model.Requests.Proizvodi;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using eSales.Services.ProizvodiStateMachine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace eSales.Services.Services
{
    public class ProizvodiService : BaseCRUDService<Model.Proizvodi, Proizvodi, ProizvodiSearchObject, ProizvodiInsertRequest, ProizvodiUpdateRequest>, IProizvodiService
    {
        public BaseState baseState { get; set; }

        public ProizvodiService(EProdajaContext context, IMapper mapper, BaseState baseState) : base(context, mapper) 
        {
           this.baseState = baseState;
        }

        public override IQueryable<Proizvodi> AddFilter(IQueryable<Proizvodi> query, ProizvodiSearchObject? search = null)
        {
            var filteredQuery =  base.AddFilter(query, search);
            if(!string.IsNullOrWhiteSpace(search?.FTS))
            {
                filteredQuery = filteredQuery.Where(x => x.Naziv.Contains(search.FTS) || x.Sifra.Contains(search.FTS));
            }

            if(!string.IsNullOrWhiteSpace(search?.Sifra))
            {
                filteredQuery = filteredQuery.Where(x => x.Sifra == search.Sifra);
            }

            return filteredQuery; 
        }

        public override Task<Model.Proizvodi> Insert(ProizvodiInsertRequest insert)
        {
            var state = baseState.CreateState("initial");

            return state.Insert(insert);
        }

        public override async Task<Model.Proizvodi> Update(int id, ProizvodiUpdateRequest update)
        {
            var entity = await context.Proizvodis.FindAsync(id);

            var state = baseState.CreateState(entity.StateMachine);

            return await state.Update(id, update);
        }

        public async Task<Model.Proizvodi> Activate(int id)
        {
            var entity = await context.Proizvodis.FindAsync(id);

            var state = baseState.CreateState(entity.StateMachine);

            return await state.Activate(id);
        }

        public async Task<Model.Proizvodi> Hide(int id)
        {
            var entity = await context.Proizvodis.FindAsync(id);

            var state = baseState.CreateState(entity.StateMachine);

            return await state.Hide(id);
        }

        public async Task<List<string>> AllowedActions(int id)
        {
            var entity = await context.Proizvodis.FindAsync(id);

            var state = baseState.CreateState(entity?.StateMachine ?? "initial");

            return await state.AllowedActions();
        }
    }
}
