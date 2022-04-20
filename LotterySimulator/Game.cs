using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotterySimulator
{
    internal class Game
    {
        public static void PrintHeader()
        {
            AnsiConsole.Write(
               new FigletText("Lottery")
               .LeftAligned()
               .Color(Color.Red));
            AnsiConsole.Write(
                new FigletText("Simulator")
                .Centered()
                .Color(Color.Green1));
        }
        public static void LottoSelect()
        {
            Console.Clear();
           PrintHeader();
            var game = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Which lottery would you like to play?")
            .PageSize(5)
            .AddChoices(new[] { "Power Ball", "Mega Millions" })
        );
            switch (game)
            {
                case "Power Ball":
                    Lotto pb = new PowerBall(TicketSelect());
                    pb.Play();
                    break;
                case "Mega Millions":
                    Lotto mm = new MegaMillions(TicketSelect());
                    mm.Play();
                    break;
                default: throw new Exception("Nope");
            }

            if (!AnsiConsole.Confirm("Play again?"))
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                GC.Collect();
                LottoSelect();
            }
        }
        public static int[] TicketSelect()
        {
            GC.Collect();
            var quickpicks = AnsiConsole.Ask<int>("How many tickets do you want?");
            var games = AnsiConsole.Ask<int>("How many games do you want to play?");
            int[] TicketSelection = new int[] {quickpicks,games};
            return TicketSelection;
        }

    }
}
