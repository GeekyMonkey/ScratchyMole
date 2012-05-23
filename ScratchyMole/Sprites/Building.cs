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
    public class BuildingSprite : Sprite
    {
        public override void Load()
        {
            SetCostume("Building");
            Scale = 200f / 800f;
            Layer = 10;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

    }
}