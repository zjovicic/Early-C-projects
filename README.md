# Early C# programs
C# programs I made while learning

### Fountain of Objects.cs
This is my implementation of the (in)famous Fountain of Objects challange from RB Whitaker's C# Player's Guide book, all with 6 expansion packs.
It is a console game in which the player navigates a grid of caverns, filled with monsters and traps, and searches for Fountain of Objects.
To win the game the player needs to find the Fountain, activate it, and return to the entrance, while avoiding the monsters and traps that are everywhere.

### Game of Life.cs
C# implementation of Conway's Game of Life. The game obeys the following rules:
    1. Any live cell with fewer than two live neighbors dies, as if by underpopulation.
    2. Any live cell with two or three live neighbors lives on to the next generation.
    3. Any live cell with more than three live neighbors dies, as if by overpopulation.
    4. Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
This is implemented as a console application on a 30x60 grid.
Living cells are represented by "#" while dead cells are represented by spaces " ".
