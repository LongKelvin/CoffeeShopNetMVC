using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IApplicationNotificationService
    {
        void Create(ApplicationNotification notification);

        void Update(ApplicationNotification notification);
        void MakeAsReadedNotification(int notificationId);

        void Delete(int id);

        ApplicationNotification GetById(int id);

        IEnumerable<ApplicationNotification> GetAll();

        List<ApplicationNotification> GetTop10NewNotification();

        ApplicationNotification GetDetail(int id);

        void SaveChanges();
    }

    public class ApplicationNotificationService : IApplicationNotificationService
    {
        private IApplicationNotificationRepository _appNotificationRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationNotificationService(IUnitOfWork unitOfWork,
            IApplicationNotificationRepository appNotificationRepository)
        {
            this._appNotificationRepository = appNotificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Create(ApplicationNotification notification)
        {
           _appNotificationRepository.Add(notification);
        }

        public void Delete(int id)
        {
            _appNotificationRepository.Delete(id);
        }

        public IEnumerable<ApplicationNotification> GetAll()
        {
            return _appNotificationRepository.GetAll();
        }

        public ApplicationNotification GetById(int id)
        {
           return _appNotificationRepository.GetById(id);
        }

        public ApplicationNotification GetDetail(int id)
        {
            return _appNotificationRepository.GetById(id);
        }

        public List<ApplicationNotification> GetTop10NewNotification()
        {
            return _appNotificationRepository.GetTop10NewNotification();
        }

        public void MakeAsReadedNotification(int notificationId)
        {
            var updateNotification = _appNotificationRepository.GetById(notificationId);
            if(updateNotification == null)
                return;

            updateNotification.IsReaded = true;
            _appNotificationRepository.Update(updateNotification);
        }

        public void SaveChanges()
        {
             _unitOfWork.Commit();
        }

        public void Update(ApplicationNotification notification)
        {
            _appNotificationRepository.Update(notification);
        }
    }
}