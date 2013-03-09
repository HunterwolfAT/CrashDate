using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate.ASourcecode
{
    class GUI
    {
        // Global Sprites
        Sprite background;

        // Sprites for Events

        // Menu Sprites

        public GUI() {
            background = new Sprite();
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            background.Draw(mySpriteBatch);
        }
    }
}
