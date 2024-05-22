using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{
    public  class Arena
    {
        public Hero HeroA {  get; private set; }
        public Hero HeroB { get; private set; }
        public Hero HeroC { get; private set; }
        public Hero HeroD { get; private set; }

        public GameEventListener EventListener { get; set; }

        public Arena (Hero a,Hero b, Hero c, Hero d)
        {
            HeroA = a;
            HeroB = b;
            HeroC = c;
            HeroD = d;
        }
       public Hero Battle()
        {
            Hero attacker, defender;
            attacker = HeroA;
            defender = HeroB;
             


            while (true)
            {
                int damage = attacker.Attack();
                defender.TakeDamage(damage);

                if (EventListener != null)

                { 
                    
                    EventListener.GameRound(attacker, defender, damage);

                }

                if (defender.IsDead) return attacker;

                Hero temp = attacker;
                attacker = defender;
                defender = temp;
                
            }
        }

        public Hero BattleWithEveryone()
        {
            Hero[] heroes = { HeroA, HeroB, HeroC, HeroD };
            int aliveCount = heroes.Length;

            while (aliveCount > 1)
            {
                for(int i = 0; i < heroes.Length; i++) 
                {
                    if (heroes[i].IsDead) continue;

                    Hero attacker = heroes[i];

                    for (int m = 0; m < heroes.Length; m++)
                    {
                        if ( i == m || heroes[m].IsDead) continue;
                       
                        Hero defender = heroes[m];
                        int damage = attacker.Attack();
                        defender.TakeDamage(damage);

                        if(EventListener != null)
                        {
                            EventListener.GameRound(attacker,defender,damage);
                        }

                        if(defender.IsDead)
                        {
                            aliveCount--;
                        }

                        if(aliveCount <= 1) break;
                    }
                    if(aliveCount <= 1) break;
                }
            }
            return heroes.First(h => h.IsAlive);


          
        }
    }
}
