using System.Text;
using AoCHelper;
using Microsoft.Win32.SafeHandles;

namespace Namespace;
public class Day09 : BaseDay
{
    readonly string _input;
    int part2 = 0;
    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        // Console.WriteLine(_input[0..80]);
        StringBuilder sb = new();
        bool skip = false;
        foreach (char c in _input)
        {
            if (skip)
            {
                skip = false;
                continue;
            }
            if (c == '!')
            {
                skip = true;
                continue;
            }
            sb.Append(c);
        }
        string pass1 = sb.ToString();

        // Console.WriteLine(pass1[0..80]);
        StringBuilder sb1 = new();
        skip = false;
        int garbageCnt = 0;
        foreach (char c in pass1)
        {
            if (skip)
            {
                if (c == '>') skip = false;
                else garbageCnt++;
                continue;
            }
            if (c == '<')
            {
                skip = true;
                continue;
            }
            sb1.Append(c);
        }
        // Console.WriteLine(sb1.ToString()[0..80]);

        part2 = garbageCnt;
        return new(GroupScore(sb1.ToString()).ToString());
    }

    private static int GroupScore(string str)
    {
        int score = 0;
        int cnt = 0;
        foreach (char c in str)
        {
            if (c == '{')
            {
                cnt++;
            }
            else if (c == '}')
            {
                score += cnt;
                cnt--;
            }
        }
        return score;
    }

    public override ValueTask<string> Solve_2()
    {
        return new(part2.ToString());
    }
}
