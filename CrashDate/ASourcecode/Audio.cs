using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate.ASourcecode
{
    class Audio
    {
        public Boolean mute = false;
        Song FirstSong;
        Song SecondSong;

        ContentManager myContentManager;

        public Audio(ContentManager contentman)
        {
            myContentManager = contentman;
        }

        public void PlaySong(String song)
        {
            if (FirstSong == null)
            {
                FirstSong = myContentManager.Load<Song>("Audio\\Songs\\" + song);
                SecondSong = null;
                MediaPlayer.Play(FirstSong);
            }
            else if (SecondSong == null)
            {
                SecondSong = myContentManager.Load<Song>("Audio\\Songs\\" + song);
                FirstSong = null;
                MediaPlayer.Play(SecondSong);
            }
        }
    }
}
