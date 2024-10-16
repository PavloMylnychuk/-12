using System;
using System.Collections.Generic;

namespace KingsGambitExtended
{
    public class King
    {
        public string Name { get; }

        public King(string name)
        {
            Name = name;
        }

        public void UnderAttack()
        {
            Console.WriteLine($"King {Name} is under attack!");
        }
    }

    public class RoyalGuard
    {
        public string Name { get; }
        public int Hits { get; private set; }
        public bool IsAlive => Hits < 3;

        public RoyalGuard(string name)
        {
            Name = name;
            Hits = 0;
        }

        public void Defend()
        {
            if (IsAlive)
            {
                Console.WriteLine($"Royal Guard {Name} is defending!");
            }
        }

        public void Kill()
        {
            Hits++;
            if (!IsAlive)
            {
                Console.WriteLine($"Royal Guard {Name} has died!");
            }
        }
    }

    public class Footman
    {
        public string Name { get; }
        public int Hits { get; private set; }
        public bool IsAlive => Hits < 2;

        public Footman(string name)
        {
            Name = name;
            Hits = 0;
        }

        public void Panic()
        {
            if (IsAlive)
            {
                Console.WriteLine($"Footman {Name} is panicking!");
            }
        }

        public void Kill()
        {
            Hits++;
            if (!IsAlive)
            {
                Console.WriteLine($"Footman {Name} has died!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string kingName = Console.ReadLine();
            King king = new King(kingName);

            string[] royalGuardsNames = Console.ReadLine().Split(' ');
            List<RoyalGuard> royalGuards = new List<RoyalGuard>();
            foreach (var name in royalGuardsNames)
            {
                royalGuards.Add(new RoyalGuard(name));
            }

            string[] footmenNames = Console.ReadLine().Split(' ');
            List<Footman> footmen = new List<Footman>();
            foreach (var name in footmenNames)
            {
                footmen.Add(new Footman(name));
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                if (command == "Attack King")
                {
                    king.UnderAttack();
                    foreach (var guard in royalGuards)
                    {
                        guard.Defend();
                    }
                    foreach (var footman in footmen)
                    {
                        footman.Panic();
                    }
                }
                else if (command.StartsWith("Kill "))
                {
                    string nameToKill = command.Substring(5);
                    var guard = royalGuards.Find(g => g.Name == nameToKill);
                    if (guard != null)
                    {
                        guard.Kill();
                        continue;
                    }

                    var footman = footmen.Find(f => f.Name == nameToKill);
                    if (footman != null)
                    {
                        footman.Kill();
                    }
                }
            }
        }
    }
}
