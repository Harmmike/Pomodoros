using System;

namespace ConsoleUI
{
    class Program
    {

        private static PomodoroController _controller;

        static void Main(string[] args)
        {
            _controller = new PomodoroController();

            int quantity = int.Parse(GetInput("How many pomodoros will you complete?", true));
            int workInterval = int.Parse(GetInput("How long will your work sets be?", true));
            int breakInterval = int.Parse(GetInput("How long will your breaks be?"));
            string name = GetInput("What is the project name?");
            string description = GetInput("What is the project description?");

            _controller.WorkTimerDing += OnWorkTimerDing;
            _controller.BreakTimerDing += OnBreakTimerDing;

            _controller.PomodoroFinished += OnPomodoroFinished;
            _controller.StartNewPomodoro(workInterval, breakInterval, quantity, name, description);

            Console.ReadLine();
        }

        private static void OnPomodoroFinished(object sender, EventArgs e)
        {
            Console.Beep();
            Console.WriteLine($"Congrats, you have finishd working on {_controller.CurrentProject.ProjectName}!");
        }

        private static void OnBreakTimerDing(object sender, EventArgs e)
        {
            Console.WriteLine("work");
            Console.Beep();
            Console.WriteLine($"Time to work for {_controller.GetWorkTime()} minutes");
        }

        private static void OnWorkTimerDing(object sender, EventArgs e)
        {
            Console.WriteLine("break");
            Console.Beep();
            Console.WriteLine($"Time for a {_controller.GetBreakTime()} minute break");
        }

        private static string GetInput(string message, bool isNumber = false)
        {
            Console.WriteLine(message);
            string input = string.Empty;
            if (isNumber)
            {
                if(int.TryParse(Console.ReadLine(), out int result))
                {
                    input = result.ToString();
                }
            }
            else
            {
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
