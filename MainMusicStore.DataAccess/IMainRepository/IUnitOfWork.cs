using System;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        ISPCallRepository Sp_call { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();

    }
}
