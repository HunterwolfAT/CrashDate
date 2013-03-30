using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    public class CharacterManager
    {
        private List<Character> characters;
        Vector2 LeftPos = new Vector2(330, 380);
        Vector2 RightPos = new Vector2(860, 380);
        Vector2 CenterPos = new Vector2(640, 380);

        Character layoutleftchar = null;
        Character layoutrightchar = null;
        bool layout2process = false;

        public CharacterManager()
        {
            characters = new List<Character>();
        }

        public void Update()
        {
            foreach (Character cha in characters)
            {
                if (cha.active)
                    cha.Update();
            }

            if (layout2process)
            {
                Console.WriteLine("YEEEEEAAAAAAAH");
                if (!layoutleftchar.IsMoving())
                {
                    layoutrightchar.Move((int)RightPos.X);
                    layoutrightchar.Fade(true);

                    layoutleftchar = null;
                    layoutrightchar = null;
                    layout2process = false;
                    Console.WriteLine("WOOOOOOOOOP");
                }
            }
        }

        public void LoadCharacters(ContentManager myContentManager)
        {
            // ############## ALL THE INGAME CHARACTERS ARE DEFINED HERE ################

            // Dummy Player Char
            List<String> body = new List<String>();
            List<String> face = new List<String>();
            AddCharacter("Player", body, face, myContentManager);

            // Testy
            body.Clear();
            face.Clear();
            body.Add("testy_normal");
            body.Add("testy_angry");
            body.Add("testy_happy");
            body.Add("testy_shy");
            AddCharacter("Testy", body, face, myContentManager);

            // Blondie
            body.Clear();
            face.Clear();
            body.Add("blondie1");
            AddCharacter("Blondie", body, face, myContentManager);
        }

        public void AddCharacter(String name, List<String> bodies, List<String> faces, ContentManager myContentManager)
        {
            Character newchar = new Character(name, bodies, faces, myContentManager);
            characters.Add(newchar);
        }

        public Character GetChar(String name)
        {
            foreach(Character cha in characters)
            {
                if (cha.name == name)
                    return cha;
            }
            return null;
        }

        public void Draw(SpriteBatch mySpriteBatch)
        {
            foreach (Character cha in characters)
            {
                cha.Draw(mySpriteBatch);
            }
        }

        public int NumberActiveChars()
        {
            int num = 0;
            foreach (Character cha in characters)
            {
                if (cha.active)
                {
                    num++;
                }
            }

            return num;
        }

        public void TwoCharLayout(Character left, Character right)
        {
            left.Move((int)LeftPos.X);
            layoutleftchar = left;
            layoutrightchar = right;
            layout2process = true;
        }
    }
}
