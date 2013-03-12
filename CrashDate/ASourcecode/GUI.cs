using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    public class GUI
    {
        // Text-Font
        SpriteFont msgfont;

        // Global Sprites
        Sprite background;
        Sprite nextbackground;
        int opacity = 255;
        int fadestep = 10;
        bool fadebackground = false;

        // Sprites for Events
        Sprite textbox;
        float textboxopac = 0.5f;

        // Textbox Variables
        String CompleteMSG = "";
        String DisplayMSG = "";
        String SpeakingChar = "";
        public Boolean Idle = true;
        float MSGSpeed = 1f;
        float MSGCounter = 0f;

        // Menu Sprites

        public GUI(ContentManager myContentManager) {
            background = new Sprite();
            nextbackground = new Sprite();
            //background.LoadContent(myContentManager, "Graphics\\527322");

            textbox = new Sprite();
            textbox.LoadContent(myContentManager, "Graphics\\textbox");
            textbox.Position = new Microsoft.Xna.Framework.Vector2(960, 850);

            msgfont = myContentManager.Load<SpriteFont>("Fonts\\MSGFont");
            
        }

        public void Update()
        {
            // Update the MSGBox
            if (CompleteMSG != DisplayMSG)
            {
                MSGCounter += MSGSpeed;
                while (MSGCounter >= 1f && CompleteMSG != DisplayMSG)
                {
                    DisplayMSG = CompleteMSG.Substring(0, DisplayMSG.Length + 1);
                    MSGCounter -= 1f;
                }
            }
            else
                Idle = true;

            // Fade to a new background
            if (fadebackground)
            {
                opacity -= fadestep;
                if (opacity <= 0)
                {
                    opacity = 0;
                    background = nextbackground;
                    nextbackground = null;
                    fadebackground = false;
                }
            }
        }

        public void WriteMSG(String name, String msg)
        {
            CompleteMSG = msg;
            DisplayMSG = "";
            SpeakingChar = name;
            Idle = false;
            MSGCounter = 0f;
        }

        public void PrintMSGText(SpriteBatch mySpriteBatch, Vector2 position, Color color, Color bordercolor, Color namecolor)
        {
            float scale = 1f;   // The thickness of the font-border

            bordercolor = Color.Black;

            // Draw the border for the Name
            mySpriteBatch.DrawString(msgfont, SpeakingChar, new Vector2(position.X, position.Y - 50) + new Vector2(1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, SpeakingChar, new Vector2(position.X, position.Y - 50) + new Vector2(-1 * scale, -1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, SpeakingChar, new Vector2(position.X, position.Y - 50) + new Vector2(-1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, SpeakingChar, new Vector2(position.X, position.Y - 50) + new Vector2(1 * scale, -1 * scale), bordercolor);

            // Draw the Character Name
            mySpriteBatch.DrawString(msgfont, SpeakingChar, new Vector2(position.X, position.Y - 50), namecolor);

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
            if (fadebackground)
            {
                nextbackground.Color = new Color(255 - opacity, 255 - opacity, 255 - opacity, 255 - opacity);
                nextbackground.Draw(mySpriteBatch);
            }
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            textbox.Draw(mySpriteBatch, textboxopac);
            PrintMSGText(mySpriteBatch, new Vector2(60, 720), Color.White, Color.Black, Color.DarkBlue);
        }

        public void SetMSGSpeed(float speed)
        {
            MSGSpeed = speed;
        }

        public void SetBackground(ContentManager myContentManager, String name)
        {
            background.LoadContent(myContentManager, "Graphics\\" + name);
        }

        public void FadeBackground(ContentManager myContentManager, String name)
        {
            nextbackground.LoadContent(myContentManager, "Graphics\\" + name);
            fadebackground = true;
            opacity = 255;
        }
    }
}
