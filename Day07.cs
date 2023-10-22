using System.Text;
using AoCHelper;

namespace Namespace;
public class Day07 : BaseDay
{
    readonly string _input;
    static readonly Dictionary<string, int> weights = new();
    static readonly Dictionary<string, List<string>> above = new();
    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);
        foreach (var row in _input.Split('\n'))
        {
            string[] first;
            string[] second;
            if (row.Contains("->"))
            {
                var arrow = row.Split("->");
                first = arrow[0].Split(' ');
                second = arrow[1].Split(", ");
                for (int i = 0; i < second.Length; ++i)
                {
                    second[i] = second[i].Trim();
                }
                above.Add(first[0], second.ToList());
            }
            else
            {
                first = row.Split(' ');
            }
            int w = int.Parse(first[1].Replace('(', ' ').Replace(')', ' '));
            weights.Add(first[0], w);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        List<string> candidates = new();
        foreach (var k in weights.Keys)
        {
            candidates.Add(k);
        }

        foreach (var v in above.Values)
        {
            foreach (var p in v)
            {
                candidates.Remove(p.Trim());
            }
        }
        return new(candidates[0]);
    }

    public override ValueTask<string> Solve_2()
    {
        string root = "xegshds";
        // string root = "tknk"; // test data
        TreeNode progs = new(root, weights[root]);
        progs.Depth = 0;

        FillTree(progs);
        progs.CalcSubTreeWeights();
        // Console.WriteLine(progs); // print the tree

        return new(FindImbalance(progs).ToString());
    }

    private int FindImbalance(TreeNode n)
    {
        Dictionary<int, int> freqSubTreeWeights = new();
        foreach (var c in n.Children)
        {
            if (freqSubTreeWeights.ContainsKey(c.SubTreeWeight))
            {
                freqSubTreeWeights[c.SubTreeWeight]++;
            }
            else
            {
                freqSubTreeWeights[c.SubTreeWeight] = 1;
            }
        }

        TreeNode cMaybeWrongW = null;
        int correctSTWeight = 0;
        foreach (var c in n.Children)
        {
            if (freqSubTreeWeights[c.SubTreeWeight] == 1)
            {
                cMaybeWrongW = c;
            }
            else
            {
                correctSTWeight = c.SubTreeWeight;
            }
        }

        if (cMaybeWrongW is not null)
        {
            // if diff != 0 we have already found the node with wrong weight deeper in the tree.
            int diff = FindImbalance(cMaybeWrongW);
            return (diff == 0)
                ? cMaybeWrongW.Weight + correctSTWeight - cMaybeWrongW.SubTreeWeight
                : diff;
        }
        else
        {
            return 0;
        }
    }
    private void FillTree(TreeNode n)
    {
        if (n.Parent is not null)
        {
            n.Depth = n.Parent.Depth + 1;
        }
        if (above.ContainsKey(n.Name))
        {
            foreach (var c in above[n.Name])
            {
                n.Children.Add(new(c, weights[c], n));
            }
            foreach (var c in n.Children)
            {
                FillTree(c);
            }
        }
    }
}

class TreeNode
{
    public TreeNode(string name, int weight)
    {
        Name = name;
        Weight = weight;
    }
    public TreeNode(string name, int weight, TreeNode parent)
    {
        Name = name;
        Weight = weight;
        Parent = parent;
    }

    public string Name { get; set; }
    public int Weight { get; set; }
    public List<TreeNode> Children { get; set; } = new();
    public int SubTreeWeight { get; set; }
    public int Depth { get; set; }
    public TreeNode Parent { get; set; } = null;

    public int CalcSubTreeWeights()
    {
        if (SubTreeWeight == 0)
        {
            int w = 0;
            foreach (var c in Children)
            {
                w += c.CalcSubTreeWeights();
            }
            SubTreeWeight = Weight + w;
        }
        return SubTreeWeight;
    }

    public override string ToString()
    {
        StringBuilder s = new();
        if (Children.Count > 0)
        {
            s.Append($"{Name} ({Weight}) ({SubTreeWeight})");
            foreach (var c in Children)
            {
                s.Append($"\n{new string(' ', Depth * 2 + 1)}{c}");
            }
        }
        else
        {
            s.Append($"{Name} ({Weight})");
        }
        return s.ToString();
    }
}