using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{
    //- Has a 30% chance to avoid damage completely.
    //- Has a 30% chance to cause damage equal to 3 times his Strength.
    public class Rogue : Hero 
    {

        Random random = new Random();
        public Rogue(string name) : base (name) 
        { 

        }

        public override void TakeDamage(int incomingDamage)
        {
            int dice = random.Next(0, 100);
            if (dice <= 30)
            {
                incomingDamage = 0; 
            }
            base.TakeDamage(incomingDamage);
        }

        public override int Attack()
        {
            int baseAttack = base.Attack();
            int dice = random.Next(0, 100);
            if (dice <= 30)
            {
                baseAttack = Strength * 3;
            }
            return baseAttack;
        }
    }
}
