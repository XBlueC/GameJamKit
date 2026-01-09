using Code.Core;
using UnityEngine;

namespace Code.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : SingletonMono<SoundManager>
    {
        public AudioSource bgmSource;
        public AudioSource sfxSource;

        protected override void Initialize()
        {
            bgmSource = new GameObject("BGM").AddComponent<AudioSource>();
            bgmSource.transform.SetParent(transform);
            sfxSource = new GameObject("SFX").AddComponent<AudioSource>();
            sfxSource.transform.SetParent(transform);
        }

        public void PlayBGM(AudioClip clip, bool loop = true)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}