using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace LotterySimulator
{
    internal class Game
    {
        public static void PrintUserTickets(List<Roll> tickets)
        {
            AnsiConsole.WriteLine("Your Tickets");
            for (int i = 0; i < tickets.Count; i++)
            {
                AnsiConsole.Write(new Markup(tickets[i].PrintNums()));
                AnsiConsole.WriteLine();
                // tickets[i].PrintNums();
            }
        }
        public static List<Roll> GetTickets(string num)
        {
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
            string ticketsBought = AnsiConsole.Ask<string>("How many tickets do you want?");
           
            List<Roll> tickets = GetTickets(ticketsBought);
            PrintUserTickets(tickets);

            string ticketsPlayed = AnsiConsole.Ask<string>("How many games do you want to play?");
            for (int i = 0; i < 2; i++)
            {
                List<Roll> drawnTickets = GetTickets(ticketsPlayed);
                foreach (Roll roll in drawnTickets)
                {
                    foreach(Roll draw in tickets)
                    {
                        CheckNums(draw,roll);
                    }
                }
            }
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
            // this is not correctly counting the number of matchs, it's counting unmatched I think
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
                        Console.WriteLine("Power Ball Matched! $2");
                        break;
                    case 1:
                        Console.WriteLine("One Number and Powerball Matched $4");
                        break;
                    case 2:
                        Console.WriteLine("Two Numbers and Powerball Matched $10");
                        break;
                    case 3:
                        Console.WriteLine("Three numbers and Powerball Numbers Matched $200");
                        break;
                    case 4:
                        Console.WriteLine("Four Numbers and Powerball Matched! $10,000");
                        break;
                    case 5:
                        Console.WriteLine("Five numbers and Powerball Numbers Matched $Jackpot!");
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
                        break;
                    case 4:
                        Console.WriteLine("Four Numbers Matched $500");
                        break;
                    case 5:
                        Console.WriteLine("Five Numbers Matched $1,000,000");
                        break;
                }
            }
        }
        public static void PrintGameScreen()
        {

        }
    }
}
