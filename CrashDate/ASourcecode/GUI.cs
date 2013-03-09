using Microsoft.Xna.Framework;
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
        // Text-Font
        SpriteFont msgfont;

        // Global Sprites
        Sprite background;

        // Sprites for Events
        Sprite textbox;
        float textboxopac = 0.5f;

        // Menu Sprites

        public GUI(ContentManager myContentManager) {
            background = new Sprite();
            background.LoadContent(myContentManager, "Graphics\\527322");

            textbox = new Sprite();
            textbox.LoadContent(myContentManager, "Graphics\\textbox");
            textbox.Position = new Microsoft.Xna.Framework.Vector2(960, 850);

            msgfont = myContentManager.Load<SpriteFont>("Fonts\\MSGFont");
            
        }

        public void PrintMSGText(SpriteBatch mySpriteBatch, String message, Vector2 position, Color color, Color bordercolor)
        {
            float scale = 1f;   // The thickness of the font-border

            bordercolor = Color.Black;

            // Draw the border
            mySpriteBatch.DrawString(msgfont, message, position + new Vector2(1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, message, position + new Vector2(-1 * scale, -1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, message, position + new Vector2(-1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, message, position + new Vector2(1 * scale, -1 * scale), bordercolor);
            
            // Draw the actual message
            mySpriteBatch.DrawString(msgfont, message, position, color);

        }

        public void Draw(SpriteBatch mySpriteBatch) {
            background.Draw(mySpriteBatch);
            textbox.Draw(mySpriteBatch, textboxopac);
            PrintMSGText(mySpriteBatch, "Es ist so ein schöner Tag!\nKomm Senpai, lass und Schlittschulaufen gehen! Es sieht so herrlich aus!", new Vector2(60, 700), Color.White, Color.Black);
        }
    }
}
