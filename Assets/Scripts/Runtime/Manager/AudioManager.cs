using UnityEngine;
using Serializable = System.SerializableAttribute;

namespace Runtime.Manager
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (!instance)
                {
                    var obj = Instantiate(Resources.Load<GameObject>("Manager/AudioManager"));
                    instance = obj.GetComponent<AudioManager>();
                    DontDestroyOnLoad(obj);
                }

                return instance;
            }
        }

        #endregion

        #region Variables

        private AudioSource musicPlayer;
        private AudioSource sfxPlayer, nextSfxPlayer;

        public bool MuteMusic;
        public bool MuteSFX;

        private bool mBlockSFXPlayer;

        private const string SFXKEY = "SFXKEY";
        private const string MUSICKEY = "MUSICKEY";

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            musicPlayer = new GameObject("Music Player").AddComponent<AudioSource>();
            musicPlayer.transform.SetParent(transform);
            musicPlayer.spatialBlend = 0;
            musicPlayer.loop = true;
            musicPlayer.volume = 1;

            sfxPlayer = new GameObject("SFX Player").AddComponent<AudioSource>();
            sfxPlayer.transform.SetParent(transform);
            sfxPlayer.spatialBlend = 0;
            sfxPlayer.loop = false;
            sfxPlayer.playOnAwake = false;
            sfxPlayer.volume = 1;

            nextSfxPlayer = new GameObject("Next SFX Player").AddComponent<AudioSource>();
            nextSfxPlayer.transform.SetParent(transform);
            nextSfxPlayer.spatialBlend = 0;
            nextSfxPlayer.loop = false;
            nextSfxPlayer.playOnAwake = false;
            nextSfxPlayer.volume = 1;


            MuteMusic = PlayerPrefs.GetInt(MUSICKEY, 0) == 1;
            MuteSFX = PlayerPrefs.GetInt(SFXKEY, 0) == 1;
        }

        #endregion

        #region Methods

        public void PlayMusic(SoundClass clip)
        {
            musicPlayer.clip = clip.Clip;
            musicPlayer.mute = MuteMusic;
            musicPlayer.volume = clip.Volume;
            musicPlayer.pitch = clip.Pitch;

            musicPlayer.loop = clip.Loop;

            musicPlayer.Play();
        }

        public void PlaySFX(SoundClass clip)
        {
            sfxPlayer.clip = clip.Clip;
            sfxPlayer.mute = MuteSFX;
            sfxPlayer.volume = clip.Volume;
            sfxPlayer.pitch = clip.Pitch;

            sfxPlayer.PlayOneShot(clip.Clip, MuteSFX ? 0 : 1);
        }

        public void Mute_SFX()
        {
            MuteSFX = !MuteSFX;
            sfxPlayer.mute = MuteSFX;
            PlayerPrefs.SetInt(SFXKEY, MuteSFX ? 1 : 0);
        }

        public void Mute_Music()
        {
            MuteMusic = !MuteMusic;
            musicPlayer.mute = MuteMusic;
            PlayerPrefs.SetInt(MUSICKEY, MuteMusic ? 1 : 0);
        }

        public void PauseSounds()
        {
            sfxPlayer.Pause();
            musicPlayer.Pause();
        }

        public void UnPauseSounds()
        {
            sfxPlayer.UnPause();
            musicPlayer.UnPause();
        }

        public void StopMusic()
        {
            musicPlayer.Stop();
        }

        public void CallEmpty()
        {
        }

        #endregion

        #region NeedeClass

        [Serializable]
        public class SoundClass
        {
            public AudioClip Clip;

            [Range(0f, 1f)] public float Volume = 1f;

            [Range(0.5f, 1.5f)] public float Pitch = 1f;

            public bool Loop;
        }

        [Serializable]
        public class MusicClass
        {
            public SoundClass menu;
            public SoundClass[] inGameMusic;
        }

        public MusicClass Music;


        [Serializable]
        public class SFXClass
        {
            public SoundClass clickSfx;
            public SoundClass helpSfx;
            public SoundClass timesUpSfx;
            public SoundClass levelCompleteSfx;
            public SoundClass foundFossilSfx;
            public SoundClass hitRockSfx;
            public SoundClass typingSfx;
            public SoundClass teleportSfx;
        }

        public SFXClass SFX;

        #endregion
    }
}