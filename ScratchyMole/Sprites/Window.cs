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
    public class WindowSprite : Sprite
    {
        int PositionNum;

        public override void Load()
        {
            SetCostume("window");
            Scale = 200f / 800f;
        }

        public void AddLayers(int positionNum)
        {
            PositionNum = positionNum;
            switch (positionNum)
            {
                case 0:
                case 1:
                    Layer = 1;
                    break;
                case 2:
                case 3:
                    Layer = 3;
                    break;
                case 4:
                case 5:
                    Layer = 5;
                    break;
                case 6:
                case 7:
                    Layer = 7;
                    break;
            }
        }

        public void SetColorDebugMode(bool colorDebugMode)
        {
            if (colorDebugMode)
            {
                Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Aqua, Color.Blue, Color.Indigo, Color.Violet };
                SpriteColor = colors[PositionNum];
            }
            else
            {
                SpriteColor = Color.White;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

    }
}