
namespace LotterySimulator
{
    internal class Roll
    {
        public List<byte> BaseRoll { get; set; }
        public byte PowerBall { get; set; }

        public Roll(List<byte> baseroll, byte Powerball)
        {
            this.BaseRoll = baseroll;
            this.PowerBall = Powerball;
        }
    }
}
