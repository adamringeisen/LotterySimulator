using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotterySimulator
{
    internal class Game
    {
        private static long winnings;
        private static long losings;
        private static int gameCount;
        private static List<string> playedGames = new();
        public static void PrintUserTickets(List<Roll> tickets)
        {
            AnsiConsole.WriteLine("Your Tickets");
            for (int i = 0; i < tickets.Count; i++)
            {
                AnsiConsole.Write(new Markup(tickets[i].PrintNums()));
                AnsiConsole.WriteLine();
                //tickets[i].PrintNums();
            }
        }
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

            string ticketsBought = AnsiConsole.Ask<string>("How many tickets do you want?");
           
            List<Roll> tickets = GetTickets(ticketsBought);

            PrintUserTickets(tickets);

            string ticketsPlayed = AnsiConsole.Ask<string>("How many games do you want to play?");
        
            List<Roll> drawnTickets = GetTickets(ticketsPlayed);
            foreach (Roll roll in drawnTickets)
            {
                gameCount++;
                playedGames.Add($"Game {gameCount} Won: {winnings:C2} Spent: {losings:C2}");
                

                foreach(Roll draw in tickets)
                {
                    CheckNums(draw,roll);
                    losings += 2;
                }
            }
     
            AnsiConsole.WriteLine($"You have won {winnings:C2}");
            AnsiConsole.WriteLine($"But you spent {losings:C2}");
            AnsiConsole.WriteLine($"For a net of {winnings -  losings:C2}");
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
            ReportWinnings(winningNums, powerBallMatch);                     
           


        }
        public static void ReportWinnings(int numberMatched, bool powerball)

        {
            
            if (powerball == true)
            {
                switch (numberMatched)
                { // must check for:
                  // powerball only : $2
                  // one number + powerball : $4
                  // 2 + pb : $10
                  // 3 numbers : $10
                  // 3 + pb : $200
                  // 4 number : $500
                  // 4 + powerball : $10,000
                  // 5 nums : $1,000,000
                  // 5 + powerball : jackpot (must calculate jackpot)
                    case 0:
                       AnsiConsole.WriteLine("Power Ball Matched! $2");
                        winnings += 2;
                        break;
                    case 1:
                       Console.WriteLine("One Number and Powerball Matched $4");
                        winnings += 4;
                        break;
                    case 2:
                      Console.WriteLine("Two Numbers and Powerball Matched $10");
                        winnings += 10;
                        break;
                    case 3:
                      Console.WriteLine("Three numbers and Powerball Numbers Matched $200");
                        winnings += 200;
                        break;
                    case 4:
                       Console.WriteLine("Four Numbers and Powerball Matched! $10,000");
                        winnings += 10_000;
                        break;
                    case 5:
                       Console.WriteLine("Five numbers and Powerball Numbers Matched $Jackpot!");
                        winnings += 150_000_000;
                        break;



                }
            }
            else {
                switch (numberMatched)
                {
                    case 0:
                       Console.WriteLine("No Numbers Match $0");
                        break;
                    case 1:
                       Console.WriteLine("One Number Matched $0");
                        break;
                    case 2:
                       Console.WriteLine("Two Numbers Matched $0");
                        break;
                    case 3:
                      Console.WriteLine("Three Numbers Matched $10");
                        winnings += 10;
                        break;
                    case 4:
                      Console.WriteLine("Four Numbers Matched $500");
                        winnings += 500;
                        break;
                    case 5:
                        Console.WriteLine("Five Numbers Matched $1,000,000");
                        winnings += 1_000_000;
                        break;


                }
            }
        }


        
    }
}
