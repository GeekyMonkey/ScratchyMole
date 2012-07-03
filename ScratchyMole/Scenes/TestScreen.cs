#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ScratchyXna
{
    public class TestScreen : ScratchyXna.Scene
    {
        Text ScoreText;
        Text RestartText;

        /// <summary>
        /// Load the game over screen
        /// </summary>
        public override void Load()
        {
            GridStyle = GridStyles.Ticks;
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";

            // Add the score text
            ScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(-100f, 100f),
                Color = Color.White
            });

            // Add the start key text
            RestartText = AddText(new Text
            {
                Value = "Press SPACE to Play Again",
                Position = new Vector2(0f, -100f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Bottom,
                AnimationType = TextAnimations.Typewriter,
                AnimationSeconds = 0.2,
                Scale = 0.5f,
                Color = Color.Lime
            });
            if (Game.Platform == GamePlatforms.XBox)
            {
                RestartText.Value = "Press START to Play Again";
            }
            else if (Game.Platform == GamePlatforms.WindowsPhone)
            {
                RestartText.Value = "TAP to Play Again";
            }
        }


        /// <summary>
        /// Start the game over screen
        /// </summary>
        public override void StartScene()
        {
            // Display the final score
            ScoreText.Value = "Score: Kickass"; // +SpaceInvaders.score;
        }


        /// <summary>
        /// Update the game over screen
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        public override void Update(GameTime gameTime)
        {
            if (Mouse.Button1Pressed())
            {

            }

            // Space key to play again
            if (Keyboard.KeyPressed(Keys.Space))
            {
                //todo: also do this for phone tap
                //todo: also do this for xbox a button
                ShowScene("play");
            }

            // Escape key to go back to the title screen
            if (Keyboard.KeyPressed(Keys.Escape))
            {
                //todo: also do this for the xbox b button
                ShowScene("title");
            }            
        }
    }
}
