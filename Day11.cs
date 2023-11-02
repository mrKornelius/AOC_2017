using AoCHelper;
using Spectre.Console;

namespace Namespace;
public class Day11 : BaseDay
{
    private string[] input;
    private (int r, int g, int b) furthestFromOrigin = (0, 0, 0);
    public Day11()
    {
        input = File.ReadAllText(InputFilePath).Split(',');
    }

    public override ValueTask<string> Solve_1()
    {
        int i = 0;
        (int r, int g, int b) p = (0, 0, 0);
        foreach (string dir in input)
        {
            switch (dir)
            {
                case "n":
                    p.g++; p.b--;
                    break;
                case "ne":
                    p.r++; p.b--;
                    break;
                case "se":
                    p.r++; p.g--;
                    break;
                case "s":
                    p.g--; p.b++;
                    break;
                case "sw":
                    p.r--; p.b++;
                    break;
                case "nw":
                    p.r--; p.g++;
                    break;
            }
            if (CalculateDistanceToOrigin(p) > CalculateDistanceToOrigin(furthestFromOrigin))
            {
                furthestFromOrigin = p;
            }
        }
        Console.WriteLine(p);
        return new((CalculateDistanceToOrigin(p)).ToString());
    }

    private static int CalculateDistanceToOrigin((int r, int g, int b) p)
    {
        return (Math.Abs(p.r) + Math.Abs(p.g) + Math.Abs(p.b)) / 2;
    }

    public override ValueTask<string> Solve_2()
    {
        return new(CalculateDistanceToOrigin(furthestFromOrigin).ToString());
    }
}
