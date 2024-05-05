using AutoMapper;
using eSales.Model.Requests.Proizvodi;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using eSales.Services.ProizvodiStateMachine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
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

        static MLContext mlContext = null;
        static object isLocked = new object();
        static ITransformer model = null;
        public List<Model.Proizvodi> Recommend(int id)
        {
            lock (isLocked) 
            {
                if (mlContext == null)
                {
                    mlContext = new MLContext();

                    var tmpData = context.Narudzbes.Include("NarudzbaStavkes").ToList();

                    var data = new List<ProductEntry>();

                    foreach (var x in tmpData)
                    {
                        if(x.NarudzbaStavkes.Count > 1)
                        {
                            var disctinctItemId = x.NarudzbaStavkes.Select(y => y.ProizvodId).ToList();

                            disctinctItemId.ForEach(y =>
                            {
                                var relatedItems = x.NarudzbaStavkes.Where(z => z.ProizvodId != y);

                                foreach(var z in relatedItems)
                                {
                                    data.Add(new ProductEntry() { ProductId = (uint)y, CoPurchaseProductId = (uint)z.ProizvodId});
                                }
                            });
                        }
                    }

                    var trainData = mlContext.Data.LoadFromEnumerable(data);

                    MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                    options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductId);
                    options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductId);
                    options.LabelColumnName = "Label";
                    options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                    options.Alpha = 0.01;
                    options.Lambda = 0.025;
                    options.NumberOfIterations = 100;
                    options.C = 0.00001;

                    var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                    model = est.Fit(trainData);
                }
            }

            var products = context.Proizvodis.Where(x => x.ProizvodId != id);

            var predictionResult = new List<Tuple<Database.Proizvodi, float>>();

            foreach (var product in products) 
            {
                var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
                var prediction = predictionEngine.Predict(
                    new ProductEntry()
                    {
                        ProductId = (uint)id,
                        CoPurchaseProductId = (uint)product.ProizvodId
                    });
                predictionResult.Add(new Tuple<Database.Proizvodi, float>(product, prediction.Score));
            }

            var finalResult = predictionResult.OrderByDescending(x => x.Item2).Select(x => x.Item1).ToList();

            return mapper.Map<List<Model.Proizvodi>>(finalResult);
        }

    }

    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }

    public class ProductEntry
    {
        [KeyType(count: 10)]
        public uint ProductId { get; set; }
        [KeyType(count: 10)]
        public uint CoPurchaseProductId { get; set; }
        public float Label { get; set; }
    }
}
