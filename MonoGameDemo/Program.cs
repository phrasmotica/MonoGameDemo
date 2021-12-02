using System;

namespace MonoGameDemo
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new DemoGame();

            game.Run();
        }
    }
}
