namespace E_learning_platform.Models
{
    public abstract class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        protected User(int id, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new System.ArgumentException("Email cannot be empty.", nameof(email));
            
            Id = id;
            Name = name;
            Email = email;
        }

        public abstract string GetRole();
    }
}
