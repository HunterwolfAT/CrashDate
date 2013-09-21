using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace CrashDate
{
    public class Audio
    {
        public Boolean mute = false;
        public float sfxvolume = 1.0f;
        public float musicvolume = 0.3f;

        int stream = 0;

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
            Console.WriteLine("Trying to play song...");
            
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                stream = Bass.BASS_StreamCreateFile("Content\\Audio\\Songs\\" + song, 0, 0, BASSFlag.BASS_DEFAULT);
            else
                Console.WriteLine("ERROR: Couldnt initialize Audiodevice.");

            if (stream != 0)
            {
                Bass.BASS_ChannelPlay(stream, true);
                Console.WriteLine("Success! Playing Song!");
            }
            else
                Console.WriteLine("...but couldn't!");
        }

        public void PlaySFX(String sfx, ContentManager cm)
        {
            SFX = cm.Load<SoundEffect>("Audio\\" + sfx);
            SFX.Play(sfxvolume, 0f, 0f);
        }

        public void Update()
        {
            Console.WriteLine("Song Duration (in Bytes): " + Bass.BASS_ChannelGetPosition(stream) + "/" + Bass.BASS_ChannelGetLength(stream).ToString());
            // If the song plays out - repeat it!
            if (Bass.BASS_ChannelGetPosition(stream) == Bass.BASS_ChannelGetLength(stream))
                Bass.BASS_ChannelPlay(stream, true);
        }
    }
}
