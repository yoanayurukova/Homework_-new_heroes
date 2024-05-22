using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{
    public class Fairy : Hero
    {
        Random random = new Random();

        private int healingTurns;

        public Fairy() : this ("Fairy")
        {

        }

        public Fairy (string name) : base(name)
        {
            random = new Random();
            Health = 400;
            Strength = 50;
            healingTurns = 0;
        }

        public override int Attack()
        {
            int baseAttack = base.Attack();
            int chance = random.Next(0, 100);
            if (chance < 50)
            {
                baseAttack += Strength * 2;

            }
            return baseAttack;
        }
        public override void TakeDamage(int incomingDamage ) 
        {
            if( healingTurns > 0 )
            {
                int heal = random.Next(10, 31);
                Health += heal;
                healingTurns--;
            }
            base.TakeDamage(incomingDamage);

        }

        public void Heal()
        {
            healingTurns = 3;
            Console.WriteLine($"{Name} is healing over the next 3 turns");
        }
    }
}

