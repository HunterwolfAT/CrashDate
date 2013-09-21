using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashDate
{
    public class Audio
    {
        public Boolean mute = false;
        public float sfxvolume = 1.0f;
        public float musicvolume = 0.3f;

        Song FirstSong;
        Song SecondSong;
        //SoundEffect ActiveSong;

        SoundEffect Speechfile;
        SoundEffect SFX;

        ContentManager myContentManager;

        public Audio(ContentManager contentman)
        {
            myContentManager = contentman;
            MediaPlayer.Volume = 0.4f;
        }

        public void PlaySong(String song)
        {
            if (FirstSong == null)
            {
                FirstSong = myContentManager.Load<Song>("Audio\\Songs\\" + song);
                SecondSong = null;
                //FirstSong.Play(musicvolume, 0f, 0f);
                MediaPlayer.Play(FirstSong);
            }
            else if (SecondSong == null)
            {
                SecondSong = myContentManager.Load<Song>("Audio\\Songs\\" + song);
                FirstSong = null;
                //SecondSong.Play(musicvolume, 0f, 0f);
                MediaPlayer.Play(SecondSong);
            }
        }

        public void PlaySFX(String sfx, ContentManager cm)
        {
            SFX = cm.Load<SoundEffect>("Audio\\" + sfx);
            SFX.Play(sfxvolume, 0f, 0f);
        }

        public void Update()
        {
            //Console.WriteLine(SecondSong.Duration);
        }
    }
}
