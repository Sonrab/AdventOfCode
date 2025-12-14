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
    var svrToFft = FindPaths("svr", "fft");
    var fftToDac = FindPaths("fft", "dac");
    var dacToOut = FindPaths("dac", "out");

    return (youToOut, svrToFft * fftToDac * dacToOut);

    long FindPaths(string start, string target)
    {
        var queue = new Queue<Node>([nodes[target]]);
        var nodeValues = nodes.ToDictionary(n => n.Key, n => new NodeValue([]));
        nodeValues[target].SourceNodes["Initalizer"] = 1;

        while(queue.Count > 0)
        {
            var node = queue.Dequeue();

            foreach(var nb in invertedNodes[node.Id])
            {
                nodeValues[nb.Id].SourceNodes[node.Id] = nodeValues[node.Id].Value;

                if(!queue.Contains(nb))
                {
                    queue.Enqueue(nb);
                }
            }
        }

        return nodeValues[start].Value;
    }
}

var (part1, part2) = Part1AndPart2();
Console.WriteLine(part1);
Console.WriteLine(part2);
;

record Node(string Id, string[] Neighbours);

record NodeValue(Dictionary<string, long> SourceNodes)
{
    public long Value => SourceNodes.Sum(sn => sn.Value);
};