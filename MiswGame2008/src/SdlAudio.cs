using System;
using Yanesdk.Ytl;
using Yanesdk.Sound;

namespace MiswGame2008
{
    public class SdlAudio : IAudio
    {
        private Yanesdk.Sound.Sound[] sounds;
        private Yanesdk.Sound.Sound[] musics;
        private Music currentMusic;

        public SdlAudio()
        {
            Initialize();
            currentMusic = (Music)(-1);
        }

        private void Initialize()
        {
            sounds = new Yanesdk.Sound.Sound[Utility.GetEnumCount(typeof(Sound))];
            for (int i = 0; i < sounds.Length; i++)
            {
                string path = "dtf/sounds/" + Enum.GetName(typeof(Sound), i) + ".wav";
                Console.WriteLine("効果音「" + path + "」を読み込みます。");
                sounds[i] = LoadSoundByPath(path);
            }
            musics = new Yanesdk.Sound.Sound[Utility.GetEnumCount(typeof(Music))];
            for (int i = 0; i < musics.Length; i++)
            {
                string path = "dtf/musics/" + Enum.GetName(typeof(Music), i) + ".ogg";
                Console.WriteLine("BGM「" + path + "」を読み込みます。");
                musics[i] = LoadMusicByPath(path);
            }
        }

        private Yanesdk.Sound.Sound LoadSoundByPath(string path)
        {
            Yanesdk.Sound.Sound sound = new Yanesdk.Sound.Sound();
            YanesdkResult result = sound.Load(path);
            if (result == YanesdkResult.NoError)
            {
                sound.Volume = 0.75f;
                return sound;
            }
            else
            {
                throw new Exception("効果音「" + path + "」が読み込めません＞＜");
            }
        }

        private Yanesdk.Sound.Sound LoadMusicByPath(string path)
        {
            Yanesdk.Sound.Sound musics = new Yanesdk.Sound.Sound();
            YanesdkResult result = musics.Load(path, -1);
            if (result == YanesdkResult.NoError)
            {
                musics.Loop = -1;
                return musics;
            }
            else
            {
                throw new Exception("BGM「" + path + "」が読み込めません＞＜");
            }
        }

        public void PlaySound(Sound sound)
        {
            sounds[(int)sound].Play();
        }

        public void PlayMusic(Music music)
        {
            if (currentMusic == music)
            {
                return;
            }
            StopMusic();
            currentMusic = music;
            musics[(int)music].PlayFade(500);
        }

        public void StopMusic()
        {
            if ((int)currentMusic == -1)
            {
                return;
            }
            else
            {
                musics[(int)currentMusic].Stop();
                currentMusic = (Music)(-1);
            }
        }
    }
}
