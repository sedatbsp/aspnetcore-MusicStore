using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.Update(shoppingCart);
        }
    }
}
