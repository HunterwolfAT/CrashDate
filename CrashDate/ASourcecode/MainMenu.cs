using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    class MainMenu
    {
        Sprite Background;
        int selecteditem;
        int MAXITEMS;
        String MenuMode;

        public MainMenu(ContentManager myContentManager)
        {
            Background = new Sprite();
            selecteditem = 0;
            MenuMode = "Title";
        }

        public void Draw(SpriteBatch mySpriteBatch)
        {
            Background.Draw(mySpriteBatch);
        }

        public void SelectUp()
        {
            if (selecteditem >= 0)
            {
                selecteditem--;
            }
        }

        public void SelectDown()
        {
            if (selecteditem < MAXITEMS)
            {
                selecteditem++;
            }
        }

        public bool Accept()
        {
            if (selecteditem == 0 && MenuMode == "Title")
            {
                return true;       // Close the menu
            }

            return false;
        }
    }
}
