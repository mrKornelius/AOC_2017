using AoCHelper;

namespace Namespace;
public class Day12 : BaseDay
{
    private string input;
    private int part2 = 0;
    private Dictionary<int, List<int>> dict = new();
    public Day12()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        foreach (string line in input.Split('\n'))
        {
            string[] parts = line.Split("<->");
            dict[int.Parse(parts[0])] = parts[1].Split(", ").Select(int.Parse).ToList();
        }

        List<int> groups = new();
        HashSet<int> used = new();
        HashSet<int> connected = new();
        Queue<int> que = new();
        foreach (var p in dict.Keys)
        {
            if (used.Contains(p)) continue;
            que.Enqueue(p);
            while (que.Count > 0)
            {
                int current = que.Dequeue();
                used.Add(current);
                connected.Add(current);
                foreach (var item in dict[current])
                {
                    if (!connected.Contains(item))
                        que.Enqueue(item);
                }
            }
            groups.Add(connected.Count);
            connected.Clear();
        }
        part2 = groups.Count;
        return new(groups[0].ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(part2.ToString());
    }
}
