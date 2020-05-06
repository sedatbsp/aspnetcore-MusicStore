using System;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        ISPCallRepository Sp_call { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();

    }
}
