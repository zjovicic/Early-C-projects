//The grid consisting of fields
Field[,] fields = new Field[30,60];

// Filling the grid with randomly assinged dead and alive cells.
Random random = new Random();
for (int i = 0; i < fields.GetLength(0); i++)
{
    for (int j = 0; j < fields.GetLength(1); j++)
    {
        if (random.Next(2) == 1)
            fields[i, j] = Field.Alive;
        else
            fields[i, j] = Field.Dead;
    }
}

// Mail loop of the simulation
while(true)
{
    PrintGrid(fields);
    Thread.Sleep(200);
    UpdateGrid(fields);
    Console.Clear();
}

// Method that determines the number of living neighboring cells of each cell of the grid
int NumberOfLivingNeighboors(Field[,] fields, int i, int j)
{
    int number = 0;
    // TOP LEFT
    if (i - 1 >= 0 && j - 1 >= 0)
    {
        if (fields[i - 1, j - 1] == Field.Alive)
        {
            number++;
        }
    }
    //TOP
    if (i - 1 >= 0)
        if (fields[i - 1, j] == Field.Alive)
            number++;
    // TOP RIGHT
    if (i - 1 >= 0 && j + 1 < fields.GetLength(1))
        if (fields[i - 1, j + 1] == Field.Alive)
            number++;
    // LEFT
    if (j - 1 >= 0)
        if (fields[i, j - 1] == Field.Alive)
            number++;
    // RIGHT
    if (j + 1 < fields.GetLength(1))
        if (fields[i, j + 1] == Field.Alive)
            number++;
    // BOTTOM LEFT
    if (i + 1 < fields.GetLength(0) && j - 1 >= 0)
        if (fields[i + 1, j - 1] == Field.Alive)
            number++;
    // BOTTOM
    if (i + 1 < fields.GetLength(0))
        if (fields[i + 1, j] == Field.Alive)
            number++;
    // BOTTOM RIGHT
    if (i + 1 < fields.GetLength(0) && j + 1 < fields.GetLength(1))
        if (fields[i + 1, j + 1] == Field.Alive)
            number++;
    return number;
}
// Method that updates the grid according to the rules of Conway's game of life
void UpdateGrid(Field[,] fields)
{
    int numLN;
    Field[,] oldfields = new Field[fields.GetLength(0), fields.GetLength(1)];
    for (int i = 0; i < fields.GetLength(0); i++)
    {
        for(int j = 0; j < fields.GetLength(1); j++)
        {
            oldfields[i, j] = fields[i, j];
        }
    }

    for (int i = 0; i < fields.GetLength(0); i++)
    {
        for (int j = 0; j < fields.GetLength(1); j++)
        {
            numLN = NumberOfLivingNeighboors(oldfields, i, j);
            if (oldfields[i, j] == Field.Alive)
            {
                if (numLN < 2 || numLN > 3)
                    fields[i, j] = Field.Dead;
            }
            else
            {
                if (numLN == 3)
                    fields[i, j] = Field.Alive;
            }
        }
    }
}
// Method that prints the grid
void PrintGrid(Field[,] fields)
{
    for (int i = 0; i < fields.GetLength(0); i++)
    {
        for (int j = 0; j < fields.GetLength(1); j++)
        {
            if (fields[i, j] == Field.Dead)
                Console.Write(" ");
            else
                Console.Write("#");
        }
        Console.WriteLine();
    }
}
// Enumeration of possible states of the grid
enum Field {Dead, Alive};