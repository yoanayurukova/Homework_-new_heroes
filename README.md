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

using Homework_new_heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework__new_heroes
{
    public class GameEventListener
    {
        public virtual void GameRound(Hero attacker, Hero defender, int attack)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_new_heroes
{
    public  class Hero
    {

        Random random = new Random();
        public string Name { get; private set; }

        public int Health { get; protected set; }

        public int Strength { get;  protected set; }


        public Hero(string name)
        {
            Name = name;
            Health = 500;
            Strength = 60;

        }

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
           
        }

        public bool IsDead
        {
            get
            {
                return !IsAlive;
            }
        }

        public virtual int Attack()
        {
        
            int coef = random.Next(80, 121);
            return (coef * Strength) / 100;

        }

        public virtual void TakeDamage(int incomingDamage)
        {
            Health = Health - incomingDamage;
        }      

        public int GetStrength() // add a method to access Strength
        {
            return Strength;
        }

        public void SetStrength(int strength)  // add a chance method to Strength
        {
            Strength = strength;
        }
    }
}
    


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
using Homework__new_heroes;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Homework_new_heroes
{
    class ConsoleGameEventListener : GameEventListener
    {
        public override void GameRound(Hero attacker, Hero defender, int attack)
        {
            string message = $"{attacker.Name} attacker {defender.Name} for {attack} points";
            if (defender.IsAlive)
            {
                message += message + $" but {defender.Name} survived.";

            }
            else
            {
                message += message + $" and {defender.Name} died.";
            }
            Console.WriteLine(message);

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Knight knight = new  Knight("Sir  John");
            Rogue rogue = new Rogue("Slim Shady");

            // Creating new characters

            EvilStepmother evilStepmother = new EvilStepmother("Evil Queen ");
            Fairy fairy = new Fairy("Ondina");

            // Creating an arena

            Arena arena = new Arena (knight, rogue , evilStepmother, fairy );
            arena.EventListener = new ConsoleGameEventListener();

            // Creating a battle

            Console.WriteLine("Battle begins between Knight,Rogue, EvilStepmother and Fairy");
            Hero winner = arena.BattleWithEveryone();
            Console.WriteLine($"Battle ended.  Winner is: {winner.Name}");
           

            Console.ReadLine();
        }
    }
}


        

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
