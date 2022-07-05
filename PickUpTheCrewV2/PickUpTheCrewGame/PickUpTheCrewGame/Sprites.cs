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
    class Sprite
    {
        public Texture2D SpriteTexture;
        public Rectangle SpriteRectangle;
        public Vector2 SpritePosition;
       
        public float CaptainSpeed = 3.0f;
        protected int SharkSpeed = 1, ScreenWidth, ScreenHeight;

        public static int seed = 1;
        
        public string Name;
        public Sprite(Texture2D inTexture, Rectangle inRectangle, Vector2 inPosition, int inScreenWidth, int inScreenHeight)
        {
            SpriteTexture = inTexture;
            SpriteRectangle = new Rectangle(inRectangle.X,inRectangle.Y, inRectangle.Width, inRectangle.Height);
            SpritePosition = new Vector2(inPosition.X, inPosition.Y);
            ScreenWidth = inScreenWidth;
            ScreenHeight = inScreenHeight;
        }
        //draw method for all sprites
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteRectangle.X = (int)Math.Round(SpritePosition.X);
            SpriteRectangle.Y = (int)Math.Round(SpritePosition.Y);
            spriteBatch.Draw(SpriteTexture, SpriteRectangle, Color.White);
        }
        // this is a blank method stub that will be overidden by the individual updates in the child classes
        public virtual void Update(PickUpTheCrewGame game, SoundEffect sharkEffect, SoundEffect crewEffect, SoundEffect LevelUpEffect)
        {
        }
        // this loads ebverything i need to load from a sprite
        public void loadGame(TextReader textIn)
        {
            //status = textIn.ReadLine();
            Name = textIn.ReadLine();
            SpriteRectangle.X = int.Parse(textIn.ReadLine());
            SpriteRectangle.Y = int.Parse(textIn.ReadLine());
            SpriteRectangle.Width = int.Parse(textIn.ReadLine());
            SpriteRectangle.Height = int.Parse(textIn.ReadLine());
            SpritePosition.X = float.Parse(textIn.ReadLine());
            SpritePosition.Y = float.Parse(textIn.ReadLine());
        
        }
        // this saves ebverything i need to load from a sprite
        public void saveGame(TextWriter textOut)
        {
          //  textOut.WriteLine(status);
            textOut.WriteLine(Name);
            textOut.WriteLine(SpriteRectangle.X);
            textOut.WriteLine(SpriteRectangle.Y);
            textOut.WriteLine(SpriteRectangle.Width);
            textOut.WriteLine(SpriteRectangle.Height);
            textOut.WriteLine(SpritePosition.X);
            textOut.WriteLine(SpritePosition.Y);
        }
        // this is pythag to measure distance from any given sprite from another 
        public float Distance(Sprite t)
        {
            Vector2 Object = t.SpritePosition;
            Vector2 Player = SpritePosition;

            float a = Player.X - Object.X;
            float b = Player.Y - Object.Y;

            return (float)Math.Sqrt((a * a) + (b * b));

        }
        // returns a bool to check for intersection
        public bool IntersectsWith(Sprite t)
        {
            return SpriteRectangle.Intersects(t.SpriteRectangle);
        }
    }

   
}
