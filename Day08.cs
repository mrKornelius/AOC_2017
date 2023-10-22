using AoCHelper;

namespace Namespace;
public class Day08 : BaseDay
{
    readonly string _input;
    int part2 = 0;
    public Day08()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int maxValue = 0;
        Dictionary<string, int> regs = new();
        foreach (var line in _input.Split('\n'))
        {
            var exp = line.Split(' ');
            if (!regs.ContainsKey(exp[0])) regs[exp[0]] = 0;
            if (!regs.ContainsKey(exp[4])) regs[exp[4]] = 0;
            int val1 = int.Parse(exp[2]);
            int val2 = int.Parse(exp[^1]);
            bool doCalc = false;

            switch (exp[^2])
            {
                case "==":
                    doCalc = regs[exp[4]] == val2;
                    break;
                case "!=":
                    doCalc = regs[exp[4]] != val2;
                    break;
                case "<":
                    doCalc = regs[exp[4]] < val2;
                    break;
                case "<=":
                    doCalc = regs[exp[4]] <= val2;
                    break;
                case ">":
                    doCalc = regs[exp[4]] > val2;
                    break;
                case ">=":
                    doCalc = regs[exp[4]] >= val2;
                    break;
            }

            if (doCalc)
            {
                if (exp[1] == "dec") regs[exp[0]] -= val1;
                else regs[exp[0]] += val1;

                if (regs[exp[0]] > maxValue) maxValue = regs[exp[0]];
            }
        }
        part2 = maxValue;
        return new(regs.Values.Max().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(part2.ToString());
    }
}
