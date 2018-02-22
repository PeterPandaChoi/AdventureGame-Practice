using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdventureGame_Practice
{
    class UI : Map
    {
        public static string willRestart = "y";//Restart String
        public static void WriteBlue(string words)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(words);
            Console.ResetColor();
        }//Blue
        public static void WriteRed(string words)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(words);
            Console.ResetColor();
        }//Red
        public static void WriteGreen(string words)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(words);
            Console.ResetColor();
        }//Green
        public static void WriteGray(string words)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(words);
            Console.ResetColor();
        }//Gray
        public static void Gameover()
        {
            WriteGray("\n      G A M E O V E R\n \n");
            WriteGray("If you want to start over type \'y\'");
            willRestart = Console.ReadLine();
        }//GameOver
        public static void CommandHelp()
        {
            WriteGreen("In order to move type commands below");
            WriteGray("move - move one block to direction you desire");
            WriteGray("attack - set the direction you want to attack");
            WriteGray("quit - you can quit the game");
            WriteGray("grab - grab the item you are stepping on");
        }//display commands
        public static void DisplayMap()
        {
            WriteGray("------------map------------");
            for (int i = 0; i < mapSize; i++)
            {
                Console.Write('-');
                for (int j = 0; j < mapSize; j++)
                {

                    Console.Write(coor[i, j]);
                }
                Console.Write('-');
                Console.Write('\n');
            }
            WriteGray("------------end------------");
                    
            return;//아직 implement 안됨
        }
    }
    class Map
    {
        public static int mapSize = 10;
        enum MapObject { empty, player, enemy, heal };
        public static int[,] coor = new int[mapSize,mapSize];

        public static void InitMap()
        {
            for (int i = 0; i < 10;i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    coor[i, j] = (int)MapObject.empty;
                }
                    
            }
            coor[0, 0] = (int)MapObject.player;
        }
        
    }

    class Program : UI
    {
        static string gameName = "Adventure Game Practice";

        static void Main(string[] args)
        {
            //Initialize the Window and WELCOME!~
            WriteRed("\n           ----------  Welcome to the "+gameName+"  ---------- \n");
            WriteBlue("Enjoy your journey through the land of _______");
            //Console.Title = gameName;



            willRestart = "y";//init restart string
            while (willRestart == "y")
            {
                //Initialize the Game
                Player player = new Player();
                Enemy enemy = new Enemy();
                InitMap();
                DisplayMap();

                //actual Game Loop
                for (bool moveAgain = true; moveAgain; moveAgain = ChooseMove(player, enemy))
                {
                    Console.Title = gameName+" / Player Health : " + player.health;
                }
                
                //Ask if Restart
                WriteGray("Do you want to Restart?");
                if (Console.ReadLine() == "y")
                {
                    willRestart = "y";
                }
                else willRestart = "n";

            }
        }
        static bool ChooseMove(Player player, Enemy enemy)
        {
            WriteGray("Choose your move :\n>>>> ");
            string Choice = Console.ReadLine();
            switch (Choice)
            {
                case "move":
                    WriteGray("Which direction you want to move? (left, right, up, down");
                    player.directionMove(player);
                    return true;
                case "attack":
                    player.Attack(enemy, player.weapon);
                    return true;
                case "quit":
                    WriteGray("Do you Want to Quit? y/n");
                    if (Console.ReadLine() == "y")
                        return false;
                    else return true;
                case "grab":
                    //if 회복 아이템을 주우면 회복
                    //if 무기를 주우면 change Weapon
                    //player.ChangeWeapons(wp2);
                    return true;
                default://show the status
                    WriteBlue("Player health = " + player.health);
                    WriteBlue("Player's Weapon " + player.weapon);
                    CommandHelp();

                    return true;
            }
            return false;
        }

    }

    class Player : UI
    {
        public int health = 100;

        
        public Weapons weapon = new Weapons("hand");

        public void ChangeWeapons(Weapons wp)
        {
            Console.WriteLine("Changing Weapon from {0} to {1}", weapon.kind, wp.kind);
            weapon = wp; 
            wp.Init();
        }
        public void directionMove(Player player)
        {
            
        }

        public void Attack(Enemy target, Weapons wp)
        {
            WriteBlue("Attacking "+target.name+" with "+wp.damage+" Damage");
            target.health -= wp.damage;
            WriteGray(target.name + "'s health : " + target.health);

            if (target.health <= 0)
            {
                WriteGreen(target.name + "is Dead");
                target = null;//왜 이거 안되지?
            }
        }
        
    }

    class Weapons
    {
        public int ammo;
        public int maxAmmo;
        public string kind = "hand";
        public int damage = 10;

        public Weapons(string weaponKind)
        {
            switch (weaponKind)
            {
                default:
                    ammo = 0;
                    maxAmmo = 0;
                    kind = "hand";
                    damage = 5;
                    break;
                case "hand":
                    ammo = 0;
                    maxAmmo = 0;
                    kind = "hand";
                    damage = 5;
                    break;
                case "sword":
                    ammo = 0;
                    maxAmmo = 0;
                    kind = "sword";
                    damage = 30;
                    break;
                case "gun":
                    ammo = 100;
                    maxAmmo = 100;
                    kind = "gun";
                    damage = 20;
                    break;
            }
        }
        public Weapons(int ammo, int max, string kind, int dam)
        {
            this.ammo = ammo;
            this.maxAmmo= max;
            this.kind = kind;
            this.damage = dam;
        }
        public void Init()
        {
            ammo = maxAmmo;
        }
    }


    class Enemy : UI
    {
        public string name = "Zombie";
        public int health = 50;
        public int damage = 10;

        public Enemy()
        {
            WriteGray(name +" Generated");
        }

        public Enemy(string name, int health, int damage)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            WriteGray(name+" Generated");
        }

        public void Attack(Player player)
        {
            player.health -= this.damage;
            if (player.health <= 0)
            {
                Gameover();
            }
        }
        

    }

}
