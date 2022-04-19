
namespace LotterySimulator
{
    internal class Roll
    {
        public List<int> BaseRoll { get; set; }
        public int PowerBall { get; set; }

        public Roll(List<int> baseroll, int Powerball)
        {
            this.BaseRoll = baseroll;
            this.PowerBall = Powerball;
        }
    }
}
