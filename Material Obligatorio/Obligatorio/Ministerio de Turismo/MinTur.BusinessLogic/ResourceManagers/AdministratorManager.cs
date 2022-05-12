using System.Collections.Generic;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class AdministratorManager : IAdministratorManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public AdministratorManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        public void DeleteAdministratorById(int administratorId)
        {
            _repositoryFacade.DeleteAdministratorById(administratorId);
        }

        public Administrator GetAdministratorById(int administratorId)
        {
            return _repositoryFacade.GetAdministratorById(administratorId);
        }

        public List<Administrator> GetAllAdministrators()
        {
            return _repositoryFacade.GetAllAdministrators();
        }

        public Administrator RegisterAdministrator(Administrator administrator)
        {
            administrator.ValidOrFail();
            int newAdministratorId = _repositoryFacade.StoreAdministrator(administrator);
            Administrator createdAdministrator = _repositoryFacade.GetAdministratorById(newAdministratorId);

            return createdAdministrator;
        }

        public Administrator UpdateAdministrator(Administrator administrator)
        {
            administrator.ValidOrFail();
            _repositoryFacade.UpdateAdministrator(administrator);

            Administrator updatedAdministrator = _repositoryFacade.GetAdministratorById(administrator.Id);
            return updatedAdministrator;
        }
    }
}