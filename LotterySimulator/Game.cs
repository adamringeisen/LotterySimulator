namespace LotterySimulator
{
    internal class Game
    {
        
        private static long winnings;
        private static long losings;
        private static int gameCount;
        private static List<string> playedGames = new();
        private static Result result = new Result();
       
        public static List<Roll> GetTickets(string num)
        {
            // TODO change this over to Spectre prompt, which can take an <int>
            List<Roll> tickets = new List<Roll>();
            if (Int32.TryParse(num, out int numTickets))
            {
                for (int i = 0; i < numTickets; i++)
                {
                    tickets.Add(new Roll());
                }
            }
            else
            {
                Console.WriteLine("Sorry, that wasn't a number? What are you doing?");
            }

            return tickets;
        }
        public static void RunSim()
        {
            
            winnings = 0;
            losings = 0;
            gameCount = 0;
            result = new Result();
            string ticketsBought = AnsiConsole.Ask<string>("How many tickets do you want?");
           
            List<Roll> tickets = GetTickets(ticketsBought);

//            PrintUserTickets(tickets);

            string ticketsPlayed = AnsiConsole.Ask<string>("How many games do you want to play?");
        
            List<Roll> drawnTickets = GetTickets(ticketsPlayed);
            foreach (Roll roll in drawnTickets)
            {
                gameCount++;
                playedGames.Add($"Game {gameCount} Won: {winnings:C2} Spent: {losings:C2}");
                

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
            if (!AnsiConsole.Confirm("Play again?"))
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
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
