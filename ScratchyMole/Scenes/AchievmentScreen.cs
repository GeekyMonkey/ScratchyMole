#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ScratchyXna;
#endregion


namespace ScratchyXna.Screens
{
    public class AchievmentScreen : Scene
    {
        // Variables
        public Dictionary<AchievmentTypes, bool> AchievmentsEarned = new Dictionary<AchievmentTypes,bool>();

        //Sprites on the achievment screen
        public Dictionary<AchievmentTypes, AchievmentThingSprite> AchievmentSprites = new Dictionary<AchievmentTypes, AchievmentThingSprite>();
        
        // Texts on the achievment screen
        Text AchievmentScreenText;

        /// <summary>
        /// Load the achievment screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";

            // Add the sprites
            AchievmentTypes a = AchievmentTypes.FirstHit;
            foreach (AchievmentTypes type in a.GetAllValues())
            {
                bool Earned;
                Earned = PlayerData.GetBool(type.ToString(), false);
                AchievmentsEarned.Add(type, Earned);
                AchievmentSprites.Add(type, AddSprite<AchievmentThingSprite>());
                AchievmentSprites[type].SetAchievmentType(type);
            }

            // Add the "Achievments" text
            AchievmentScreenText = AddText(new Text
            {
                Value = "Achievments",
                Position = new Vector2(0f, 70f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Center,
                Scale = 0.5f,
                Color = Color.Lime
            });
        }

        /// <summary>
        /// Start the achievment screen
        /// </summary>
        public override void StartScreen()
        {
            // Show or hide the sprites depending on if they were earned
            foreach (var typeEarned in AchievmentsEarned)
            {
                AchievmentThingSprite sprite = AchievmentSprites[typeEarned.Key];
                if (typeEarned.Value == true)
                {
                    sprite.GhostEffect = 0;
                }
                else
                {
                    sprite.GhostEffect = 70;
                }
            }
        }


        public void AchievmentEarned(AchievmentTypes achievmentType)
        {
            AchievmentsEarned[achievmentType] = true;
        }

        /// <summary>
        /// Update the achievment screen
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        public override void Update(GameTime gameTime)
        {

        }
    }
}