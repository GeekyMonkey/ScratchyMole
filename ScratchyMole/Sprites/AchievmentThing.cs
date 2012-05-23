#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchyXna;
using Microsoft.Xna.Framework;
#endregion


namespace ScratchyXna
{
    public class AchievmentThingSprite : Sprite
    {
        AchievmentTypes AchievmentType;

        public override void Load()
        {
            SetCostume("Achievments/FirstHitAchievment");
            Scale = 0.5f;
            Layer = 101;
            Position = new Vector2(0, -110);
        }

        public void SetAchievmentType(AchievmentTypes acheivmentType)
        {
            AchievmentType = acheivmentType;
            switch (acheivmentType)
            {
                case AchievmentTypes.FirstHit:
                    SetCostume("Achievments/FirstHitAchievment");
                    break;
                case AchievmentTypes.Hit100:
                    SetCostume("Achievments/Hit100Achievment");
                    break;
                case AchievmentTypes.Level10:
                    SetCostume("Achievments/Level10Achievment");
                    break;
            }
        }
        
        public void AchievmentRise(AchievmentTypes acheivmentType)
        {
            SetAchievmentType(acheivmentType);
            Costume.YCenter = VerticalAlignments.Top;
            GlideTo(new Vector2(0, -84), 1);
            Wait(3, AchievmentDrop);
        }

        void AchievmentDrop()
        {
            GlideTo(new Vector2(0, -110), 1);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}