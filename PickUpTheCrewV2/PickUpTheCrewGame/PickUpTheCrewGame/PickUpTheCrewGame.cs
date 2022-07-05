using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace PickUpTheCrewGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PickUpTheCrewGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite Title, GameOver, Background, LevelUp;

        Captain WhiteBall, PirateShip, PirateShip2;

        Shark BlackBall, SharkFin;

        Crew CrewSprite, RedBall, OrangeBall, PinkBall, YellowBall, GreenBall, BlueBall;

        SpriteFont messageFont;

        StreamWriter textOut, HighScoreOut;

        StreamReader HighScoreIn, textIn;

        SoundEffect PickedUpSound, GameOverSound, LevelUpSound;

        Song TitleSong;

        static float delay = 3500f, elapsed = 0;

        int ScreenWidth, ScreenHeight, Score, HighScore, CrewCount, sharkCount;

        static int seed = 1;
        static int tree = 1;

        string Name, Status;

        bool Visible = true;

        List<Crew> DrawCrew = new List<Crew>();
        List<Shark> DrawShark = new List<Shark>();

        public Random Place = new Random(seed);
        public Random Shark = new Random(seed);
        // places the crew and shark on the screen
        public enum gamestate
        {
            startscreen,
            playingGame,
            LevelUp,
            gameover
        }
        gamestate currentgamestate = gamestate.startscreen;
        
        public PickUpTheCrewGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        } 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            Background = new Sprite(
                Content.Load<Texture2D>("Background"),
                new Rectangle(0, 0, 800, 600),
                Vector2.Zero,
                ScreenWidth, ScreenHeight);

            Title = new Sprite(
               Content.Load<Texture2D>("Menu"),
               new Rectangle(0, 0, 800, 600),
               Vector2.Zero,
               ScreenWidth, ScreenHeight);

            LevelUp = new Sprite(
               Content.Load<Texture2D>("LevelUp"),
               new Rectangle(0, 0, 800, 600),
               Vector2.Zero,
               ScreenWidth, ScreenHeight);

            GameOver = new Sprite(
               Content.Load<Texture2D>("GameOver"),
               new Rectangle(0, 0, 800, 600),
               Vector2.Zero,
               ScreenWidth, ScreenHeight);

            PirateShip = new Captain(
            Content.Load<Texture2D>("pirateship"),
            new Rectangle(400, 100, 100, 70),
           new Vector2(20, 400),
            ScreenWidth, ScreenHeight);

            PirateShip2 = new Captain(
               Content.Load<Texture2D>("pirateship2"),
               new Rectangle(20, 100, 100, 70),
                new Vector2(600, 400),
               ScreenWidth, ScreenHeight);

            SharkFin = new Shark(
               Content.Load<Texture2D>("sharkfin"),
               new Rectangle(0, 0, 60, 40),
               new Vector2(900, 425),
               ScreenWidth, ScreenHeight);

            CrewSprite = new Crew(
               Content.Load<Texture2D>("crew2"),
               new Rectangle(120, 100, 60, 40),
              new Vector2(600, 425),
               ScreenWidth, ScreenHeight,
               "name");

            WhiteBall = new Captain(
                 Content.Load<Texture2D>("WhiteBall"),
                 new Rectangle(0, 0, 40, 40),
                 Vector2.Zero,
                 ScreenWidth, ScreenHeight);

            BlackBall = new Shark(
                 Content.Load<Texture2D>("BlackBall"),
                 new Rectangle(0, 0, 40, 40),
                 Vector2.Zero,
                 ScreenWidth, ScreenHeight);

            GreenBall = new Crew(
                 Content.Load<Texture2D>("GreenBall"),
                 new Rectangle(0, 0, 40, 40),
                 Vector2.Zero,
                 ScreenWidth, ScreenHeight,
                 "Master Bates");

            PinkBall = new Crew(
                Content.Load<Texture2D>("PinkBall"),
                new Rectangle(0, 0, 40, 40),
                Vector2.Zero,
                ScreenWidth, ScreenHeight,
                "Mistress Pink");

            YellowBall = new Crew(
               Content.Load<Texture2D>("YellowBall"),
               new Rectangle(0, 0, 40, 40),
               Vector2.Zero,
               ScreenWidth, ScreenHeight,
               "Seaman Staines");

            BlueBall = new Crew(
                Content.Load<Texture2D>("BlueBall"),
                new Rectangle(0, 0, 40, 40),
                Vector2.Zero,
                ScreenWidth, ScreenHeight,
                "Rodger the Cabin boy");

            OrangeBall = new Crew(
               Content.Load<Texture2D>("OrangeBall"),
               new Rectangle(0, 0, 40, 40),
               Vector2.Zero,
               ScreenWidth, ScreenHeight,
               "Agent Orange");

            RedBall = new Crew(
                Content.Load<Texture2D>("RedBall"),
                new Rectangle(0, 0, 40, 40),
                Vector2.Zero,
                ScreenWidth, ScreenHeight,
                "The Scarlet Pimpenel");

            messageFont = Content.Load<SpriteFont>("SpriteFont");
            // adds sprites to lists for mangement ease
            DrawShark.Add(BlackBall);
            DrawCrew.Add(BlueBall);
            DrawCrew.Add(GreenBall);
            DrawCrew.Add(OrangeBall);
            DrawCrew.Add(PinkBall);
            DrawCrew.Add(RedBall);
            DrawCrew.Add(YellowBall);

            PickedUpSound = Content.Load<SoundEffect>("PickedUpCrewSong");
            GameOverSound = Content.Load<SoundEffect>("GameOverSound");
            LevelUpSound = Content.Load<SoundEffect>("LevelUpSong");
            TitleSong = Content.Load<Song>("TitleScreenSong"); //(alestorm, 2009. track used with permission)
           

            MediaPlayer.Play(TitleSong);
            // this loads the high score at the start of the game so there is constantly a high score file
            HighScoreIn = new StreamReader("ScoreSave.txt");
            HighScore = int.Parse(HighScoreIn.ReadLine());
            HighScoreIn.Close();
        }
        public void PlaceCrew()
        {
            //places the crew on screen in random postions
            Place = new Random(seed);
            foreach (Crew t in DrawCrew)
            {
                t.SpritePosition.X = Place.Next(0, ScreenWidth - t.SpriteRectangle.Width);
                t.SpritePosition.Y = Place.Next(0, ScreenHeight - t.SpriteRectangle.Height);
            }
        }
       
        public void PlaceShark()
        {
            // places the sharks on screen at a certain distance so the shark doesnt chase the player from the start
            Shark = new Random(seed);
            for (int i = 0; i < DrawShark.Count; i++)
            {
                DrawShark[i].SpritePosition.X = Place.Next(0, ScreenWidth - DrawShark[i].SpriteRectangle.Width);
                DrawShark[i].SpritePosition.Y = Place.Next(0, ScreenHeight - DrawShark[i].SpriteRectangle.Height);

                while(DrawShark[i].Distance(WhiteBall) < 200)
                {
                    seed = seed + 1;
                    DrawShark[i].SpritePosition.X = Place.Next(0, ScreenWidth - DrawShark[i].SpriteRectangle.Width);
                    DrawShark[i].SpritePosition.Y = Place.Next(0, ScreenHeight - DrawShark[i].SpriteRectangle.Height);
                }
           }
        }
        public void StartGame()
        {
            WhiteBall.SpritePosition.X = ScreenWidth / 2;
            WhiteBall.SpritePosition.Y = ScreenHeight / 2;
            PlaceCrew();
            PlaceShark();
                
        }
        public void PLayingGame()
        {
            WhiteBall.Update(this, GameOverSound, PickedUpSound, LevelUpSound);
            // checks to see if the player is within distance of the shark and updates the chase condition if it is     
            foreach (Shark s in DrawShark)
            {
                if (WhiteBall.Distance(s) < 200)
                {
                    s.Update(this, WhiteBall, GameOverSound, PickedUpSound, LevelUpSound);
                }
                if (WhiteBall.IntersectsWith(s))
                {
                    GameOverSound.Play();
                    currentgamestate = gamestate.gameover;
                }
            }
            // checks for intersection and updates the crew sprites and game conditions
            foreach (Crew t in DrawCrew)
            {
                if (WhiteBall.IntersectsWith(t))
                {
                    PickedUpSound.Play();
                    t.DrawOffScreen(); // makes the crew invisible
                    Name = t.Name; // puts the name of the crew into the draw string
                    Score = Score + 100;
                    CrewCount = CrewCount + 1; // this increments a count of picked up crew. once the count reaches 6, the next level is implemented
                    WhiteBall.CaptainSpeed = WhiteBall.CaptainSpeed - (WhiteBall.CaptainSpeed / 100 * 10); // this decreases the spped of the captain by 10% each time
                }
                // sets a winning condition
                if (CrewCount == 6 && WhiteBall.SpritePosition.X < 0)
                {
                    LevelReset();
                }
                if (CrewCount == 6 && WhiteBall.SpritePosition.Y < 0)
                {
                    LevelReset();
                }
                if (CrewCount == 6 && WhiteBall.SpritePosition.X + WhiteBall.SpriteRectangle.Width > ScreenWidth)
                {
                    LevelReset();
                }
                if (CrewCount == 6 && WhiteBall.SpritePosition.Y + WhiteBall.SpriteRectangle.Height > ScreenHeight)
                {
                    LevelReset();
                }
            }
        }
        // resets the level
        public void LevelReset()
        {
            LevelUpSound.Play();
            currentgamestate = gamestate.LevelUp;
            DrawShark.Add(new Shark(BlackBall));
            sharkCount = DrawShark.Count;
            CrewCount = 0;
            seed = seed + 1;
            WhiteBall.CaptainSpeed = 3.0f;
        }

        // resets the game
        public void ResetGame()
        {
            sharkCount = 1;
            CrewCount = 0;
            Score = 0;
            DrawShark.Clear();
            DrawShark.Add(BlackBall);
            Visible = true;
            seed = 1;
            WhiteBall.CaptainSpeed = 3.0f;

        }
        protected override void Update(GameTime gameTime)
        {
            if (Status == "Playing")
            {
                MediaPlayer.Resume();
            }
            else if (Status == "Stopped")
            {
                MediaPlayer.Stop();
            }
            switch (currentgamestate)
            {
                case gamestate.startscreen:
                    Status = "Playing";
                    // loads all positions from a txt file via the load game method in the sprite class
                    if (Keyboard.GetState().IsKeyDown(Keys.L))
                    {
                        textIn = new StreamReader("GameSave.txt");
                        seed = int.Parse(textIn.ReadLine());
                        Score = int.Parse(textIn.ReadLine());
                        WhiteBall.loadGame(textIn);
                        WhiteBall.CaptainSpeed = float.Parse(textIn.ReadLine());
                        CrewCount = int.Parse(textIn.ReadLine());
                        foreach (Crew t in DrawCrew)
                            t.loadGame(textIn);
                        sharkCount = int.Parse(textIn.ReadLine());
                        for (int i = 0; i < sharkCount; i++)
                        {
                            if (i >= DrawShark.Count)
                                DrawShark.Add(new Shark(BlackBall));
                            DrawShark[i].loadGame(textIn);
                        }
                        currentgamestate = gamestate.playingGame;
                        Status = "Stopped";
                        textIn.Close();
                    }
                    // this creates a loop to update the attract screen until space bar has been pressed
                    if (Visible == true)
                    {
                        PirateShip.MoveAttract();
                        if (PirateShip.IntersectsWith(CrewSprite))
                        {
                            PirateShip.SpritePosition = new Vector2(20, 400);
                            Visible = false;
                            break;
                        }
                    } 
                    if (Visible == false)
                    {
                        PirateShip2.OppositeAttract();
                        SharkFin.SharkAttract();
                        if (PirateShip2.IntersectsWith(SharkFin))
                        {
                            PirateShip2.SpritePosition = new Vector2(600, 400);
                            SharkFin.SpritePosition = new Vector2(900, 425);
                            Visible = true;
                            break;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        currentgamestate = gamestate.playingGame;
                        Status = "Stopped";
                        StartGame();
                    }
                    break;
                case gamestate.playingGame:
                    // this saves the game via the save game method in the sprite class
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        textOut = new StreamWriter("GameSave.txt");
                        textOut.WriteLine(seed);
                        textOut.WriteLine(Score);
                        WhiteBall.saveGame(textOut);
                        textOut.WriteLine(WhiteBall.CaptainSpeed);
                        textOut.WriteLine(CrewCount);
                        foreach (Crew t in DrawCrew)
                            t.saveGame(textOut);
                        textOut.WriteLine(sharkCount);
                        foreach (Shark s in DrawShark)
                            s.saveGame(textOut);
                        textOut.Close();
                        this.Exit();
                    }
                    PLayingGame();
                    break;
                case gamestate.LevelUp:
                    Status = "Stopped";
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        currentgamestate = gamestate.playingGame;
                        StartGame();
                    }
                    break;
                case gamestate.gameover:
                    // this checks to see if a highscore has been set and saves it to a txt file
                    if (Score > HighScore)
                    {
                        HighScore = Score;
                        HighScoreOut = new StreamWriter("ScoreSave.txt");
                        HighScoreOut.WriteLine(HighScore);
                        HighScoreOut.Close();
                    }
                    ResetGame();
                    // this takes a predefined time and once it has been reached, automaticly resets to the attract screen and resets the elapsed time to zero
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= delay)
                    {
                        currentgamestate = gamestate.startscreen;
                        elapsed = 0f;
                        Status = "Playing";

                    }
                    break;
            }

            base.Update(gameTime);
        }
        public void DrawGame()
        {
            WhiteBall.Draw(spriteBatch);
            foreach (Crew c in DrawCrew)
                c.Draw(spriteBatch);
            foreach (Shark s in DrawShark)
                s.Draw(spriteBatch);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (currentgamestate)
            {
                // this creates a loop to draw the title screen automaticly
                case gamestate.startscreen:
                    Title.Draw(spriteBatch);
                    if (Visible == true)
                    {
                        PirateShip.Draw(spriteBatch);
                        CrewSprite.Draw(spriteBatch);
                    }
                    if (Visible == false)
                    {
                        PirateShip2.Draw(spriteBatch);
                        SharkFin.Draw(spriteBatch);
                    }
                    break;

                case gamestate.playingGame:
                    // this is all drawn in order so the background does not hide the sprites
                    Background.Draw(spriteBatch);
                    spriteBatch.DrawString(messageFont, "you have picked up " + Name + " and your score is " + Score, new Vector2(100, 20), Color.Black);
                    DrawGame();
                    break;
                case gamestate.LevelUp:
                    LevelUp.Draw(spriteBatch);
                    break;
                case gamestate.gameover:
                    GameOver.Draw(spriteBatch);
                    spriteBatch.DrawString(messageFont, "you have been eaten by the shark! the highest score is " + HighScore, new Vector2(100, 20), Color.Black);
                    break;
            }
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
// Biblography
//Alestorm, 2009, 'Wolves of the sea', on Black sails at midnight (CD), Austria, Naplam Records.
// all in game sound effects from www.freesfx.com
