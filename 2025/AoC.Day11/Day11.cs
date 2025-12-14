using System.Data;
using AoC.Shared;

static (long, long) Part1AndPart2()
{
    var rows = "AoC.Day11/input.txt".ReadAllLines();

    Dictionary<string, Node> nodes = rows.Select(row =>
    {
        var split = row.Split(':');
        return new Node(
            Id: split[0],
            Neighbours: split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }).Append(new("out", [])).ToDictionary(n => n.Id); 

    Dictionary<string, Node[]> invertedNodes = nodes.Keys.Select(nodeId =>
    {
        return new KeyValuePair<string, Node[]>(
            nodeId,
            [.. nodes.Values.Where(n => n.Neighbours.Contains(nodeId)).Select(n => nodes[n.Id])]);
    }).ToDictionary();

    var youToOut = FindPaths("you", "out");

    // Finns inga vägar från dac till fft så det här är enda sekvensen som behöver kollas
    var dacToOut = FindPaths("dac", "out");
    var fftToDac = FindPaths("fft", "dac");
    var svrToFft = FindPaths("svr", "fft");

    return (youToOut, svrToFft * fftToDac * dacToOut);

    long FindPaths(string start, string target)
    {
        var queue = new Queue<Node>([nodes[target]]);
        var nodeValues = new Dictionary<string, List<NodeValue>>()
        {
            { target, [ new("Initializer", 1) ] }
        };

        while(queue.Count > 0)
        {
            var node = queue.Dequeue();

            foreach(var nb in invertedNodes[node.Id])
            {
                if(nodeValues.TryGetValue(nb.Id, out var val))
                {
                    val.RemoveAll(v => v.Id == node.Id);
                    nodeValues[nb.Id].Add(new(node.Id, nodeValues[node.Id].Sum(v => v.Value)));
                }
                else
                {
                    nodeValues[nb.Id] = [new(node.Id, nodeValues[node.Id].Sum(v => v.Value))];
                }

                if(!queue.Contains(nb))
                {
                    queue.Enqueue(nb);
                }
            }
        }

        return nodeValues[start].Sum(v => v.Value);
    }
}

var (part1, part2) = Part1AndPart2();
Console.WriteLine(part1);
Console.WriteLine(part2);
;

record Node(string Id, string[] Neighbours);

record NodeValue(string Id, long Value);