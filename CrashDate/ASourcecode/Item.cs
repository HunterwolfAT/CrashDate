using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    public class Item
    {
        String name;
        Sprite picture;

        public Item(String Name, String Picture, ContentManager myContentManager) {
            name = Name;
            picture = new Sprite();
            picture.LoadContent(myContentManager, Picture);
        }
    }
}
