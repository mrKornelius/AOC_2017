using AoCHelper;

namespace Namespace;
public class Day03 : BaseDay
{
    private readonly string _input;
    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        // in a 3x3 grid there are 9 cells (let cell #9 be the bottom right cell)
        // in a 5x5 grid there are 25 cells 
        // The grid grows by the odd squares so we can find the distance to
        // the input cell by finding the closest square thats bigger than the
        // input cell. So in a NxN grid the Manhattan distance to cell 1 is N-1 cells
        // from any corner. Find the closest corner and do the math...

        int x = int.Parse(_input);
        int sq_side = 1;
        while (sq_side * sq_side < x) sq_side++;
        int ans = sq_side - (sq_side * sq_side - x) - 1;
        return new(ans.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long input = int.Parse(_input);
        int dim = 11;
        long[,] grid = new long[dim, dim];
        (int x, int y) c = (dim / 2, dim / 2);
        int ydir = -1, xdir = 1;
        int ylim = 1, xlim = 1;

        grid[c.x, c.y] = 1;
        int lim = xlim;
        bool isX = true;


        while (grid[c.x, c.y] < input)
        {
            for (int i = 0; i < lim; ++i)
            {
                if (isX)
                {
                    c.x += xdir;
                }
                else
                {
                    c.y += ydir;
                }
                grid[c.x, c.y] = Calc(grid, c.x, c.y);
                if (grid[c.x, c.y] > input) break;
            }
            if (isX)
            {
                xdir *= -1;
                xlim++;
                lim = ylim;
            }
            else
            {
                ydir *= -1;
                ylim++;
                lim = xlim;
            }
            isX ^= true;
        }
        // PrintGrid(grid);

        return new(grid[c.x, c.y].ToString());
    }

    static long Calc(long[,] G, int x, int y)
    {
        long v = 0;
        for (int i = -1; i < 2; ++i)
            for (int j = -1; j < 2; ++j)
            {
                if (i == 0 && j == 0) continue;
                v += G[x + i, y + j];
            }
        return v;
    }
    static void PrintGrid(long[,] G)
    {
        long mx = 0;
        foreach (var row in G)
        {
            mx = Math.Max(mx, row);
        }
        int len = mx.ToString().Length;

        for (int i = 0; i < G.GetLength(1); ++i)
        {
            for (int j = 0; j < G.GetLength(0); ++j)
            {
                Console.Write($"{G[j, i],8}");
            }
            Console.WriteLine();
        }

    }
}
