using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotterySimulator
{
    internal class ResultFactory
    {
        private static Result MegaMillionsResult = new Result("MegaMillions");
        private static Result PowerBallResult = new Result("PowerBall");

        internal static Result MegaMillions()
        {
            return MegaMillionsResult;
        }
        internal static Result PowerBall()
        {
            return PowerBallResult;
        }
    }
}
