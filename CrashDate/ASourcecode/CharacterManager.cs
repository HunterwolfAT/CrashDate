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

        public CharacterManager()
        {
            characters = new List<Character>();
        }

        public void LoadCharacters(ContentManager myContentManager)
        {
            // ############## ALL THE INGAME CHARACTERS ARE DEFINED HERE ################

            // Testy
            List<String> body = new List<String>();
            List<String> face = new List<String>();
            body.Add("testy_normal");
            body.Add("testy_angry");
            body.Add("testy_happy");
            body.Add("testy_shy");
            AddCharacter("Testy", body, face, myContentManager);
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
    }
}
