using System;

namespace E_learning_platform.Patterns.Flyweight
{
    public class CharacterFlyweight : ICharacterFlyweight
    {
        private readonly char _symbol;

        // Intrinsic state: the character symbol
        public CharacterFlyweight(char symbol)
        {
            _symbol = symbol;
        }

        // Extrinsic state: font and size passed in by the client
        public void Draw(string font, int size)
        {
            Console.WriteLine($"Drawing '{_symbol}' in {font} at size {size}");
        }
        
        public char GetSymbol() => _symbol;
    }
}
