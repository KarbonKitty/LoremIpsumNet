using System.Text;

namespace LoremIpsumNet;

class Program
{
    private static readonly Dictionary<char, double> letterFrequency = new()
    {
        { 'a', 0.082 },
        { 'b', 0.015 },
        { 'c', 0.028 },
        { 'd', 0.043 },
        { 'e', 0.127 },
        { 'f', 0.022 },
        { 'g', 0.020 },
        { 'h', 0.061 },
        { 'i', 0.070 },
        { 'j', 0.0015 },
        { 'k', 0.0077 },
        { 'l', 0.040 },
        { 'm', 0.024 },
        { 'n', 0.067 },
        { 'o', 0.075 },
        { 'p', 0.019 },
        { 'q', 0.001 },
        { 'r', 0.060 },
        { 's', 0.063 },
        { 't', 0.091 },
        { 'u', 0.028 },
        { 'v', 0.001 },
        { 'w', 0.024 },
        { 'x', 0.0015 },
        { 'y', 0.020 },
        { 'z', 0.001 }
    };

    private static readonly double totalFrequency = letterFrequency.Values.Sum();

    static void Main(string[] args)
    {
        var paragraphBuilder = new StringBuilder(2000);

        while (paragraphBuilder.Length < (0.95 * paragraphBuilder.Capacity))
        {
            paragraphBuilder.Append(GenerateWord());
            paragraphBuilder.Append(' ');
        }

        Console.WriteLine(totalFrequency);

        Console.WriteLine(paragraphBuilder.ToString());
    }

    private static Span<char> GenerateWord()
    {
        var length = Random.Shared.Next(2, 15);
        Span<char> word = new char[length + 1];
        for (var i = 0; i <= length; i++)
        {
            word[i] = GenerateCharacter();
        }
        return word;
    }

    private static char GenerateCharacter()
    {
        var r = Random.Shared.NextDouble();
        while (r > totalFrequency)
        {
            r = Random.Shared.NextDouble();
        }
        var runningTotal = 0.0;
        foreach (var (c, w) in letterFrequency)
        {
            runningTotal += w;
            if (runningTotal >= r)
            {
                return c;
            }
        }
        throw new InvalidOperationException("We should have returned something!");
    }
}
