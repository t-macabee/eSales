using AutoMapper;
using eSales.Model.Requests.Korisnici;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace eSales.Services.Services
{
    public class KorisniciService : IKorisniciService
    {
        private EProdajaContext context { get; set; }
        public IMapper mapper { get; set; }

        public KorisniciService(EProdajaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Model.Korisnici> Get()
        {
            var entityList = context.Korisnicis.ToList();
            return mapper.Map<List<Model.Korisnici>>(entityList);
        }       

        public Model.Korisnici Insert(KorisniciInsertRequest request)
        {
            var entity = new Korisnici();
            mapper.Map(request, entity);

            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);

            context.Korisnicis.Add(entity);
            context.SaveChanges();

            return mapper.Map<Model.Korisnici>(entity);
        }

        public Model.Korisnici Update(int id, KorisniciUpdateRequest request)
        {
            var entity = context.Korisnicis.Find(id);
            mapper.Map(request, entity);

            context.SaveChanges();

            return mapper.Map<Model.Korisnici>(entity);
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
    }
}
