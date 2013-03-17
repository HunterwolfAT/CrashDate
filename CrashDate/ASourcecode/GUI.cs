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

        // Choice Variables
        Sprite choicebox;
        List<String> Questions;
        public Boolean showchoice = false;
        public int selectedchoice = 0;

        // Menu Sprites

        public GUI(ContentManager myContentManager) {
            background = new Sprite();
            nextbackground = new Sprite();
            //background.LoadContent(myContentManager, "Graphics\\527322");

            textbox = new Sprite();
            textbox.LoadContent(myContentManager, "Graphics\\textbox");
            textbox.Position = new Vector2(960, 850);

            choicebox = new Sprite();
            choicebox.LoadContent(myContentManager, "Graphics\\choicebox");

            msgfont = myContentManager.Load<SpriteFont>("Fonts\\MSGFont");

            Questions = new List<string>();
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

        public void PrintText(SpriteBatch mySpriteBatch, String text, Vector2 position, Color color, Color bordercolor)
        {
            float scale = 1f;   // The thickness of the font-border

            bordercolor = Color.Black;

            // Draw the border
            mySpriteBatch.DrawString(msgfont, text, position + new Vector2(1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, text, position + new Vector2(-1 * scale, -1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, text, position + new Vector2(-1 * scale, 1 * scale), bordercolor);
            mySpriteBatch.DrawString(msgfont, text, position + new Vector2(1 * scale, -1 * scale), bordercolor);

            // Draw the actual message
            mySpriteBatch.DrawString(msgfont, text, position, color);

        }

        private void DrawChoices(SpriteBatch mySpriteBatch)
        {
            if (showchoice)
            {
                int questioncounter = 0;
                foreach (String question in Questions)
                {
                    choicebox.Position = new Vector2(960, 580 - (110 * questioncounter));

                    if (questioncounter == selectedchoice)
                        choicebox.Color = Color.Red;
                    else
                        choicebox.Color = Color.White;

                    choicebox.Draw(mySpriteBatch, textboxopac);
                    PrintText(mySpriteBatch, question, new Vector2(960 - (msgfont.MeasureString(question).X / 2), 550 - (110 * questioncounter)), Color.White, Color.Black);
                    questioncounter++;
                }
            }
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
            DrawChoices(mySpriteBatch);
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
            nextbackground = new Sprite();
            nextbackground.LoadContent(myContentManager, "Graphics\\" + name);
            fadebackground = true;
            opacity = 255;
        }

        public void AddQuestion(String question)
        {
            Questions.Add(question);
        }

        public void ShowChoices()
        {
            showchoice = true;
            selectedchoice = Questions.Count() - 1;
            //Questions.Reverse();
        }

        public void CleanUpChoices()
        {
            Questions.Clear();
            showchoice = false;
        }

        public void SelectDown()
        {
            if (selectedchoice > 0)
                selectedchoice -= 1;
        }

        public void SelectUp()
        {
            if (selectedchoice <= Questions.Count() - 2)
                selectedchoice += 1;
        }
    }
}
