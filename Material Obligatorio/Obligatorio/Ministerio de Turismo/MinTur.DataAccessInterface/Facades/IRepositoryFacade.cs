using MinTur.DataAccessInterface.Repositories;

namespace MinTur.DataAccessInterface.Facades
{
    public interface IRepositoryFacade : IRegionRepository, ITouristPointRepository, ICategoryRepository, IResortRepository, IReservationRepository,
        IAuthenticationTokenRepository, IAdministratorRepository, IReviewRepository, IChargingPointRepository
    {

    }
}
