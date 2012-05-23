using System;

namespace ScratchyXna
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (WhackAMole game = new WhackAMole())
            {
                game.Run();
            }
        }
    }
#endif
}
