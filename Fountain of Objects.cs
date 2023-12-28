DateTime start = DateTime.Now;
TimeSpan timeInCavern = TimeSpan.Zero;
bool victory = false;
bool gameOver = false;
Fountain fountainOfObjects = new Fountain();
Room[,] rooms = new Room[0, 0];
Player player = new Player();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.");
Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns.");
Console.WriteLine("You must navigate the Caverns with your other senses.");
Console.WriteLine("Find the Fountain of Objects, activate it, and return to the entrance.");
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("Look out for pits. You will feel a breeze if a pit is in an adjacent room.\nIf you enter a room with a pit, you will die.");
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.DarkCyan;
Console.WriteLine("Maelstroms are violent forces of sentient wind. Entering a room with one could transport you to any other location \nin the caverns. You will be able to hear their growling and groaning in nearby rooms.");
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms.");
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("You carry with you a bow and a quiver of arrows. \nYou can use them to shoot monsters in the caverns but be warned: you have a limited supply");
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("You can use the following commands: move north, move south, move west, move east and enable fountain.");
Console.WriteLine("You can also shoot the monsters with the following commands: shoot north, shoot south, shoot west and shoot east!");
Console.WriteLine();
Console.WriteLine("If you forget any commands, you can get a list of all commands by typing help!");
Console.WriteLine();

string selectedSize = "";
while(selectedSize != "1" && selectedSize != "2" && selectedSize != "3")
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("For small (4x4) world type 1, medium (6x6) world type 2, and large (8x8) world type 3. ");
    selectedSize = Console.ReadLine();
    switch (selectedSize)
    {
        case "1":
            rooms = new Room[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rooms[i, j] = new Room();
                }
            }
            rooms[0, 0].Type = RoomType.Entrance;
            rooms[0, 2].Type = RoomType.Fountain;
            rooms[2, 2].Type = RoomType.Pit;
            rooms[1, 1].Type = RoomType.Maelstorm;
            rooms[0, 1].Type = RoomType.Amarok;
            break;
        case "2":
            rooms = new Room[6, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    rooms[i, j] = new Room();
                }
            }
            rooms[0, 0].Type = RoomType.Entrance;
            rooms[3, 2].Type = RoomType.Fountain;
            rooms[1, 2].Type = RoomType.Pit;
            rooms[4, 1].Type = RoomType.Pit;
            rooms[2, 0].Type = RoomType.Maelstorm;
            rooms[3, 1].Type = RoomType.Maelstorm;
            rooms[1, 0].Type = RoomType.Amarok;
            rooms[0, 2].Type = RoomType.Amarok;
            break;
        case "3":
            rooms = new Room[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    rooms[i, j] = new Room();
                }
            }
            rooms[0, 0].Type = RoomType.Entrance;
            rooms[2, 5].Type = RoomType.Fountain;
            rooms[5, 4].Type = RoomType.Pit;
            rooms[6, 2].Type = RoomType.Pit;
            rooms[3, 3].Type = RoomType.Pit;
            rooms[1, 2].Type = RoomType.Pit;
            rooms[2, 1].Type = RoomType.Maelstorm;
            rooms[2, 3].Type = RoomType.Maelstorm;
            rooms[1, 4].Type = RoomType.Maelstorm;
            rooms[4, 3].Type = RoomType.Maelstorm;
            rooms[0, 3].Type = RoomType.Amarok;
            rooms[2, 2].Type = RoomType.Amarok;
            rooms[4, 5].Type = RoomType.Amarok;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Wrong command!");
            break;
    }
}


while(victory == false && gameOver == false)
{
    Console.WriteLine();
    DisplayInformation();
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Maelstorm)
        Maelstorm();
    ReceiveCommand();
    DetectVictoryOrDeath();
}    

void ReceiveCommand()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("What do you want to do? ");
    string command = Console.ReadLine().ToLower();
    switch (command)
    {
        case "move east":
            player.MoveEast(rooms);
            break;
        case "move west":
            player.MoveWest();
            break;
        case "move north":
            player.MoveNorth();
            break;
        case "move south":
            player.MoveSouth(rooms);
            break;
        case "enable fountain":
            player.EnableFountain(fountainOfObjects, rooms[player.Position.X, player.Position.Y]);
            break;
        case "shoot north":
            player.ShootNorth(rooms);
            break;
        case "shoot south":
            player.ShootSouth(rooms);
            break;
        case "shoot east":
            player.ShootEast(rooms);
            break;
        case "shoot west":
            player.ShootWest(rooms);
            break;
        case "help":
            Help();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Wrong command!");
            break;
    }
}

void Help()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("You can use the following commands to navigate the mage: \nmove north, \nmove south, \nmove west, \nmove east.");
    Console.WriteLine("Those commands will move you to the adjacent room, in the desired direction!");
    Console.WriteLine("Pay attention as you can't move outside the maze!");
    Console.WriteLine("To enable fountain you can type \"enable fountain\". You can only enable fountain when you're in the fountain room!");
    Console.WriteLine("You can also shoot the monsters with the following commands: \nshoot north, \nshoot south, \nshoot west and \nshoot east!");
    Console.WriteLine("You can only kill maelstorms and amaroks, but not pits!");
    Console.WriteLine("Pay attention not to wastefully spend your arrows!");
}

void DisplayInformation()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");
    Console.WriteLine($"You have {player.Arrows} arrows left!");
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Entrance && victory == false)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You see light coming from the cavern entrance!");
    }
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Fountain && fountainOfObjects.IsOn == false)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
    }
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Fountain && fountainOfObjects.IsOn == true)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
    }
    if (FindRoom(player.Position.X, player.Position.Y, RoomType.Pit))
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
    }
    if (FindRoom(player.Position.X, player.Position.Y, RoomType.Maelstorm))
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("You hear the growling and groaning of a maelstrom nearby.");
    }
    if (FindRoom(player.Position.X, player.Position.Y, RoomType.Amarok))
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room.");
    }
}

void Maelstorm()
{
    int maelstormX = player.Position.X;
    int maelstormY = player.Position.Y;
    rooms[player.Position.X, player.Position.Y].Type = RoomType.Normal;

    maelstormX++;
    player.Position.X--;
    if (player.Position.X < 0)
        player.Position.X = 0;
    if (maelstormX > rooms.GetLength(0) - 1)
        maelstormX = rooms.GetLength(0) - 1;

    maelstormY -= 2;
    player.Position.Y += 2;
    if (player.Position.Y > rooms.GetLength(1) - 1)
        player.Position.Y = rooms.GetLength(1) - 1;
    if (maelstormY < 0)
        maelstormY = 0;

    rooms[maelstormX, maelstormY].Type = RoomType.Maelstorm;

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("The wild Maelstorm has moved you, and it escaped in the opposite direction!");
    DisplayInformation();
}

void DetectVictoryOrDeath()
{
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Entrance && fountainOfObjects.IsOn == true)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
        Console.WriteLine("You win!");
        timeInCavern = DateTime.Now - start;
        Console.WriteLine($"You've spent {timeInCavern.Hours} hours, {timeInCavern.Minutes} minutes and {timeInCavern.Seconds} seconds playing the game!");
        victory = true;
    }
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Pit)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You fell into the pit. You're dead!");
        Console.WriteLine("Game Over.");
        timeInCavern = DateTime.Now - start;
        Console.WriteLine($"You've spent {timeInCavern.Hours} hours, {timeInCavern.Minutes} minutes and {timeInCavern.Seconds} seconds playing the game!");
        gameOver = true;
    }
    if (rooms[player.Position.X, player.Position.Y].Type == RoomType.Amarok)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The hungry Amarok has eaten you! You're dead!");
        Console.WriteLine("Game Over.");
        timeInCavern = DateTime.Now - start;
        Console.WriteLine($"You've spent {timeInCavern.Hours} hours, {timeInCavern.Minutes} minutes and {timeInCavern.Seconds} seconds playing the game!");
        gameOver = true;
    }
}

bool FindRoom (int x, int y, RoomType type)
{
    int minX = x - 1;
    int minY = y - 1;
    int maxX = x + 1;
    int maxY = y + 1;

    if (x == 0)
        minX = 0;
    if (y == 0)
        minY = 0;
    if (x == rooms.GetLength(0) - 1)
        maxX = x;
    if (y == rooms.GetLength(1) - 1)
        maxY = y;

    if (rooms[x, y].Type == type)
        return false;

    for (int i = minX; i <= maxX; i++)
    {
        for (int j = minY; j <= maxY; j++)
        {
            if (rooms[i, j].Type == type)
                return true;
        }
    }
    return false;
}

public enum RoomType { Normal, Entrance, Fountain, Pit, Maelstorm, Amarok}

public class Room
{
    public RoomType Type { get; set; }
    public Room ()
    {
        Type = RoomType.Normal;
    }
}

public class Fountain
{
    public bool IsOn { get; set; }
    public Fountain ()
    {
        IsOn = false;
    }
}

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Player
{
    public Position Position { get; set; }

    public int Arrows { get; set; }


    public Player ()
    {
        Position = new Position(0, 0);
        Arrows = 5;
    }

    public void EnableFountain(Fountain fountain, Room room)
    {
        if (room.Type == RoomType.Fountain)
        {
            fountain.IsOn = true;
        }
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can't enable fountain from this room!");
        }
    }

    public void MoveNorth()
    {
        if (Position.X > 0)
            Position.X--;
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can't move out of the grid!");
        }
    }

    public void ShootNorth(Room[,] rooms)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (Arrows > 0)
        {
            if (Position.X > 0 && rooms[Position.X - 1, Position.Y].Type != RoomType.Normal && rooms[Position.X - 1, Position.Y].Type != RoomType.Entrance && rooms[Position.X - 1, Position.Y].Type != RoomType.Pit && rooms[Position.X - 1, Position.Y].Type != RoomType.Fountain)
            {
                Console.WriteLine($"You've killed a nasty {rooms[Position.X - 1, Position.Y].Type} located at Row = {Position.X - 1}, Column = {Position.Y} with your arrow!");
                rooms[Position.X - 1, Position.Y].Type = RoomType.Normal;
            }
            else if (Position.X > 0 && rooms[Position.X - 1, Position.Y].Type == RoomType.Normal)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow shooting into an empty room!");
            }
            else if (Position.X > 0 && rooms[Position.X - 1, Position.Y].Type == RoomType.Entrance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow shooting into an entrance!");
            }
            else if (Position.X > 0 && rooms[Position.X - 1, Position.Y].Type == RoomType.Pit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow! Unfortunately, you can't kill a pit!");
            }
            else if (Position.X > 0 && rooms[Position.X - 1, Position.Y].Type == RoomType.Fountain)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow shooting into a fountain! Luckily, you can't destroy it!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow shooting outside the grid!");
            }
            Arrows--;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You're out of arrows! You can't shoot!");
        }
    }

    public void MoveWest()
    {
        if (Position.Y > 0)
            Position.Y--;
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can't move out of the grid!");
        }
    }

    public void ShootWest(Room[,] rooms)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (Arrows > 0)
        {
            if (Position.Y > 0 && rooms[Position.X, Position.Y - 1].Type != RoomType.Normal && rooms[Position.X, Position.Y - 1].Type != RoomType.Pit && rooms[Position.X, Position.Y - 1].Type != RoomType.Entrance && rooms[Position.X, Position.Y - 1].Type != RoomType.Fountain)
            {
                Console.WriteLine($"You've killed a nasty {rooms[Position.X, Position.Y - 1].Type}, located at Row = {Position.X}, Column = {Position.Y - 1} with your arrow!");
                rooms[Position.X, Position.Y - 1].Type = RoomType.Normal;
            }
            else if (Position.Y > 0 && rooms[Position.X, Position.Y - 1].Type == RoomType.Normal)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an empty room!");
            }
            else if (Position.Y > 0 && rooms[Position.X, Position.Y - 1].Type == RoomType.Pit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow! Unfortunately you can't kill a pit!");
            }
            else if (Position.Y > 0 && rooms[Position.X, Position.Y - 1].Type == RoomType.Entrance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an entrance!");
            }
            else if (Position.Y > 0 && rooms[Position.X, Position.Y - 1].Type == RoomType.Fountain)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into the fountain! Luckily you can't destroy it!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting outside the grid!");
            }
            Arrows--;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You are out of arrows, you can't shoot!");
        }
    }

    public void MoveSouth(Room[,] rooms)
    {
        if (Position.X < rooms.GetLength(0) - 1)
            Position.X++;
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("You can't move out of the grid!");
        }
    }

    public void ShootSouth(Room[,] rooms)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (Arrows > 0)
        {
            if (Position.X < rooms.GetLength(0) - 1 && rooms[Position.X + 1, Position.Y].Type != RoomType.Normal && rooms[Position.X + 1, Position.Y].Type != RoomType.Pit && rooms[Position.X + 1, Position.Y].Type != RoomType.Entrance && rooms[Position.X + 1, Position.Y].Type != RoomType.Fountain)
            {
                Console.WriteLine($"You've killed a nasty {rooms[Position.X + 1, Position.Y].Type}, located at Row = {Position.X + 1}, Column = {Position.Y} with your arrow!");
                rooms[Position.X + 1, Position.Y].Type = RoomType.Normal;
            }
            else if (Position.X < rooms.GetLength(0) - 1 && rooms[Position.X + 1, Position.Y].Type == RoomType.Normal)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an empty room!");
            }
            else if (Position.X < rooms.GetLength(0) - 1 && rooms[Position.X + 1, Position.Y].Type == RoomType.Pit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow! Unfortunately, you can't kill a pit!");
            }
            else if (Position.X < rooms.GetLength(0) - 1 && rooms[Position.X + 1, Position.Y].Type == RoomType.Entrance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an entrance!");
            }
            else if (Position.X < rooms.GetLength(0) - 1 && rooms[Position.X + 1, Position.Y].Type == RoomType.Fountain)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into the fountain! Luckily, you can't destroy it!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting outside the grid!");
            }
            Arrows--;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You are out of arrows, you can't shoot!");
        }
    }

    public void MoveEast(Room[,] rooms)
    {
        if (Position.Y < rooms.GetLength(1) - 1)
            Position.Y++;
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can't move out of the grid!");
        }
    }

    public void ShootEast(Room[,] rooms)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (Arrows > 0)
        {
            if (Position.Y < rooms.GetLength(1) - 1 && rooms[Position.X, Position.Y + 1].Type != RoomType.Normal && rooms[Position.X, Position.Y + 1].Type != RoomType.Pit && rooms[Position.X, Position.Y + 1].Type != RoomType.Entrance && rooms[Position.X, Position.Y + 1].Type != RoomType.Fountain)
            {
                Console.WriteLine($"You've killed a nasty {rooms[Position.X, Position.Y + 1].Type}, located at Row = {Position.X}, Column = {Position.Y + 1} with your arrow!");
                rooms[Position.X, Position.Y + 1].Type = RoomType.Normal;
            }
            else if (Position.Y < rooms.GetLength(1) - 1 && rooms[Position.X, Position.Y + 1].Type == RoomType.Normal)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an empty room!");
            }
            else if (Position.Y < rooms.GetLength(1) - 1 && rooms[Position.X, Position.Y + 1].Type == RoomType.Pit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, you can't kill a pit!");
            }
            else if (Position.Y < rooms.GetLength(1) - 1 && rooms[Position.X, Position.Y + 1].Type == RoomType.Entrance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into an entrance!");
            }
            else if (Position.Y < rooms.GetLength(1) - 1 && rooms[Position.X, Position.Y + 1].Type == RoomType.Fountain)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting into the fountain! Luckily, you can't destroy it!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've wasted an arrow, shooting outside the grid!");
            }
            Arrows--;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You are out of arrows, you can't shoot!");
        }
    }
}