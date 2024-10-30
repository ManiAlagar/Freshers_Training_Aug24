using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class StatusService :IStatusService
    {

        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            this._statusRepository = statusRepository;
        }
        public async Task<IEnumerable<Status>> GetAllOrderStatus()
        {
            return await _statusRepository.GetAllOrderStatus();
        }
    }
}
