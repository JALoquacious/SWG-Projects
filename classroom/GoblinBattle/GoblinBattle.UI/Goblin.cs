using System;

namespace GoblinBattle.UI
{
    class Goblin
    {
        private static Random _rng = new Random();
        private int _hitPoints;
        private int _maxHealth = 1000;
        private bool _hasShield = false;
        private bool[] _chance = { true, false, false, false };
        private Weapon _weapon = Weapon.fist;

        public string Name { get; set; }
        public bool IsDead { get; private set; }

        public int HitPoints {
            get { return _hitPoints; }
            set
            {
                if (value < 0)
                    return;
                else if (value > _maxHealth)
                    _hitPoints = _maxHealth;
                else
                    _hitPoints = value;
            }
        }

        public bool HasShield
        {
            get { return _hasShield; }
            set
            {
                if (_hasShield) return;
                if (value) Console.WriteLine(this.Name + " picked up a shield!");
                else Console.WriteLine(this.Name + " lost his/her shield.");
                _hasShield = value;
            }
        }
        
        public bool Block(bool[] probability)
        {
            return probability[_rng.Next(0, probability.Length)];
        }

        public void ChangeWeapon(Weapon weapon = Weapon.fist)
        {
            if (_weapon == weapon) return;
            else if (weapon == Weapon.fist)
            {
                Console.WriteLine($"{this.Name} is unarmed and is using fists.");
            }
            else
            {
                Console.WriteLine($"{this.Name} picked up a {weapon}.");
                _weapon = weapon;
            }
        }

        public void UseRemedy(Remedy drink)
        {
            Console.WriteLine(this.Name + " drinks a " + drink + " and recovers "
                + (int)drink + " hit points.");
            HitPoints += (int)drink;
        }

        public void Attack(Goblin target)
        {
            int damage = _rng.Next(0, 100) * (int)_weapon;

            if (target.HasShield && target.Block(_chance))
            {
                Console.WriteLine(Name + " swings at " + target.Name +
                    ", but " + target.Name + "'s shield blocks the damage!");
            }
            else if (damage == 0)
            {
                Console.WriteLine(Name + " swings at " + target.Name +
                    ", but misses! Zero damage dealt.");
            }
            else
            {
                Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
                target.Hit(damage);
            }
            
        }

        public void Hit(int damage)
        {
            _hitPoints -= damage;
            Console.WriteLine($"{Name} receives {damage} damage. He/she has {_hitPoints} health.");

            if (_hitPoints <= 0)
            {
                Console.WriteLine($"{Name} has died!");
                IsDead = true;
            }
        }
    }
}
