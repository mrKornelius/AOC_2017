using AoCHelper;

namespace AOC_2017;
class Day01 : BaseDay
{
    public readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        long sum = 0;
        for (int i = 1; i < _input.Length; ++i)
        {
            sum += (_input[i - 1] == _input[i]) ? long.Parse(_input[i].ToString()) : 0;
        }
        sum += (_input[0] == _input.Last()) ? long.Parse(_input[0].ToString()) : 0;
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long sum = 0;
        int half = _input.Length / 2;
        for (int i = 0; i < half; ++i)
        {
            sum += (_input[i] == _input[i + half]) ? long.Parse(_input[i].ToString()) : 0;
        }
        sum *= 2;
        return new(sum.ToString());
    }
}