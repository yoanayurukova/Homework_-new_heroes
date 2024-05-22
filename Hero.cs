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
    


           