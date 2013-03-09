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
        String CompleteMSG = "";
        String DisplayMSG = "";
        Boolean Idle = true;

        // Menu Sprites

        public GUI(ContentManager myContentManager) {
            background = new Sprite();
            background.LoadContent(myContentManager, "Graphics\\527322");

            textbox = new Sprite();
            textbox.LoadContent(myContentManager, "Graphics\\textbox");
            textbox.Position = new Microsoft.Xna.Framework.Vector2(960, 850);

            msgfont = myContentManager.Load<SpriteFont>("Fonts\\MSGFont");
            
        }

        public void Update()
        {
            if (CompleteMSG != DisplayMSG)
            {
                DisplayMSG = CompleteMSG.Substring(0, DisplayMSG.Length + 1);
            }
            else
                Idle = true;
        }

        public void WriteMSG(String msg)
        {
            CompleteMSG = msg;
            Idle = false;
        }

        public void PrintMSGText(SpriteBatch mySpriteBatch, Vector2 position, Color color, Color bordercolor)
        {
            float scale = 1f;   // The thickness of the font-border

            bordercolor = Color.Black;

            // Draw the border
            mySpriteBatch.DrawString(msgfont, DisplayMSG, position + new Vector2(1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, DisplayMSG, position + new Vector2(-1 * scale, -1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, DisplayMSG, position + new Vector2(-1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, DisplayMSG, position + new Vector2(1 * scale, -1 * scale), bordercolor);
            
            // Draw the actual message
            mySpriteBatch.DrawString(msgfont, DisplayMSG, position, color);

        }

        public void DrawBackground(SpriteBatch mySpriteBatch)
        {
            background.Draw(mySpriteBatch);
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            textbox.Draw(mySpriteBatch, textboxopac);
            PrintMSGText(mySpriteBatch, new Vector2(60, 700), Color.White, Color.Black);
        }
    }
}
