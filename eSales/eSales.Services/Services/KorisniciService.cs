using AutoMapper;
using Azure.Core;
using eSales.Model.Requests.Korisnici;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace eSales.Services.Services
{
    public class KorisniciService : BaseCRUDService<Model.Korisnici, Database.Korisnici, KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>, IKorisniciService
    {
        public KorisniciService(EProdajaContext context, IMapper mapper) : base(context, mapper) { }

        public override async Task BeforeInsert(Korisnici entity, KorisniciInsertRequest insert)
        {
            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, insert.Password);
        }

        public override IQueryable<Korisnici> AddInclude(IQueryable<Korisnici> query, KorisniciSearchObject? search = null)
        {
            if(search?.IsUlogeIncluded == true)
            {
                query = query.Include("KorisniciUloges.Uloga");
            }
            return base.AddInclude(query, search);
        }

        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);

            return Convert.ToBase64String(byteArray);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);

            return Convert.ToBase64String(inArray);
        }

        public async Task<Model.Korisnici> Login(string username, string password)
        {
            var entity = await context.Korisnicis.Include("KorisniciUloges.Uloga").FirstOrDefaultAsync(x => x.KorisnickoIme == username);
            if(entity == null) 
            {
                return null;
            }
            var hash = GenerateHash(entity.LozinkaSalt, password);
            if(hash != entity.LozinkaHash)
            {
                return null;
            }
            return mapper.Map<Model.Korisnici>(entity);
        }
    }
}
