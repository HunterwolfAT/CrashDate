using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{    
    class Character
    {
        String name;
        List<Sprite> bodies;
        List<Sprite> faces;

        int actbody;
        int actface;

        public Character(String Name, List<String> bodiepics, List<String> facepics, ContentManager myContentManager) {
            name = Name;
            actbody = 0;
            actface = 0;

            bodies = new List<Sprite>();
            foreach (String path in bodiepics)
            {
                Sprite newbodypic = new Sprite();
                newbodypic.LoadContent(myContentManager, path);
                bodies.Add(newbodypic);
            }

            faces = new List<Sprite>();
            foreach (String path in facepics)
            {
                Sprite newfacepic = new Sprite();
                newfacepic.LoadContent(myContentManager, path);
                faces.Add(newfacepic);
            }
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            bodies[actbody].Draw(mySpriteBatch);
            faces[actface].Draw(mySpriteBatch);
        }
    }
}
