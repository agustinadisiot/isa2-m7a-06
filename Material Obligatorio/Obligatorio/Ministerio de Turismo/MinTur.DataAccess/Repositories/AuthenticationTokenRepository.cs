using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.DataAccess.Repositories
{
    public class AuthenticationTokenRepository : IAuthenticationTokenRepository
    {
        protected DbContext Context { get; set; }

        public AuthenticationTokenRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public AuthorizationToken GetAuthenticationTokenById(Guid tokenId)
        {
            AuthorizationToken retrievedToken = Context.Set<AuthorizationToken>().Include(t => t.Administrator).
                Where(a => a.Id.Equals(tokenId)).FirstOrDefault();

            if (retrievedToken == null)
                throw new ResourceNotFoundException("Could not find specified token");

            return retrievedToken;
        }

        public Guid CreateNewAuthorizationTokenFor(Administrator administrator)
        {
            Administrator storedAdministrator = GetAdministratorWithMatchingCredentials(administrator);

            if (storedAdministrator == null)
                throw new ResourceNotFoundException("Could not find specified administrator");
            
            DeleteExistingTokensFrom(storedAdministrator);

            AuthorizationToken newToken = StoreAuthorizationTokenInDb(storedAdministrator);
            return newToken.Id;
        }

        private void DeleteExistingTokensFrom(Administrator administrator)
        {
            List<AuthorizationToken> existingTokens = Context.Set<AuthorizationToken>().AsNoTracking()
                .Where(a => a.Administrator.Equals(administrator)).ToList();

            if (existingTokens.Count > 0)
            {
                Context.Set<AuthorizationToken>().RemoveRange(existingTokens);
            }
        }

        private Administrator GetAdministratorWithMatchingCredentials(Administrator administratorCredentials)
        {
            Administrator retrievedAdministrator = Context.Set<Administrator>().AsNoTracking().
                Where(a => a.Email == administratorCredentials.Email && a.Password == administratorCredentials.Password)
                .FirstOrDefault();

            return retrievedAdministrator;
        }

        private AuthorizationToken StoreAuthorizationTokenInDb(Administrator administrator)
        {
            AuthorizationToken newToken = new AuthorizationToken() { Administrator = administrator };

            Context.Entry(newToken.Administrator).State = EntityState.Unchanged;
            Context.Add(newToken);
            Context.SaveChanges();
            Context.Entry(newToken).State = EntityState.Detached;

            return newToken;
        }

    }
}
