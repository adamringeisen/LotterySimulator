namespace LotterySimulator
{
    abstract class Lotto     {
        public abstract Result LottoResult { get; }

        public List<Roll> PlayerDraws;
        public List<Roll> GameDraws;
        public int Winings { get; set; }
        public int Losings { get; set; }
        public Lotto(int numberTickets, int numberGames)
        {
            this.PlayerDraws = GetTickets(numberTickets);
            this.GameDraws = GetTickets(numberGames);
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
        internal void AddResult(int numberMatched, bool PowerBall)
        {

            switch (numberMatched)
            {
                case 0:
                    LottoResult.Matches[0].AddHit(PowerBall);
                    Winings += LottoResult.Matches[0].GetValue(PowerBall);
                    break;
                case 1:
                    LottoResult.Matches[1].AddHit(PowerBall);
                    Winings += LottoResult.Matches[1].GetValue(PowerBall);
                    break;
                case 2:
                    LottoResult.Matches[2].AddHit(PowerBall);
                    Winings += LottoResult.Matches[2].GetValue(PowerBall);
                    break;
                case 3:
                    LottoResult.Matches[3].AddHit(PowerBall);
                    Winings += LottoResult.Matches[3].GetValue(PowerBall);
                    break;
                case 4:
                    LottoResult.Matches[4].AddHit(PowerBall);
                    Winings += LottoResult.Matches[4].GetValue(PowerBall);
                    break;
                case 5:
                    LottoResult.Matches[5].AddHit(PowerBall);
                    Winings += LottoResult.Matches[5].GetValue(PowerBall);
                    break;
            }

        }
        
        public List<Roll> GetTickets(int num)
        {
            
            List<Roll> rolls = new List<Roll>();
            for (int i = 0; i < num; i++)
            {
                rolls.Add(GetSingleDraw());
            }
            return rolls;
        }

        public abstract Roll GetSingleDraw();
        public abstract void PrintResult();

        public void CheckTickets()
        {
            foreach (Roll roll in GameDraws)
            {
                foreach (Roll draw in PlayerDraws)
                {
                    CheckNums(draw, roll);
                    //Console.SetCursorPosition(0, 2);
                    //PrintResult();
                    Console.WriteLine($" $0 Hits: {LottoResult.Matches[0].Hits}");
                    Losings += 2;
                }
            }
        }

        public void CheckNums(Roll playerPick, Roll lottoDraw)
        {

            bool powerBallMatch = false;
            if (playerPick.PowerBall == lottoDraw.PowerBall)
            {
                powerBallMatch = true;
            }

            int winningNums = 5 - playerPick.BaseRoll.Except(lottoDraw.BaseRoll).Count();
            AddResult(winningNums, powerBallMatch);
        }
        public int GetWinnings()
        {
            return Winings;
        }
        public abstract void Play();
       
    }

}
