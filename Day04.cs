using AoCHelper;

namespace Namespace;
public class Day04 : BaseDay
{
    private readonly string _input;
    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int cnt = 0;
        foreach (var line in _input.Split('\n'))
        {
            var pw = line.Split(' ');
            HashSet<string> hs = new();
            foreach (var str in pw)
            {
                hs.Add(str);
            }
            if (pw.Length == hs.Count) cnt++;
        }
        return new(cnt.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int cnt = 0;
        foreach (var line in _input.Split('\n'))
        {
            var pw = line.Split(' ');
            HashSet<string> hs = new();
            foreach (var str in pw)
            {
                var tmp = str.ToCharArray();
                Array.Sort(tmp);
                hs.Add(new string(tmp));
            }
            if (pw.Length == hs.Count) cnt++;
        }
        return new(cnt.ToString());
    }
}
