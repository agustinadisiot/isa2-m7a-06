using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IAuthenticationTokenRepository
    {
        AuthorizationToken GetAuthenticationTokenById(Guid id);
        Guid CreateNewAuthorizationTokenFor(Administrator administrator);
    }
}
