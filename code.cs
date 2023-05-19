using System.Collections.Generic;
using System.Text;

public class Program
{
    private const string CURRENCY = "EUR";
    private static int[] Denominations => new[] { 10, 50, 100 };
    private static int[] SortedDenominations = Denominations.OrderBy(x => x).ToArray();
    static void Main(string[] args)
    {
        int n = 0;
        while (int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine($"There are {GetCombinationCount(n, SortedDenominations.Length - 1)} combinations for {n} {CURRENCY}");
        }
    }

    static int GetCombinationCount(int amount, int index, int[]? banknoteCounters = null)
    {
        if (banknoteCounters == null) banknoteCounters = new int[SortedDenominations.Length];
        if (amount < 0 || index < 0) 
            return 0;
        if (amount == 0 || index == 0)
        {
            if (index == 0)
                banknoteCounters[index] = amount / SortedDenominations[index];
            OutputBanknoteCounters(banknoteCounters);
            return 1;
        }
        var countBranch1 = GetCombinationCount(amount, index - 1, (int[]?)banknoteCounters.Clone());
        banknoteCounters[index]++;
        var countBranch2 = GetCombinationCount(amount - SortedDenominations[index], index, banknoteCounters);
        return countBranch1 + countBranch2;
    }

    static void OutputBanknoteCounters(int[] banknoteCounters)
    {
        var list = new List<string>();
        for (int i = 0; i < SortedDenominations.Length; i++)
        {
            if (banknoteCounters[i] > 0)
                list.Add($"{banknoteCounters[i]}*{SortedDenominations[i]} {CURRENCY}");
        }
        Console.WriteLine(string.Join(" + ", list));
    }
}
