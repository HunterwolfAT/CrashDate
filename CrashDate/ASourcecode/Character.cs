using Microsoft.Xna.Framework;
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
        Vector2 position;
        public bool active;
        int opacity;
        int fading;
        int fadestep = 10;
        bool moving;
        int movespeed = 0;
        int movetarget = 0;
        int movecounter = 0;
        int actbody;
        int actface;

        int sympathy = 0;

        public Character(String Name, List<String> bodiepics, List<String> facepics, ContentManager myContentManager) {
            name = Name;
            actbody = 0;
            actface = 0;

            active = false;
            fading = 0;
            opacity = 0;

            position = new Vector2(640, 380);

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
                    active = false;
                }
                if (opacity <= 0)
                {
                    opacity = 0;
                    fading = 0;
                }
            }

            if (moving)
            {
                if (movespeed < 7 && movecounter > 4 && Math.Abs(position.X - movetarget) > 15)
                {
                    movespeed+=2;
                    movecounter = 0;
                }
                if (movespeed >= 7 && movecounter > 2 && Math.Abs(position.X - movetarget) <= 30)
                {
                    movespeed -= 2;
                    movecounter = 0;
                    if (movespeed <= 0)
                        movespeed = 1;
                }

                if (movetarget > position.X)
                {
                    position = new Vector2(position.X + movespeed, position.Y);
                    if (movetarget <= position.X)
                    {
                        position = new Vector2(movetarget, position.Y);
                        moving = false;
                    }
                }
                else
                {
                    position = new Vector2(position.X - movespeed, position.Y);
                    if (movetarget >= position.X)
                    {
                        position = new Vector2(movetarget, position.Y);
                        moving = false;
                    }
                }
                movecounter++;
            }
        }

        public void Draw(SpriteBatch mySpriteBatch) {
            if (active || fading != 0)
            {
                bodies[actbody].Position = position;
                bodies[actbody].Color = new Color(255 - opacity, 255 - opacity, 255 - opacity, 255 - opacity);
                bodies[actbody].Draw(mySpriteBatch);
            }
            //faces[actface].Position = new Vector2(1120, 380);
            //faces[actface].Draw(mySpriteBatch);
        }

        public void Move(int target)
        {
            movetarget = target;
            movespeed = 0;
            moving = true;
            movecounter = 0;
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

        public void IncreaseSympathy(int value) {sympathy += value;}

        public void DecreaseSympathy(int value) {sympathy -= value;}

        public int GetSympathy()    {return sympathy;}

        public bool IsMoving() { return moving; }

        public void SetOpacity(int value) { opacity = value; }
    }
}
