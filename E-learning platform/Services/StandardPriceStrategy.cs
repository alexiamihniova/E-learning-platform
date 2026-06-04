using E_learning_platform.Interfaces;

namespace E_learning_platform.Services
{
    public class StandardPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice;
        }
    }
}
