using Microsoft.EntityFrameworkCore;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using System.Linq;
using MinTur.Exceptions;
using System;

namespace MinTur.DataAccess.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        protected DbContext Context { get; set; }

        public AdministratorRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public List<Administrator> GetAllAdministrators()
        {
            return Context.Set<Administrator>().ToList();
        }

        public void DeleteAdministratorById(int administratorId)
        {
            if (!AdministratorExists(administratorId))
                throw new ResourceNotFoundException("Could not find specified administrator");
            if (GetAmountOfAdministrators() == 1)
                throw new InvalidOperationException("Can not delete the last administrator");

            Administrator retrievedAdministrator = Context.Set<Administrator>().Where(a => a.Id == administratorId).FirstOrDefault();
            RemoveAdministratorFromDb(retrievedAdministrator);
        }

        public int StoreAdministrator(Administrator administrator)
        {
            if (EmailAlreadyExists(administrator.Email))
                throw new InvalidOperationException("Administrator with same email already registered");

            StoreAdministratorInDb(administrator);
            return administrator.Id;
        }

        public void UpdateAdministrator(Administrator newAdministrator)
        {
            if (!AdministratorExists(newAdministrator.Id))
                throw new ResourceNotFoundException("Could not find specified administrator");
            if (EmailAlreadyExists(newAdministrator.Email))
                throw new InvalidOperationException("Administrator with same email already registered");

            Administrator retrievedAdministrator = Context.Set<Administrator>().Where(a => a.Id == newAdministrator.Id).FirstOrDefault();
            retrievedAdministrator.Update(newAdministrator);
            UpdateAdministratorInDb(retrievedAdministrator);
        }

        public Administrator GetAdministratorById(int administratorId)
        {
            if(!AdministratorExists(administratorId))
                throw new ResourceNotFoundException("Could not find specified administrator");

            return Context.Set<Administrator>().AsNoTracking().Where(a => a.Id == administratorId).FirstOrDefault();
        }


        private bool AdministratorExists(int administratorId)
        {
            Administrator retrievedAdministrator = Context.Set<Administrator>().AsNoTracking().Where(a => a.Id == administratorId)
                .FirstOrDefault();

            return retrievedAdministrator != null;
        }

        private bool EmailAlreadyExists(string email)
        {
            Administrator retrievedAdministrator = Context.Set<Administrator>().AsNoTracking().Where(a => a.Email == email)
                .FirstOrDefault();

            return retrievedAdministrator != null;
        }

        private int GetAmountOfAdministrators()
        {
            return Context.Set<Administrator>().AsNoTracking().ToList().Count;
        }

        private void RemoveAdministratorFromDb(Administrator administrator) 
        {
            Context.Remove(administrator);
            Context.SaveChanges();
        }

        private void StoreAdministratorInDb(Administrator administrator)
        {
            Context.Set<Administrator>().Add(administrator);
            Context.SaveChanges();
            Context.Entry(administrator).State = EntityState.Detached;
        }
        
        private void UpdateAdministratorInDb(Administrator administrator)
        {
            Context.Entry(administrator).State = EntityState.Modified;
            Context.SaveChanges();
            Context.Entry(administrator).State = EntityState.Detached;
        }

    }
}