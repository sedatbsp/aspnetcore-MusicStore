using System;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository category { get; }
        IProductRepository product { get; }
        ICoverTypeRepository coverType { get; }
        ISPCallRepository sp_call { get; }
        void Save();

    }
}
