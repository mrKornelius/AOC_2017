using AoCHelper;

namespace Namespace;
public class Day06 : BaseDay
{
    readonly string[] _input;
    static int part2;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath).Split('\t');
    }

    public int[] ParseToIntArray()
    {
        int[] a = new int[_input.Length];
        int i = 0;
        foreach (var s in _input)
        {
            a[i++] = int.Parse(s);
        }
        return a;
    }

    public override ValueTask<string> Solve_1()
    {
        int[] banks = ParseToIntArray();
        int steps = 0;
        int ans = 0;
        HashSet<string> hs = new();

        string loopStart = "";
        while (true)
        {
            if (hs.Contains(IntArrayToString(banks)) && string.IsNullOrEmpty(loopStart))
            {
                ans = steps;
                steps = 0;
                loopStart = IntArrayToString(banks);
            }

            if (!string.IsNullOrEmpty(loopStart) && steps > 0)
            {
                if (loopStart == IntArrayToString(banks))
                {
                    part2 = steps;
                    break;
                }
            }
            // save the state, if this state have been seen before, stop
            steps++;
            hs.Add(IntArrayToString(banks));

            // find bank with maximum value (find its index)
            int mx = banks.Max();
            int i = Array.IndexOf(banks, mx);

            int a = mx / banks.Length;
            int r = mx % banks.Length;

            // distribute value evenly to all 16 banks,
            int runs = 16;
            banks[i] = 0;
            while (runs > 0)
            {
                i++;
                banks[i % banks.Length] += (r > 0) ? a + 1 : a;
                r--;
                runs--;
            }
        }
        return new(ans.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(part2.ToString());
    }


    string IntArrayToString(int[] arr)
    {
        return string.Join(", ", arr);
    }
}
