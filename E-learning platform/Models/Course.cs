using E_learning_platform.Interfaces;

namespace E_learning_platform.Models
{
    public class Course : ICourseComponent
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public decimal BasePrice { get; private set; }
        private IPriceStrategy _priceStrategy;

        public Course(int id, string title, decimal basePrice, IPriceStrategy priceStrategy)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new System.ArgumentException("Title cannot be empty.", nameof(title));
            if (basePrice < 0)
                throw new System.ArgumentException("Price cannot be negative.", nameof(basePrice));
            if (priceStrategy == null)
                throw new System.ArgumentNullException(nameof(priceStrategy));

            Id = id;
            Title = title;
            BasePrice = basePrice;
            _priceStrategy = priceStrategy;
        }

        public void SetPriceStrategy(IPriceStrategy priceStrategy)
        {
            if (priceStrategy == null)
                throw new System.ArgumentNullException(nameof(priceStrategy));
            _priceStrategy = priceStrategy;
        }

        public decimal GetPrice()
        {
            return _priceStrategy.CalculatePrice(BasePrice);
        }
        public List<string> Modules { get; private set; } = new List<string>();

        public void AddModule(string module)
        {
            if (!string.IsNullOrWhiteSpace(module))
            {
                Modules.Add(module);
            }
        }

        public override string ToString()
        {
            return $"Course: {Title}, Price: {GetPrice():C}, Modules: {string.Join(", ", Modules)}";
        }

        // Composite Pattern: Leaf methods
        public void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + $" Course: {Title} (Price: {GetPrice():C})");
        }

        public void Add(ICourseComponent component)
        {
            throw new NotSupportedException("Cannot add a component to a leaf course.");
        }

        public void Remove(ICourseComponent component)
        {
            throw new NotSupportedException("Cannot remove a component from a leaf course.");
        }
    }
}
