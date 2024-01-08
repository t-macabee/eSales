using AutoMapper;
using eSales.Model.Requests.Proizvodi;
using eSales.Services.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSales.Services.ProizvodiStateMachine
{
    public class BaseState
    {
        public EProdajaContext context { get; set; }
        public IMapper mapper { get; set; }
        public IServiceProvider serviceProvider { get; set; }

        public BaseState(EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider)
        {
            this.context = context;
            this.mapper = mapper;
            this.serviceProvider = serviceProvider;
        }

        public virtual Task<Model.Proizvodi> Insert(ProizvodiInsertRequest request)
        {
            throw new Exception("Not allowed!");
        }

        public virtual Task<Model.Proizvodi> Update(int id, ProizvodiUpdateRequest request)
        {
            throw new Exception("Not allowed!");
        }

        public virtual Task<Model.Proizvodi> Activate(int id)
        {
            throw new Exception("Not allowed!");
        }

        public virtual Task<Model.Proizvodi> Hide(int id)
        {
            throw new Exception("Not allowed!");
        }

        public virtual Task<Model.Proizvodi> Delete(int id)
        {
            throw new Exception("Not allowed!");
        }

        public BaseState CreateState(string stateName)
        {
            switch (stateName) 
            {
                case "initial": return serviceProvider.GetService<InitialProductState>();
                case "draft": return serviceProvider.GetService<DraftProductState>();
                case "active": return serviceProvider.GetService<ActiveProductState>();
                default: throw new Exception("Not allowed!");
            }
        }
    }
}
