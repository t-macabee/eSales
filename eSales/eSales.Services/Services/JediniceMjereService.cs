using AutoMapper;
using eSales.Model;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSales.Services.Services
{
    public class JediniceMjereService : BaseService<Model.JediniceMjere, Database.JediniceMjere, JediniceMjereSearchObject>, IJediniceMjereService
    {
        public JediniceMjereService(EProdajaContext context, IMapper mapper) : base(context, mapper) 
        {
        }

        public override IQueryable<Database.JediniceMjere> AddFilter(IQueryable<Database.JediniceMjere> query, JediniceMjereSearchObject? search = null)
        {
            if (!string.IsNullOrWhiteSpace(search.Naziv))
            {
                query = query.Where(x => x.Naziv.StartsWith(search.Naziv));
            }

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                query = query.Where(x => x.Naziv.Contains(search.FTS));
            }

            return base.AddFilter(query, search);
        }
    }
}
