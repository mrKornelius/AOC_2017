using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using AoCHelper;
using Spectre.Console.Rendering;

namespace Namespace;
public class Day10 : BaseDay
{
    private string input;
    public Day10()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        List<int> lens = input.Split(",").Select(int.Parse).ToList();
        int[] xs = Enumerable.Range(0, 256).ToArray();
        int current = 0;
        int skip = 0;
        foreach (int l in lens)
        {
            if (l > 0) ReverseArrayPartially(current, l, xs);
            current += l + skip;
            current %= xs.Length;
            skip++;
        }

        return new((xs[0] * xs[1]).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<int> lens = input.Select(s => (int)s).ToList();
        List<int> lenSuffix = new() { 17, 31, 73, 47, 23 };
        lens.AddRange(lenSuffix);

        int[] xs = Enumerable.Range(0, 256).ToArray();
        int round = 0;
        int current = 0;
        int skip = 0;
        while (round++ < 64)
        {
            foreach (int l in lens)
            {
                if (l > 0) ReverseArrayPartially(current, l, xs);
                current += l + skip;
                current %= xs.Length;
                skip++;
            }
        }

        List<int> denseHash = new();
        foreach (var x in xs.Chunk(16))
        {
            denseHash.Add(CalculateDenseHash(x));
        }

        StringBuilder sb = new();
        foreach (var h in denseHash)
        {
            sb.Append(string.Format("{0:x2}", h));
        }

        return new(sb.ToString());
    }

    private static int CalculateDenseHash(int[] x)
    {
        int result = x[0];
        for (int i = 1; i < x.Length; ++i)
        {
            result ^= x[i];
        }
        return result;
    }

    public static void ReverseArrayPartially(int start, int len, int[] arr)
    {
        if (len <= 1) return;
        int[] res = new int[arr.Length];

        if (start + len <= arr.Length) // no wrap around
        {
            int hi = start + len - 1;
            while (start < hi)
            {
                (arr[start], arr[hi]) = (arr[hi], arr[start]);
                start++; hi--;
            }
        }
        else // the part we want to rev wraps around the array
        {
            // split the array at start index and swap order
            int l1 = arr.Length - start;
            arr[start..].CopyTo(res, 0);
            arr[..start].CopyTo(res, l1);
            // reverse the new array from start to len
            res[..len].Reverse().ToArray().CopyTo(res, 0);
            // split the new array and swap it back
            res[l1..].CopyTo(arr, 0);
            res[..l1].CopyTo(arr, start);

        }
    }
}
