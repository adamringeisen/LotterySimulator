class Roll
{
    public List<int> baseRoll;
    public int powerBall;  
  
    public Roll()
    {
        
        // Lottery Numbers should be sorted in ascending order
        baseRoll = GetNums(5, 1, 70).OrderBy(x => x).ToList();
        powerBall = Global.rand.Next(1, 26);

    }
    public static List<int> GetNums(int numberOfNums, int start, int end)
    {
        
        List<int> list = new List<int>();
        for (int i = 0; i < numberOfNums; i++)
        {
            list.Add(Global.rand.Next(start, end));
        }
        return list;
    }
    public void PrintNums()
    {
        foreach (int i in baseRoll)
          
            {
                if (i < 10)
                {
                    Console.Write($"  {i}");
                }
                else
                {
                    Console.Write($" {i}");
                }
            }

        Console.ForegroundColor = ConsoleColor.Yellow;
        if (powerBall < 10)
        {
            Console.Write($"  {powerBall}");
        }
        else
        {
            Console.Write($" {powerBall}");
        }
        Console.ResetColor();
        Console.WriteLine();
    }

}