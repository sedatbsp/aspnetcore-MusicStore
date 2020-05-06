
using MainMusicStore.DataAccess.IMainRepository.IRepository;
using MainMusicStore.Models.DbModels;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}
