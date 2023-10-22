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
            string id_1 = exp[0];
            string id_2 = exp[4];
            if (!regs.ContainsKey(id_1)) regs[id_1] = 0;
            if (!regs.ContainsKey(id_2)) regs[id_2] = 0;
            int val1 = int.Parse(exp[2]);
            int val2 = int.Parse(exp[^1]);
            string comp = exp[^2];
            string op = exp[1];
            bool doCalc = false;

            switch (comp)
            {
                case "==":
                    doCalc = regs[id_2] == val2;
                    break;
                case "!=":
                    doCalc = regs[id_2] != val2;
                    break;
                case "<":
                    doCalc = regs[id_2] < val2;
                    break;
                case "<=":
                    doCalc = regs[id_2] <= val2;
                    break;
                case ">":
                    doCalc = regs[id_2] > val2;
                    break;
                case ">=":
                    doCalc = regs[id_2] >= val2;
                    break;
            }

            if (doCalc)
            {
                if (op == "dec") regs[id_1] -= val1;
                else regs[id_1] += val1;

                if (regs[id_1] > maxValue) maxValue = regs[id_1];
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
