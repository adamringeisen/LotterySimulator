
global using Spectre.Console;
using LotterySimulator;
var game = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Which lottery would you like to play?")
        .PageSize(5)
        .AddChoices(new[] { "Power Ball", "Mega Millions" })
        );

var quickpicks = AnsiConsole.Ask<int>("How many tickets do you want?");
var games = AnsiConsole.Ask<int>("How many games do you want to play?");

switch (game) {
    case "Power Ball":
        Lotto playgame = new PowerBall(quickpicks, games);
        playgame.Play();
        break;
    case "Mega Millions":
        Lotto playothergame = new MegaMillions(quickpicks, games);
        playothergame.Play();
        break;
    default: throw new Exception("Nope");
            }