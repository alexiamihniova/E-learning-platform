using E_learning_platform.Interfaces;

namespace E_learning_platform.Services
{
    public class DiscountPriceStrategy : IPriceStrategy
    {
        private readonly decimal _discountPercentage;

        public DiscountPriceStrategy(decimal discountPercentage = 0.1m) // default 10%
        {
            _discountPercentage = discountPercentage;
        }

        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice - (basePrice * _discountPercentage);
        }
    }
}
