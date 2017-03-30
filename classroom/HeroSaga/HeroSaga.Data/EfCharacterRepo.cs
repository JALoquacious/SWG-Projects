using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSaga.Data
{
    public class EfCharacterRepo : ICharacterRepo
    {
        HeroSagaEntities context;
        public EfCharacterRepo()
        {
            context = new HeroSagaEntities();
        }
        public IEnumerable<Character> GetCharacters()
        {
            IQueryable<Character> query = from c in context.Characters.Include("Battles")
                                          where c.Desc.Contains("Dragon")
                select c;

    
            return query.ToList();
        }

        public Character GetCharacter(int id)
        {
            return context.Characters.Find(id);
        }

        public Character CreateCharacter(Character model)
        {
            context.Characters.Add(model);
            context.SaveChanges();
            return model;
        }

        public void UpdateCharacter(Character model)
        {
            context.Entry(model).State= EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCharacter(int id)
        {
            Character character = GetCharacter(id);
            context.Characters.Remove(character);
            context.SaveChanges();
        }
    }
}
