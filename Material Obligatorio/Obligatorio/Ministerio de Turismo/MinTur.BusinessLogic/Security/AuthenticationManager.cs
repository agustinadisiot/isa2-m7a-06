using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.BusinessLogicInterface.Security;
using System;

namespace MinTur.BusinessLogic.Security
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public AuthenticationManager(IRepositoryFacade repositoryFacade) 
        {
            _repositoryFacade = repositoryFacade;
        }

        public bool IsTokenValid(Guid id)
        {
            try 
            {
                AuthorizationToken token =  _repositoryFacade.GetAuthenticationTokenById(id);
                /*If there was further logic of durability, token belonging only to an admin and not another kind of user
                it would all go here*/
                return true;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public Guid Login(Administrator administratorCredentials)
        {
            administratorCredentials.ValidOrFail();
            Guid newToken = _repositoryFacade.CreateNewAuthorizationTokenFor(administratorCredentials);

            return newToken;
        }
    }
}
