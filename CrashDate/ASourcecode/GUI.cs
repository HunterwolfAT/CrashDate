using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    class GUI
    {
        // Global Sprites
        Sprite background;

        // Sprites for Events
        Sprite textbox;
        float textboxopac = 0.3f;

        // Menu Sprites

        public GUI(ContentManager myContentManager) {
            background = new Sprite();
            background.LoadContent(myContentManager, "Graphics\\527322");

            textbox = new Sprite();
            textbox.LoadContent(myContentManager, "Graphics\\textbox");
            textbox.Position = new Microsoft.Xna.Framework.Vector2(960, 850);
            
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            background.Draw(mySpriteBatch);
            textbox.Draw(mySpriteBatch, textboxopac);
        }
    }
}
