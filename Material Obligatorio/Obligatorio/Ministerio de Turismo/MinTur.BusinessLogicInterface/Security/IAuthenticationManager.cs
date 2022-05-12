using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.BusinessLogicInterface.Security
{
    public interface IAuthenticationManager
    {
        bool IsTokenValid(Guid token);

        Guid Login(Administrator administratorCredentials);
    }
}
