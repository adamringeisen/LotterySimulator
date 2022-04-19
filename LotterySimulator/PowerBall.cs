namespace LotterySimulator
{

    class PowerBall : Lotto
    {
        public override Result LottoResult => ResultFactory.PowerBall();


        public PowerBall(int numberTickets, int numberGames) : base(numberTickets, numberGames)
        {
        }

        public override Roll GetSingleDraw()
        {

            List<byte> baseRoll = GetNums(5, 1, 70).OrderBy(x => x).ToList();
            byte powerBall = (byte)Global.rand.Next(1, 27);
            return new Roll(baseRoll, powerBall);
        }

        public int NumZeroDollarHits()
        {
            return LottoResult.Matches[0].Hits + LottoResult.Matches[1].Hits + LottoResult.Matches[2].Hits;
        }

        public override void PrintResult()
        {
            Console.WriteLine($"{"$0",-10}:{NumZeroDollarHits(),10}");
            Console.WriteLine($"{"$4",-10}:{LottoResult.Matches[0].Phits + LottoResult.Matches[1].Phits,10}");
            Console.WriteLine($"{"$7",-10}:{LottoResult.Matches[2].Phits + LottoResult.Matches[3].Hits,10}");
            Console.WriteLine($"{"$100",-10}:{LottoResult.Matches[3].Phits + LottoResult.Matches[4].Hits,10}");
            Console.WriteLine($"{"$50_000",-10}:{LottoResult.Matches[4].Phits,10}");
            Console.WriteLine($"{"1,000,000",-10 }:{LottoResult.Matches[5].Hits,10}");
            Console.WriteLine($"{"Jackpot!",-10}:{LottoResult.Matches[5].Phits,10}");
        }
        public override void Play()
        {
            Console.CursorVisible = false;
            CheckTickets();
            Console.SetCursorPosition(0, 12);
            AnsiConsole.WriteLine($"You have won {GetWinnings():C2}");
            AnsiConsole.WriteLine($"But you spent {Losings:C2}");
            AnsiConsole.WriteLine($"For a net of {GetWinnings() - Losings:C2}");
            Console.CursorVisible = true;


        }


    }



}
