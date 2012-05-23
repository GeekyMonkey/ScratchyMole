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
    public class LevelSprite : Sprite
    {
        LevelStates State;

        public override void Load()
        {
            SetCostume("levelup");
            Scale = 0.5f;
            GhostEffect = 100;
            Layer = 60;
            State = LevelStates.Waiting;
            Hide();
        }
        public void LevelUp()
        {
            State = LevelStates.Appearing;
            Show();
            GhostEffect = 100;
        }
        private void Disappear()
        {
            GhostEffect = 0;
            Hide();
            State = LevelStates.Waiting;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (State == LevelStates.Appearing)
            {
                GhostEffect -= 1f;
                if (GhostEffect < 1)
                {
                    State = LevelStates.Fading;
                }
            }
            if (State == LevelStates.Fading)
            {
                GhostEffect += 1f;
                if (GhostEffect > 99)
                {
                    Disappear();
                }
            }
        }
    }
}