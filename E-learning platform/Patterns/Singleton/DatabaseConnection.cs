using System;

namespace E_learning_platform.Patterns.Singleton
{
    public class DatabaseConnection
    {
        private static DatabaseConnection? _instance;
        private static readonly object _lock = new object();
        public bool IsConnected { get; private set; }

        // Private constructor to prevent external instantiation
        private DatabaseConnection()
        {
            Console.WriteLine("Database Connection Initialized.");
            IsConnected = false;
        }

        public static DatabaseConnection Instance
        {
            get
            {
                // Double-check locking for thread safety and performance
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseConnection();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Connect()
        {
            if (!IsConnected)
            {
                // Simulate connection logic
                IsConnected = true;
                Console.WriteLine("Database connected.");
            }
            else
            {
                Console.WriteLine("Already connected.");
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                // Simulate disconnection logic
                IsConnected = false;
                Console.WriteLine("Database disconnected.");
            }
            else
            {
                Console.WriteLine("Already disconnected.");
            }
        }
    }
}
