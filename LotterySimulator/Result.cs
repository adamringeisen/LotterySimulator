namespace LotterySimulator
{
    public class Result
    {
        // must check for:
        // powerball only : $2
        // one number + powerball : $4
        // 2 + pb : $10
        // 3 numbers : $10
        // 3 + pb : $200
        // 4 number : $500
        // 4 + powerball : $10,000
        // 5 nums : $1,000,000
        // 5 + powerball : jackpot (must calculate jackpot)
        public int Winings { get; set; }
        private Match Match0 = new Match(0, 0, 2);
        private Match Match1 = new Match(1, 0, 4);
        private Match Match2 = new Match(2, 0, 10);
        private Match Match3 = new Match(3, 10, 200);
        private Match Match4 = new Match(4, 500, 10_000);
        private Match Match5 = new Match(5, 1_000_000, 150_000_000);

        public int NumZeroDollarHits()
        {
            return Match0.Hits + Match1.Hits + Match2.Hits;
        }
        public void PrintResult()
        {
            Console.WriteLine($"{"$0",-10}:{NumZeroDollarHits(),10}");
            Console.WriteLine($"{"$2",-10}:{Match0.Phits,10}");
            Console.WriteLine($"{"$4",-10}:{Match1.Phits,10}");
            Console.WriteLine($"{"$10",-10}:{Match2.Phits + Match3.Hits,10}");
            Console.WriteLine($"{"$200",-10}:{Match3.Phits,10}");
            Console.WriteLine($"{"$500",-10}:{Match4.Hits,10}");
            Console.WriteLine($"{"$10,000",-10}:{Match4.Phits,10}");
            Console.WriteLine($"{"1,000,000",-10 }:{Match5.Hits,10}");
            Console.WriteLine($"{"Jackpot!",-10}:{Match5.Phits,10}");
        }

        public int GetWinnings()
        {
            return Winings;
        }
        public void AddResult(int numberMatched, bool PowerBall)
        {

            switch (numberMatched)
            { 
                case 0:
                    Match0.AddHit(PowerBall);
                    Winings += Match0.GetValue(PowerBall);
                    break;
                case 1:
                    Match1.AddHit(PowerBall);
                    Winings += Match1.GetValue(PowerBall);
                    break;
                case 2:
                    Match2.AddHit(PowerBall);
                    Winings += Match2.GetValue(PowerBall);
                    break;
                case 3:
                    Match3.AddHit(PowerBall);
                    Winings += Match3.GetValue(PowerBall);
                    break;
                case 4:
                    Match4.AddHit(PowerBall);
                    Winings += Match4.GetValue(PowerBall);
                    break;
                case 5:
                    Match5.AddHit(PowerBall);
                    Winings += Match5.GetValue(PowerBall);
                    break;
            }

        }

    }
}


 
