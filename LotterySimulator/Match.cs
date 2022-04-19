namespace LotterySimulator
{
    public class Match
    {
        int NumMatched;
        int BaseValue;
        int PowerBallValue;
        public int Hits { get; set; } = 0;
        public int Phits { get; set; } = 0;
        public Match(int NumMatched, int BaseValue, int PowerBallValue)
        {
            this.NumMatched = NumMatched;
            this.BaseValue = BaseValue;
            this.PowerBallValue = PowerBallValue;
        }

        public int GetValue(bool powerball = false)
        {
            if (powerball)
            {
                return PowerBallValue;
            }
            else
            {
                return BaseValue;
            }
        }
        public void AddHit(bool powerball = false)
        {
            if (!powerball)
            {
                Hits++;
                Console.WriteLine("Hit added no powerball");
                Console.WriteLine($" Current Hits = {Hits}");
            }
            else
            {
                Phits++;
                Console.WriteLine("Hit added powerball");
            }
        }
        public override string ToString()
        {
            return $"Number Matched {NumMatched} Base: {BaseValue} PowerBall: {PowerBallValue}";
        }
    }
}
