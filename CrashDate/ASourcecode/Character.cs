﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{    
    public class Character
    {
        public String name;
        List<Sprite> bodies;
        List<Sprite> faces;
        public bool active;
        int opacity;
        int fading;
        int fadestep = 10;
        int actbody;
        int actface;

        public Character(String Name, List<String> bodiepics, List<String> facepics, ContentManager myContentManager) {
            name = Name;
            actbody = 0;
            actface = 0;

            active = false;
            fading = 0;
            opacity = 0;

            bodies = new List<Sprite>();
            foreach (String path in bodiepics)
            {
                Sprite newbodypic = new Sprite();
                newbodypic.LoadContent(myContentManager, "Graphics\\Chars\\" + name + "\\" + path);
                bodies.Add(newbodypic);
            }

            faces = new List<Sprite>();
            foreach (String path in facepics)
            {
                Sprite newfacepic = new Sprite();
                newfacepic.LoadContent(myContentManager, "Graphics\\Chars\\" + name + "\\"  + path);
                faces.Add(newfacepic);
            }
        }

        public void Update()
        {
            if (fading != 0)
            {
                opacity += fadestep * fading;
                if (opacity >= 255)
                {
                    opacity = 255;
                    fading = 0;
                }
                if (opacity <= 0)
                {
                    opacity = 0;
                    fading = 0;
                }
            }
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            if (active)
            {
                bodies[actbody].Position = new Vector2(1100, 580);
                bodies[actbody].Color = new Color(255 - opacity, 255 - opacity, 255 - opacity, 255 - opacity);
                bodies[actbody].Draw(mySpriteBatch);
            }
            //faces[actface].Position = new Vector2(1120, 380);
            //faces[actface].Draw(mySpriteBatch);
        }

        public void ChangeFace(int face)
        {
            if (face <= faces.Count)
                actface = face;
        }

        public void ChangeBody(int body)
        {
            if (body <= bodies.Count)
                actbody = body;
        }

        public void Fade(bool fadein)
        {
            if (fadein)
            {
                fading = -1;
                opacity = 255;
            }
            else
            {
                fading = 1;
                opacity = 0;
            }
        }
    }
}
