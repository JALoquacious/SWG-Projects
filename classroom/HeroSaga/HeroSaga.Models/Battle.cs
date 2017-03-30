namespace HeroSaga.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Battle
    {
        public int Id { get; set; }

        public int CharacterId { get; set; }

        public int EnemyId { get; set; }

        public bool? IsWon { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        public int ExpGained { get; set; }

        public virtual Character Character { get; set; }

        public virtual Character EnemyCharacter { get; set; }
    }
}
