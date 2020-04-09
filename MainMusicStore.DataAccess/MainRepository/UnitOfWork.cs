using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
            coverType = new CoverTypeRepository(_db);
            sp_call = new SPCallRepository(_db);
            product = new ProductRepository(_db);
        }

        public ICategoryRepository category { get; private set; }

        public IProductRepository product { get; private set; }
        public ISPCallRepository sp_call { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
