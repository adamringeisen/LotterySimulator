

using System.Collections;

namespace LotterySimulator
{
    internal class Result : IEnumerable
    {
        public List<Match> Matches = new List<Match>();

        public Result() { }
        public Result(string format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            else if (format == "PowerBall")
            {
                Matches = new List<Match>
                {
                    new Match(0, 0, 4),
                    new Match(1, 0, 4),
                    new Match(2, 0, 7),
                    new Match(3, 7, 100),
                    new Match(4, 100, 50_000),
                    new Match(5, 1_000_000, 150_000_000),
                };
            
            }
            else if (format == "MegaMillions")
            {
               Matches = new List<Match>
                {
                    new Match(0, 0, 2),
                    new Match(1, 0, 4),
                    new Match(2, 0, 10),
                    new Match(3, 10, 200),
                    new Match(4, 500, 10_000),
                    new Match(5, 1_000_000, 150_000_000),
                };
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Matches).GetEnumerator();
        }

      
    }
}
