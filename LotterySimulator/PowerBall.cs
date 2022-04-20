namespace LotterySimulator
{

    class PowerBall : Lotto
    {
        public override Result LottoResult { get; } = new Result("PowerBall");


        public PowerBall(int numberTickets, int numberGames) : base(numberTickets, numberGames)
        {
        }
        public PowerBall(int[] tickets) : base(tickets) { }

        public override Roll GetSingleDraw()
        {

            List<byte> baseRoll = GetNums(5, 1, 70).OrderBy(x => x).ToList();
            byte powerBall = (byte)Global.rand.Next(1, 27);
            return new Roll(baseRoll, powerBall);
        }

        public int NumHits(int matches)
        {
            switch (matches)
            {
                case 0:
                    return LottoResult.Matches[0].Hits + LottoResult.Matches[1].Hits + LottoResult.Matches[2].Hits;
                case 4:
                    return LottoResult.Matches[0].Phits + LottoResult.Matches[1].Phits;
                case 7:
                    return LottoResult.Matches[2].Phits + LottoResult.Matches[3].Hits;
                case 100:
                    return LottoResult.Matches[3].Phits + LottoResult.Matches[4].Hits;
                case 50_000:
                    return LottoResult.Matches[4].Phits;
                case 1_000_000:
                    return LottoResult.Matches[5].Hits;
                case -1:
                    return LottoResult.Matches[5].Phits;
                default: throw new Exception("Invalid option passed to NumHits method");

            }
               
                
        }

        public override void PrintResult()
        {
            Game.PrintHeader();
            var HitsTable = new Table();
            HitsTable.AddColumn("Prize");
            HitsTable.AddColumn("Hits");
            HitsTable.AddColumn("Winnings");
            HitsTable.Columns[0].LeftAligned();
            HitsTable.Columns[1].RightAligned();
            HitsTable.Columns[2].RightAligned();
            HitsTable.AddRow($"$0", $"{NumHits(0)}", "$0.00");
            HitsTable.AddRow($"$4", $"{NumHits(4)}", $"{NumHits(4)*4:C2}");
            HitsTable.AddRow("$7", $"{NumHits(7)}", $"{NumHits(7) * 7:C2}");
            HitsTable.AddRow("$100", $"{NumHits(100)}", $"{NumHits(100) * 100:C2}");
            HitsTable.AddRow("$1,000,000", $"{NumHits(1_000_000)}", $"{NumHits(1_000_000) * 1_000_000:C2}");
            HitsTable.AddRow("Jackpot!", $"{NumHits(-1)}", $"{NumHits(-1) * 150_000_000:C2}");

            var ResultTable = new Table();
            ResultTable.AddColumn("Results");
            ResultTable.AddRow($"Game {CurrentGames} of {GetTotalGames()}");
            ResultTable.AddRow($"Total Winnings: {Winings:C2}");
            ResultTable.AddRow($"Total Tickest Cost: {Losings:C2}");
            ResultTable.AddRow($"Balance: {Winings - Losings:C2}");

            var MainTable = new Table();
            MainTable.AddColumn("");
            MainTable.AddColumn("");
            MainTable.AddRow(HitsTable, ResultTable);
            MainTable.Centered();
            MainTable.HideHeaders();

            if (Winings < Losings)
            {
                MainTable.BorderColor(Color.Red);
            }
            else
            {
                MainTable.BorderColor(Color.Green);
            }
            AnsiConsole.Write(MainTable);
            //Console.WriteLine($"Playing {GameDraws.Count} games of PowerBall with {PlayerDraws.Count} tickets");
            //Console.WriteLine($"{"$0",-10}:{NumHits(0),10}");
            //Console.WriteLine($"{"$4",-10}:{LottoResult.Matches[0].Phits + LottoResult.Matches[1].Phits,10}");
            //Console.WriteLine($"{"$7",-10}:{LottoResult.Matches[2].Phits + LottoResult.Matches[3].Hits,10}");
            //Console.WriteLine($"{"$100",-10}:{LottoResult.Matches[3].Phits + LottoResult.Matches[4].Hits,10}");
            //Console.WriteLine($"{"$50_000",-10}:{LottoResult.Matches[4].Phits,10}");
            //Console.WriteLine($"{"1,000,000",-10 }:{LottoResult.Matches[5].Hits,10}");
            //Console.WriteLine($"{"Jackpot!",-10}:{LottoResult.Matches[5].Phits,10}");
            //Console.WriteLine($"$ Won {Winings:C2}");
            //Console.WriteLine($"$ Spent {Losings:C2}");
        }
        public override void Play()
        {
            Console.CursorVisible = false;
            Console.Clear();

            CheckTickets();
            AnsiConsole.Write(
               new FigletText("You")
               .Centered()
               .Color(Color.Red));
            if (Winings < Losings)
            {
                AnsiConsole.Write(
                    new FigletText("Lost!")
                    .Centered()
                    .Color(Color.Red3));
            }
            else
            {
                AnsiConsole.Write(
                    new FigletText("Won!")
                    .Centered()
                    .Color(Color.Green1));
            }
            Console.CursorVisible = true;


        }


    }



}
