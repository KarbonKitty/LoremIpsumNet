using System.Text;

namespace LoremIpsumNet;

public class LoremIpsumGenerator
{
    private static readonly Dictionary<char, double> letterFrequency = new()
    {
        ['a'] = 0.082,
        ['b'] = 0.015,
        ['c'] = 0.028,
        ['d'] = 0.043,
        ['e'] = 0.127,
        ['f'] = 0.022,
        ['g'] = 0.020,
        ['h'] = 0.061,
        ['i'] = 0.070,
        ['j'] = 0.0015,
        ['k'] = 0.0077,
        ['l'] = 0.040,
        ['m'] = 0.024,
        ['n'] = 0.067,
        ['o'] = 0.075,
        ['p'] = 0.019,
        ['q'] = 0.001,
        ['r'] = 0.060,
        ['s'] = 0.063,
        ['t'] = 0.091,
        ['u'] = 0.028,
        ['v'] = 0.001,
        ['w'] = 0.024,
        ['x'] = 0.0015,
        ['y'] = 0.020,
        ['z'] = 0.001,
    };

    private static readonly double totalFrequency = letterFrequency.Values.Sum();

    // from: https://core.ac.uk/download/pdf/82753461.pdf
    private static readonly Dictionary<int, double> wordLengthFrequency = new()
    {
        [1] = 1000,
        [2] = 7000,
        [3] = 8000,
        [4] = 7000,
        [5] = 4000,
        [6] = 3000,
        [7] = 3000,
        [8] = 2000,
        [9] = 1500,
        [10] = 1000,
        [11] = 600,
        [12] = 400,
        [13] = 200,
        [14] = 100,
        [15] = 50,
        [16] = 50
    };

    private static readonly double totalWordFrequency = wordLengthFrequency.Values.Sum();

    public static string Generate()
    {
        var paragraphBuilder = new StringBuilder(2000);

        while (paragraphBuilder.Length < (0.95 * paragraphBuilder.Capacity))
        {
            paragraphBuilder.Append(GenerateWord());
            paragraphBuilder.Append(' ');
        }

        return paragraphBuilder.ToString();
    }


    private static Span<char> GenerateWord()
    {
        var length = 0;
        var r = Random.Shared.NextDouble() * totalWordFrequency;

        var runningTotal = 0.0;
        foreach (var (l, f) in wordLengthFrequency)
        {
            runningTotal += f;
            if (runningTotal >= r)
            {
                length = l;
                break;
            }
        }
        
        Span<char> word = new char[length];
        for (var i = 0; i < length; i++)
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