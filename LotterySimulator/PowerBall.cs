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
                4 => LottoResult.Matches[0].Phits + LottoResult.Matches[1].Phits,
                7 => LottoResult.Matches[2].Phits + LottoResult.Matches[3].Hits,
                100 => LottoResult.Matches[3].Phits + LottoResult.Matches[4].Hits,
                50_000 => LottoResult.Matches[4].Phits,
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
            
        }
      


    }



}
