using AoCHelper;

namespace Namespace;
public class Day05 : BaseDay
{
    readonly int[] _input;
    public Day05()
    {
        string[] input = File.ReadAllText(InputFilePath).Split('\n');
        _input = new int[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            _input[i] = int.Parse(input[i]);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        int[] nums = new int[_input.Length];
        Array.Copy(_input, nums, _input.Length);

        int steps = 0;
        int i = 0;
        while (0 <= i && i < nums.Length)
        {
            i += nums[i]++;
            steps++;
        }
        return new(steps.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int[] nums = new int[_input.Length];
        Array.Copy(_input, nums, _input.Length);

        long steps = 0;
        int i = 0;
        while (0 <= i && i < nums.Length)
        {
            int tmp = i;
            i += nums[i];
            nums[tmp] += (nums[tmp] >= 3) ? -1 : 1;
            steps++;
        }
        return new(steps.ToString());
    }
}
