using Code.Core;
using UnityEngine;

namespace Code.Managers
{
    public class SoundManager : SingletonMono<SoundManager>
    {
        public AudioSource bgmSource;
        public AudioSource sfxSource;

        protected override void Initialize()
        {
            var bgmObj = new GameObject("BGM");
            bgmObj.transform.SetParent(transform, false);
            bgmSource = bgmObj.AddComponent<AudioSource>();
            bgmSource.playOnAwake = false;
            bgmSource.loop = true;

            var sfxObj = new GameObject("SFX");
            sfxObj.transform.SetParent(transform, false);
            sfxSource = sfxObj.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;

            bgmSource.volume = GameSettings.Instance.BGMVolume;
            sfxSource.volume = GameSettings.Instance.SFXVolume;
        }

        public void PlayBGM(AudioClip clip, bool loop = true)
        {
            if (clip == null)
            {
                Debug.LogWarning("尝试播放空的 BGM AudioClip！", this);
                return;
            }

            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }

        public void StopBGM()
        {
            if (bgmSource.isPlaying)
                bgmSource.Stop();
        }

        public void PauseBGM()
        {
            if (bgmSource.isPlaying)
                bgmSource.Pause();
        }

        public void ResumeBGM()
        {
            if (!bgmSource.isPlaying && bgmSource.clip != null)
                bgmSource.UnPause(); // 或直接 Play()，但 UnPause 更准确
        }

        public void SetBGMVolume(float volume)
        {
            bgmSource.volume = Mathf.Clamp01(volume);
        }

        public float GetBGMVolume() => bgmSource.volume;

        public bool IsBGMPlaying => bgmSource.isPlaying;

        public void PlaySfx(AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null)
            {
                Debug.LogWarning("尝试播放空的 SFX AudioClip！", this);
                return;
            }

            sfxSource.PlayOneShot(clip, volumeScale);
        }

        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = Mathf.Clamp01(volume);
        }

        public float GetSFXVolume() => sfxSource.volume;
    }
}