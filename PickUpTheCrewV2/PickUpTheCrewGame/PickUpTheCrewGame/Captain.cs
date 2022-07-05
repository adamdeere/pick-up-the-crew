using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickUpTheCrewGame
{

    class Captain : Sprite
    {
         public Captain(Texture2D inTexture, Rectangle inRectangle, Vector2 inPosition, int inScreenWidth, int inScreenHeight) :
            base(inTexture, inRectangle, inPosition, inScreenWidth, inScreenHeight)
        {
        }
        // this method overrides the update method in the parent class
         public override void Update(PickUpTheCrewGame game, SoundEffect sharkEffect, SoundEffect crewEffect, SoundEffect LevelUpEffect)
         {
             // alows movement in the captain
             KeyboardState keystate = Keyboard.GetState();
             if (keystate.IsKeyDown(Keys.Up))
             {
                 SpritePosition.Y -= CaptainSpeed;
             }
             if (SpritePosition.Y < 0)
             {
                 SpritePosition.Y = SpritePosition.Y + 1;
             }
             if (keystate.IsKeyDown(Keys.Down))
             {
                 SpritePosition.Y += CaptainSpeed;
             }
             if (SpritePosition.Y + SpriteRectangle.Height > ScreenHeight)
             {
                 SpritePosition.Y = SpritePosition.Y - 1;
             }
             if (keystate.IsKeyDown(Keys.Left))
             {
                 SpritePosition.X -= CaptainSpeed;
             }
             if (SpritePosition.X < 0)
             {
                 SpritePosition.X = SpritePosition.X + 1;
             }
             if (keystate.IsKeyDown(Keys.Right))
             {
                 SpritePosition.X += CaptainSpeed;
             }
             if (SpritePosition.X + SpriteRectangle.Width > ScreenWidth)
             {
                 SpritePosition.X = SpritePosition.X - 1;
             }

             base.Update(game, sharkEffect, crewEffect, LevelUpEffect);
         }
        // these methods are for the attract screen
         public void MoveAttract()
         {
             SpritePosition.X += CaptainSpeed;
         }
         public void OppositeAttract()
         {
             SpritePosition.X -= CaptainSpeed;
         }
    }
}
