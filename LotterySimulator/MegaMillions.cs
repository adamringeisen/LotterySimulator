namespace LotterySimulator

{

    class MegaMillions : Lotto
    {



        public override Result LottoResult { get; } = new Result("MegaMillions");

        public MegaMillions(int numberTickets, int numberGames) : base(numberTickets, numberGames)
        {
        }
        public MegaMillions(int[] tickets) : base(tickets) 
        {
            
        }

        public override Roll GetSingleDraw()
        {
            
            List<byte> baseRoll = GetNums(5, 1, 71).OrderBy(x => x).ToList();
            byte powerBall = (byte)Global.rand.Next(1, 26);
            return new Roll(baseRoll, powerBall);
        }

        public int NumZeroDollarHits()
        {
            return LottoResult.Matches[0].Hits + LottoResult.Matches[1].Hits + LottoResult.Matches[2].Hits;
        }
        /// <summary>
        ///  Takes the prize value and returns
        ///  the number of times that prize has
        ///  been won.
        /// </summary>
        /// <param name="prizes"></param>
        /// <returns>Number of times a prize has been won</returns>
        /// <exception cref="Exception"></exception>
        public int NumHits(int prizes)
        {
            return prizes switch
            {
                0 => LottoResult.Matches[0].Hits + LottoResult.Matches[1].Hits + LottoResult.Matches[2].Hits,
                2 => LottoResult.Matches[0].Phits,
                4 => LottoResult.Matches[1].Phits,
                10 => LottoResult.Matches[2].Phits + LottoResult.Matches[3].Hits,
                200 => LottoResult.Matches[3].Phits,
                500 => LottoResult.Matches[4].Hits,
                10_000 => LottoResult.Matches[4].Phits,
                1_000_000 => LottoResult.Matches[5].Hits,
                -1 => LottoResult.Matches[5].Phits,
                _ => throw new Exception("Invalid option passed to NumHits method"),
            };
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
            HitsTable.AddRow($"$2", $"{NumHits(2)}", $"{NumHits(2) * 2:C2}");
            HitsTable.AddRow("$10", $"{NumHits(10)}", $"{NumHits(10) * 10:C2}");
            HitsTable.AddRow("$200", $"{NumHits(200)}", $"{NumHits(200) * 200:C2}");
            HitsTable.AddRow("$500", $"{NumHits(500)}", $"{NumHits(500) * 500:C2}");
            HitsTable.AddRow("$10,000", $"{NumHits(10_000)}", $"{NumHits(10_000) * 10_000:C2}");
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

        }
    }

}
