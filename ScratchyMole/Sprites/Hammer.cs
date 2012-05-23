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
    public class HammerSprite : Sprite
    {
        public override void Load()
        {
            AddCostume("Hammer 2").XCenter = HorizontalAlignments.Center;
            SetCostume("Hammer 2").YCenter = VerticalAlignments.Bottom;
            Scale = 0.3f;
            Layer = 50;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GoTo(Mouse.Position);
            if (Mouse.Button1Down())
            {
                Rotation = 55;
            }
            else
            {
                Rotation = 0;
            } 
        }
    }
}