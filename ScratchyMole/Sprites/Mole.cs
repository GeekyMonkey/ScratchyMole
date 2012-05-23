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
    public class MoleSprite : Sprite
    {
        public MoleStates State;
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public int PositionNum;
        public float DropSeconds = 0.9f;

        public override void Load()
        {
            SetCostume("Mole").YCenter = VerticalAlignments.Top;
            Scale = 0.3f;
            State = MoleStates.Waiting;
            Hide();
        }

        public void Pop(int positionNum, bool colorDebugMode)
        {
            if (colorDebugMode)
            {
                Color[] colors = { Color.Green, Color.Aqua, Color.Blue, Color.Indigo, Color.Violet, Color.Red, Color.Orange, Color.Yellow};
                SpriteColor = colors[positionNum];
            }
            switch (positionNum)
            {
                case 0:
                case 1:
                    Layer = 2;
                    break;
                case 2:
                case 3:
                    Layer = 4;
                    break;
                case 4:
                case 5:
                    Layer = 6;
                    break;
                case 6:
                case 7:
                    Layer = 8;
                    break;
            }
            PositionNum = positionNum;
            State = MoleStates.Rising;
            Position = StartPosition;
            Position.Y -= 25;
            EndPosition = Position;
            Show();
            GlideTo(StartPosition, 1);
        }

        void Done()
        {
            if (State == MoleStates.Dropping)
            {
                State = MoleStates.Missed;
            }
            else
            {
                State = MoleStates.Done;
            }
            (GameScreen as PlayScreen).MolesNeedCleanup();
        }

        public void Hit()
        {
            if (State != MoleStates.Dropping)
            {
                // If it was dropping, then Done has already been scheduled
                // Don't need to schedule it twice
                Wait(0.4f, Done);
            }
            State = MoleStates.Hit;
            SpriteColor = Color.Red;
            GlideTo(EndPosition, 0.4f);
        }

        /// <summary>
        /// Start moving the mole down
        /// </summary>
        public void Drop()
        {
            // If it was hit, then it's already going down
            if (State != MoleStates.Hit)
            {
                State = MoleStates.Dropping;
                GlideTo(EndPosition, DropSeconds);
                Wait(DropSeconds, Done);
            }
        }

        /// <summary>
        /// How long the mole stays at the top position
        /// </summary>
        public float UpSeconds
        {
            get
            {
                // Between 0.0 and 1.0 seconds
                return (float)Random.NextDouble();
            }
        }

        /// <summary>
        /// Mole update (60 times a second)
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (State == MoleStates.Rising)
            {
                if (Position == StartPosition)
                {
                    State = MoleStates.Up;
                    Wait(UpSeconds, Drop);
                }
            }
        }

    }
}