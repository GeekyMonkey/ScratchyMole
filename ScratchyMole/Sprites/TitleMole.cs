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
    public class TitleMoleSprite : Sprite
    {
        public override void Load()
        {
            SetCostume("Moles/TestTitleMole").YCenter = VerticalAlignments.Top;
            Scale = 1;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

    }
}