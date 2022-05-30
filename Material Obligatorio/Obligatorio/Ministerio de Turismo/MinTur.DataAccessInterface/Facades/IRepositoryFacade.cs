using System.Collections.Generic;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;

namespace MinTur.DataAccessInterface.Facades
{
    public interface IRepositoryFacade : IRegionRepository, ITouristPointRepository, ICategoryRepository, IResortRepository, IReservationRepository,
        IAuthenticationTokenRepository, IAdministratorRepository, IReviewRepository, IChargingPointRepository
    {
  
    }
}
