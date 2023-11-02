using AoCHelper;

namespace Namespace;
public class Day13 : BaseDay
{
    Dictionary<int, int> fw = new();

    public Day13()
    {
        string input = File.ReadAllText(InputFilePath);
        foreach (string line in input.Split("\n"))
        {
            var xs = line.Split(": ").Select(int.Parse).ToArray();
            fw[xs[0]] = xs[1];
        }
    }

    public override ValueTask<string> Solve_1()
    {
        int severity = 0;
        for (int pico = 0; pico < 100; ++pico)
        {
            if (fw.ContainsKey(pico) && pico % ((fw[pico] - 1) * 2) == 0)
            {
                // Console.WriteLine($"{pico} {fw[pico]} ");
                severity += pico * fw[pico];
            }
        }
        return new(severity.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
