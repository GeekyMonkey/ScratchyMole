﻿#region usings
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

    public class PlayScreen : Scene
    {
        //Variables
        int money;
        int level;
        bool ColorDebugMode = false;
        bool molesDirty = false;

        // Sprites on the screen
        BuildingSprite building;
        LevelSprite levelsprite;
        AchievmentThingSprite achievmentthing;
        //HammerSprite hammer;

        //The lists
        List<MoleSprite> moles = new List<MoleSprite>(8);
        List<Vector2> moleplaces = new List<Vector2>(8);

        // Text on the screen
        Text ScoreText;
        Text MoneyText;
        Text LevelText;

        /// <summary>
        /// Load the screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";

            //Add the sounds
            AddSound("hit");

            // Create the sprites
            building = AddSprite<BuildingSprite>();
            levelsprite = AddSprite<LevelSprite>();
            achievmentthing = AddSprite<AchievmentThingSprite>();
            //hammer = AddSprite<HammerSprite>();

            //Create the texts
            ScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Right,
                VerticalAlign = VerticalAlignments.Bottom,
                Scale = .4f,
                Position = new Vector2(60f, -100f),
                Color = Color.White
            });

            MoneyText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Bottom,
                Scale = .4f,
                Position = new Vector2(-60, -100f),
                Color = Color.White
            });

            LevelText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(-60, 100),
                Color = Color.White
            });
        }
        //Add the list of places (Vector2s) that the mole can pop out of
        //TODO : Not use this list because it was made only for testing
        void AddTestMolePlaces()
        {
            moleplaces.Clear();
            //The first(top)row
            //The left
            moleplaces.Add(new Vector2(-23, 75));
            //The right
            moleplaces.Add(new Vector2(20, 75));
            //The second row
            //The left
            moleplaces.Add(new Vector2(-23, 35));
            //The right
            moleplaces.Add(new Vector2(20, 35));
            //The third row
            //The left
            moleplaces.Add(new Vector2(-23, -10));
            //The right
            moleplaces.Add(new Vector2(20, -10));
            //The last(bottom)row
            //The left
            moleplaces.Add(new Vector2(-23, -50));
            //The right
            moleplaces.Add(new Vector2(20, -50));
        }
        //Add the list of places (Vector2s) that the mole can pop out of
        //TODO : Use this list instead of the above list
        void AddFirstMolePlaces()
        {
            moleplaces.Clear();
            //The first(top)row
            //The left
            moleplaces.Add(new Vector2(-37, 36));
            //The middle
            moleplaces.Add(new Vector2(0, 36));
            //The right
            moleplaces.Add(new Vector2(38, 36));
            //The second(middle)row
            //The left
            moleplaces.Add(new Vector2(-37, -19));
            //The middle
            moleplaces.Add(new Vector2(0, -19));
            //The right
            moleplaces.Add(new Vector2(38, -19));
            //The third(bottom)row
            //The left
            moleplaces.Add(new Vector2(-37, -72));
            //The middle
            //The middle is the door, so I'm not using it for now.
            //The right
            moleplaces.Add(new Vector2(38, -72));
        }
        void MissedMoles(int missed)
        {
            if (Money > 0)
            {
                Money -= 10;
            }
            else
            {
                ShowScene("GameOver");
            }
        }

        void CleanupOldMoles()
        {
            int missed = moles.Count(m => m.State == MoleStates.Missed);
            if (missed > 0)
            {
                MissedMoles(missed);
            }
            moles.RemoveAll(m => m.State == MoleStates.Done || m.State == MoleStates.Missed);
            Sprites.RemoveAll(m => m is MoleSprite && (((MoleSprite)m).State == MoleStates.Done || ((MoleSprite)m).State == MoleStates.Missed));
            molesDirty = false;
        }

        /// <summary>
        /// Set a flag that the cleanup is needed
        /// More than one mole may call this during an update
        /// </summary>
        public void MolesNeedCleanup()
        {
            molesDirty = true;
        }

        /// <summary>
        /// Pop up a mole
        /// </summary>
        void MolePop()
        {
            List<int> availablePositions = Enumerable.Range(0, 8).ToList();
            foreach (MoleSprite mole in moles)
            {
                if (availablePositions.Contains(mole.PositionNum))
                {
                    availablePositions.Remove(mole.PositionNum);
                }
            }
            int availablePlaceCount = availablePositions.Count();

            if (availablePlaceCount > 0)
            {
                MoleSprite newMole = AddSprite<MoleSprite>();
                moles.Add(newMole);
                int positionNum = availablePositions[Random.Next(0, availablePlaceCount)];
                newMole.StartPosition = moleplaces[positionNum];
                newMole.Pop(positionNum, ColorDebugMode);
            }
            Wait(MolePopSecs, MolePop);
        }

        /// <summary>
        /// Start the play screen
        /// </summary>
        public override void StartScene()
        {
            Money = 100;
            Score = 0;
            Level = 0;
            StartLevel();

            // Start poppin
            Wait(MolePopSecs, MolePop);
        }

        /// <summary>
        /// Start the next level
        /// </summary>
        private void StartLevel()
        {
            Level += 1;

            switch (Level)
            {
                case 0:
                    building.SetCostume("Buildings/TestBuilding");
                    AddTestMolePlaces();
                    break;
                case 1:
                default:
                    building.SetCostume("Buildings/LeinsterHouse-NoWindows");
                    AddFirstMolePlaces();
                    break;
            }

            Sprites.RemoveAll(s => s is WindowSprite);
            for (int i = 0; i < moleplaces.Count; i++)
            {
                WindowSprite window = AddSprite<WindowSprite>();
                window.Position = moleplaces[i];
                window.Y -= 8;
                window.AddLayers(i);
            }

        }

        /// <summary>
        /// Time to wait before popping again
        /// </summary>
        float MolePopSecs
        {
            get
            {
                return 1f - (Level * .05f)
                    + (float)Random.NextDouble();
            }
        }

        /// <summary>
        /// Stop the play screen
        /// </summary>
        public override void StopScene()
        {
            //todo : put something here
        }

        /// <summary>
        /// Turn color debug mode off or on
        /// </summary>
        public void SetColorDebugMode(bool newColorDebugMode)
        {
            ColorDebugMode = newColorDebugMode;
            foreach (WindowSprite window in Sprites.Where(s => s is WindowSprite))
            {
                window.SetColorDebugMode(ColorDebugMode);
            }
        }

        /// <summary>
        /// Earned an achievment
        /// </summary>
        /// <param name="type">Type of achievment earned</param>
        public void GetAchievment(AchievmentTypes type)
        {
            bool IfEarned;
            IfEarned = ((WhackAMole)Game).achievments.AchievmentsEarned[type];
            if (IfEarned == false)
            {
                ((WhackAMole)Game).achievments.AchievmentEarned(type);
                achievmentthing.AchievmentRise(type);
                PlayerData.SetValue(type.ToString(), true);
            }
        }

        /// <summary>
        /// Update the play screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //temp : disco mode
            if (Keyboard.KeyDown(Keys.LeftControl))
            {
                if (Keyboard.KeyPressed(Keys.C))
                {
                    SetColorDebugMode(!ColorDebugMode);
                }
                if (Keyboard.KeyPressed(Keys.O))
                {
                    Forever(.7f, () => SetColorDebugMode(!ColorDebugMode));
                }
            }

            // Cleanup old moles - only if the list is dirty
            if (molesDirty)
            {
                CleanupOldMoles();
            }

            // Esc key goes back to the title screen
            if (Keyboard.KeyPressed(Keys.Escape))
            {
                //todo: also do this for phone back button
                //todo: also do this for xbox b button
                PlayerData.Save();
                ShowScene("Title");
            }
            if (Mouse.Button1Pressed() && (building.IsTouching(Mouse.Position) == false))
            {
                foreach (MoleSprite mole in moles)
                {
                    if (mole.IsTouching(Mouse.Position) && mole.State != MoleStates.Hit)
                    {
                        MoleHit(mole);
                    }
                }
            }

            foreach (MoleSprite mole in moles)
            {
                if (mole.IsTouching(Touch.Taps) && mole.State != MoleStates.Hit)
                {
                    MoleHit(mole);
                }
            }

        }

        /// <summary>
        /// A mole has been hit
        /// </summary>
        /// <param name="mole">The mole that was hit</param>
        void MoleHit(MoleSprite mole)
        {
            Score += 1;
            if (Score % 5 == 0)
            {
                Money += 10;
            }
            if (Score % 10 == 0)
            {
                StartLevel();
                levelsprite.LevelUp();

                //Achievment for getting to Level 10
                if (Level == 10)
                {
                    GetAchievment(AchievmentTypes.Level10);
                }
                if (Level == 2)
                {
                    GetAchievment(AchievmentTypes.LevelUpAchievment);
                }
            }
            PlaySound("hit");
            mole.Hit();

            //Achievment for punching a mole
            if (Score == 1)
            {
                GetAchievment(AchievmentTypes.FirstHit);
            }
            //Achievment for hitting 100 moles
            if (Score == 100)
            {
                GetAchievment(AchievmentTypes.Hit100);
            }
        }

        /// <summary>
        /// Score variable, but when you set it, it automatically updates the score text
        /// </summary>
        public int Score
        {
            get
            {
                return WhackAMole.score;
            }
            set
            {
                WhackAMole.score = value;
                ScoreText.Value = "Hits: " + WhackAMole.score;
            }
        }

        /// <summary>
        /// The level that the player is on. Setting it automatically updades the level text
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                LevelText.Value = "Level " + level;
            }
        }
        /// <summary>
        /// The money that the player has. Setting it automatically updades the money text
        /// </summary>
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                MoneyText.Value = "Money: " + money;
            }
        }
    }
}
