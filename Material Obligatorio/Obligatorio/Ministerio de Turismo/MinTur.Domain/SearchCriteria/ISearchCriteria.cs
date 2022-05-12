namespace MinTur.Domain.SearchCriteria
{
    public interface ISearchCriteria<T>
    {
        bool MatchesCriteria(T businessEntity);
    }
}
