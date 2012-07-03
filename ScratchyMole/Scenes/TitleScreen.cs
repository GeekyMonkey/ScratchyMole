#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ScratchyXna
{
    /// <summary>
    /// The Title Screen
    /// </summary>
    public class TitleScreen : Scene
    {
        // Title screen text objects
        Text StartText;
        //Title Screen variables
        TitleMoleSprite TitleMole;

        /// <summary>
        /// Load the title screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";
            TitleMole = AddSprite<TitleMoleSprite>();
            TitleMole.Y = -105;

            //Add the texts
            StartText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Center,
                Scale = .4f,
                Position = new Vector2(0, 80),
                Color = Color.White,
                AnimationType = TextAnimations.Throb,
                AnimationSeconds = 0.5,
                AnimationIntensity = 0.5,
                Value = "Whack a Jerk"
            });
        }

        /// <summary>
        /// Start the title screen
        /// This happens at the beginning of the game, and when you're done playing
        /// </summary>
        public override void StartScene()
        {
            TitleMole.GlideTo(new Vector2(0, 0), 2);
        }

        /// <summary>
        /// Update the title screen
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // click starts the game
            if (
                (Mouse.Button1Pressed() && TitleMole.IsTouching(Mouse.Position))
                ||
                (TitleMole.IsTouching(Touch.Taps))
                )
            {
                ShowScene("Play");
            }
        }
    }
}
