using System.Collections.Generic;

namespace E_learning_platform.Patterns.Flyweight
{
    public class CharacterFactory
    {
        private readonly Dictionary<char, ICharacterFlyweight> _characters = new Dictionary<char, ICharacterFlyweight>();

        public ICharacterFlyweight GetCharacter(char symbol)
        {
            if (!_characters.ContainsKey(symbol))
            {
                _characters[symbol] = new CharacterFlyweight(symbol);
            }
            return _characters[symbol];
        }

        public int GetTotalCharactersCreated()
        {
            return _characters.Count;
        }
    }
}
