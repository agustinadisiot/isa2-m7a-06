using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IAdministratorManager
    {
        List<Administrator> GetAllAdministrators();
        void DeleteAdministratorById(int administratorId);
        Administrator RegisterAdministrator(Administrator administrator);
        Administrator UpdateAdministrator(Administrator administrator);
        Administrator GetAdministratorById(int administratorId);
    }
}