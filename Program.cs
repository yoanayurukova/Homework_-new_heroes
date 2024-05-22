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


        

