using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickUpTheCrewGame
{
    class Crew : Sprite
    {
        public Crew(Texture2D inTexture, Rectangle inRectangle, Vector2 inPosition, int inScreenWidth, int inScreenHeight, string inName) :
            base(inTexture, inRectangle, inPosition, inScreenWidth, inScreenHeight)
        {
            Name = inName;
        }
        //this draws the crew offscreen, effectivly picking them up
        public void DrawOffScreen()
        {
            SpritePosition.X = -100;
            SpritePosition.Y = -100;
           
        }
    }
}
