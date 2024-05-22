using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{
    public class EvilStepmother : Hero
    {
        private Random random = new Random();
        private int CurseDuration;

        public EvilStepmother() : this("EvilStepmother")
        {
            
        }

        public EvilStepmother(string name) : base (name)
        {
            CurseDuration = 0;
            random = new Random();
        }

        public override int Attack()
        {
            int baseAttack = base.Attack();
            int chance = random.Next(0, 100);
            if (chance < 20)
            {
                baseAttack *= 4;
            }
            return baseAttack;
        }

        public override void TakeDamage(int incomingDamage)
        {
            if(CurseDuration > 0)
            {
                incomingDamage += 20;
                CurseDuration--;
            }
            base.TakeDamage(incomingDamage);
        }

        public void Curse (Hero opponent)
        {
            int opponentStrength = opponent.GetStrength(); // use the strength access method
            opponent.SetStrength(opponentStrength - 20); // use the chance to strength method
            CurseDuration = 3;
        }
    }
}

