namespace LotterySimulator
{
    internal class Game
    {
        
        private static long losings;
        private static Result result = new Result();
       
        public static List<Roll> GetTickets(int num)
        {
            List<Roll> tickets = new List<Roll>();
            for (int i = 0; i < num; i++)
                {
                    tickets.Add(new Roll());
                }
            

            return tickets;
        }
        public static void RunSim()
        {
            // reset for new game

            losings = 0;

            result = new Result();
            Console.Clear();

            int ticketsBought = AnsiConsole.Ask<int>("How many tickets do you want?");
           
            List<Roll> tickets = GetTickets(ticketsBought);

            int ticketsPlayed = AnsiConsole.Ask<int>("How many games do you want to play?");
        
            List<Roll> drawnTickets = GetTickets(ticketsPlayed);
            Console.CursorVisible = false;
            foreach (Roll roll in drawnTickets)
            {
                foreach(Roll draw in tickets)
                {
                    CheckNums(draw,roll);
                    Console.SetCursorPosition(0, 2);
                    result.PrintResult();
                    losings += 2;
                }
            }
            Console.SetCursorPosition(0, 12);
            AnsiConsole.WriteLine($"You have won {result.GetWinnings():C2}");
            AnsiConsole.WriteLine($"But you spent {losings:C2}");
            AnsiConsole.WriteLine($"For a net of {result.GetWinnings() -  losings:C2}");
            Console.CursorVisible = true;
            if (!AnsiConsole.Confirm("Play again?"))
            {
                Environment.Exit(0);
            }
            else
            {
                RunSim();
            }
        }

        public static void CheckNums(Roll playerPick, Roll lottoDraw)
        {
           
            bool powerBallMatch = false;
            if (playerPick.powerBall == lottoDraw.powerBall)
            {
                powerBallMatch = true;
            }

            int winningNums = 5 - playerPick.baseRoll.Except(lottoDraw.baseRoll).Count();
            result.AddResult(winningNums, powerBallMatch);                     
          }
    }
}
