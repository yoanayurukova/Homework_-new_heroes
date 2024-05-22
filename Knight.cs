using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{

    //- Has armor, which reduces the incoming damage between 20 and 60%.
    //- Every 5 hits can cause double damage.
    public class Knight : Hero
    {

        Random random = new Random();

        public Knight() : this ("Sir John")
        { 

        }

        public Knight (string name) : base (name)  //A named knight, we supply the name
        { 
            hitCount = 0;
        }

        private int hitCount;
        public override int Attack()
        {
            hitCount = hitCount + 1;
            int baseAttack = base.Attack();
            if(hitCount == 5)
            {
                baseAttack *= 2;
                hitCount = 0;
            }
            return baseAttack;
        }

        public override void TakeDamage( int incomingDamage)
        {
            int coef = random.Next(20, 61);
            incomingDamage = incomingDamage - (coef * incomingDamage) / 100;
            base.TakeDamage(incomingDamage);

        }
    }
}
