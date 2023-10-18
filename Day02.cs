using AoCHelper;

namespace AOC_2017;
class Day02 : BaseDay
{
    public readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        long sum = 0;

        foreach (string line in _input.Split('\n'))
        {
            int mn = int.MaxValue, mx = 0;
            foreach (string n in line.Split('\t'))
            {
                int x = int.Parse(n);
                if (x < mn) mn = x;
                if (x > mx) mx = x;
            }
            sum += mx - mn;
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long sum = 0;
        foreach (string line in _input.Split('\n'))
        {
            List<int> l = new();
            foreach (string n in line.Split('\t'))
            {
                l.Add(int.Parse(n));
            }
            l.Sort();
            for (int i = 0; i < l.Count; ++i)
            {
                for (int j = i + 1; j < l.Count; ++j)
                {
                    if (l[j] % l[i] == 0) sum += l[j] / l[i];
                }
            }
        }
        return new(sum.ToString());
    }
}