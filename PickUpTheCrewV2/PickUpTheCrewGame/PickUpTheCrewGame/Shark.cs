using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickUpTheCrewGame
{
    class Shark : Sprite
    {
        public Shark(Texture2D inTexture, Rectangle inRectangle, Vector2 inPosition, int inScreenWidth, int inScreenHeight) :
            base(inTexture, inRectangle, inPosition, inScreenWidth, inScreenHeight)
        {
        }
        
        public Shark(Shark inShark):
            base(inShark.SpriteTexture, inShark.SpriteRectangle, inShark.SpritePosition, inShark.ScreenWidth, inShark.ScreenHeight)
        { 
        }
         // this method overrides the update method in the parent class
        public void Update(PickUpTheCrewGame game, Sprite t, SoundEffect sharkEffect, SoundEffect crewEffect, SoundEffect LevelUpEffect)
        {
          // this updates the shark spped if a distance condition has been met, instigating a chase condition
                if (SpritePosition.X > t.SpritePosition.X)
                {
                    SpritePosition.X -= SharkSpeed;
                }
                if (SpritePosition.X < t.SpritePosition.X)
                {
                    SpritePosition.X += SharkSpeed;
                }
                if (SpritePosition.Y > t.SpritePosition.Y)
                {
                    SpritePosition.Y -= SharkSpeed;
                }
                if (SpritePosition.Y < t.SpritePosition.Y)
                {
                    SpritePosition.Y += SharkSpeed;
                }
                base.Update(game, sharkEffect, crewEffect, LevelUpEffect);
           
        }
        // sets spped for attract mode
        public void SharkAttract()
        {
            SpritePosition.X -= SharkSpeed + 3;
        }
    }
}
