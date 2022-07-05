using System;

namespace PickUpTheCrewGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PickUpTheCrewGame game = new PickUpTheCrewGame())
            {
                game.Run();
            }
        }
    }
#endif
}

