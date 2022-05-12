using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IAdministratorRepository
    {
        List<Administrator> GetAllAdministrators();
        Administrator GetAdministratorById(int administratorId);
        void DeleteAdministratorById(int administratorId);
        void UpdateAdministrator(Administrator administrator);
        int StoreAdministrator(Administrator administrator);
    }
}