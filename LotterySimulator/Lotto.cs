namespace LotterySimulator
{
    abstract class Lotto     {
        public abstract Result LottoResult { get; }

        public List<Roll> PlayerDraws;
        public List<Roll> GameDraws;
        public int CurrentGames = 0;
        public int GetTotalGames()
        {
            return PlayerDraws.Count*GameDraws.Count;
        }
        public int Winings { get; set; }
        public int Losings { get; set; }
        public Lotto(int numberTickets, int numberGames)
        {
            this.PlayerDraws = GetTickets(numberTickets);
            this.GameDraws = GetTickets(numberGames);
        }
        public Lotto(int[] tickets)
        {
            this.PlayerDraws = GetTickets(tickets[0]);
            this.GameDraws= GetTickets(tickets[1]);
        }
        /// <summary>
        /// Gets a list of random numbers for the lottery draw
        /// and ticket generation.
        /// </summary>
        /// <param name="numberOfNums">How many numbers needed in the list</param>
        /// <param name="start">Start range of numbers in list (inclusive)</param>
        /// <param name="end">End range of numbers in list (exclusive)</param>
        /// <returns>A list of numbers of <c>numberOfNums</c> length where the 
        /// numbers start at <c>start</c> and end at <c>end -1</c></returns>
    public static List<byte> GetNums(int numberOfNums, int start, int end)
        {

            List<byte> list = new List<byte>();
            for (int i = 0; i < numberOfNums; i++)
            {
                list.Add((byte)Global.rand.Next(start, end));
            }
            return list;
        }
        /// <summary>
        /// Takes the number of times a player ticket has matched with
        /// a lottery draw, adds the "hit" to results object and adds
        /// the value of that hit to the winnings property
        /// </summary>
        /// <param name="numberMatched">Number of matches a player ticket has with a lottery draw</param>
        /// <param name="PowerBall">Whether the powerball matched or not</param>
        internal void AddResult(int numberMatched, bool PowerBall)
        {
                    LottoResult.Matches[numberMatched].AddHit(PowerBall);
                    Winings += LottoResult.Matches[numberMatched].GetValue(PowerBall);
        }
        /// <summary>
        /// Gets either a list of player draws or the draws for a particular lottery
        /// </summary>
        /// <param name="num">Number of draws</param>
        /// <returns>A list of lottery draws or tickets</returns>
        public List<Roll> GetTickets(int num)
        {
            
            List<Roll> rolls = new List<Roll>();
            for (int i = 0; i < num; i++)
            {
                rolls.Add(GetSingleDraw());
            }
            return rolls;
        }
        /// <summary>
        /// Gets a single draw for a particular lottery. 
        /// Each lottery has it's own rules for valid draw.
        /// </summary>
        /// <returns>A single draw/ticket for particular lottery</returns>
        public abstract Roll GetSingleDraw();
        /// <summary>
        /// Print the culmulative result of a series of lottery draws.
        /// </summary>
        public abstract void PrintResult();
        /// <summary>
        /// Loop for checking player tickets against lottery draws.
        /// This is O(n^2) and therefore is a bottleneck.
        /// </summary>
        public void CheckTickets()
        {

            foreach (Roll roll in GameDraws)
            {
                foreach (Roll draw in PlayerDraws)
                {
                    CurrentGames++;
                    CheckNums(draw, roll);
                    Losings += 2;
                    Console.SetCursorPosition(0, 0);
                    PrintResult();
                }
            }
        }
        /// <summary>
        /// Check a single lottery draw against a single player picked ticket.
        /// </summary>
        /// <param name="playerPick"></param>
        /// <param name="lottoDraw"></param>
        public void CheckNums(Roll playerPick, Roll lottoDraw)
        {

            bool powerBallMatch = false;
            if (playerPick.PowerBall == lottoDraw.PowerBall)
            {
                powerBallMatch = true;
            }
            
            int winningNums = lottoDraw.BaseRoll.Count - playerPick.BaseRoll.Except(lottoDraw.BaseRoll).Count();
            AddResult(winningNums, powerBallMatch);
        }
        public int GetWinnings()
        {
            return Winings;
        }
        public void Play()
        
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
