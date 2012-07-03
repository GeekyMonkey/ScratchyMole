#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ScratchyXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class WhackAMole : ScratchyXnaGame
    {
        // Game variables
        public static int score;
        public AchievmentScreen achievments;

        /// <summary>
        /// Load the scenes needed for the game
        /// The first one added is where the game will start
        /// </summary>
        public override void LoadScenes()
        {
            PlayerData.Load();
            float screenScale = 1f / 2f;
            SetScreenSize(480f * screenScale, 800f * screenScale);
            AddScene<TitleScreen>();
            AddScene<PlayScreen>();
            AddScene<GameOverScreen>();
            achievments = AddScene<AchievmentScreen>();
            AddScene<TestScreen>();
        }
    }
}
