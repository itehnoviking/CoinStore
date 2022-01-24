namespace CoinStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get;}
    }
}
