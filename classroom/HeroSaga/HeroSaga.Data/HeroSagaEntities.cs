namespace HeroSaga.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HeroSagaEntities : DbContext
    {
        public HeroSagaEntities()
            : base("name=HeroSagaEntities")
        {
        }

        public virtual DbSet<Battle> Battles { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharactersStat> CharactersStats { get; set; }
        public virtual DbSet<Stat> Stats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Character>()
                .Property(e => e.Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Character>()
                .HasMany(e => e.Battles)
                .WithRequired(e => e.Character)
                .HasForeignKey(e => e.CharacterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasMany(e => e.Challenges)
                .WithRequired(e => e.EnemyCharacter)
                .HasForeignKey(e => e.EnemyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasMany(e => e.CharactersStats)
                .WithRequired(e => e.Character)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stat>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Stat>()
                .HasMany(e => e.CharactersStats)
                .WithRequired(e => e.Stat)
                .WillCascadeOnDelete(false);
        }
    }
}
