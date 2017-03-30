using System.Collections.Generic;

namespace HeroSaga.Data
{
    public interface ICharacterRepo
    {
        Character CreateCharacter(Character model);
        void DeleteCharacter(int id);
        Character GetCharacter(int id);
        IEnumerable<Character> GetCharacters();
        void UpdateCharacter(Character model);
    }
}