using Code.Core;
using UnityEngine;

namespace Code.Managers
{
    /// <summary>
    /// 游戏全局设置，负责读写 PlayerPrefs 并提供默认值。
    /// </summary>
    public class GameSettings : Singleton<GameSettings>
    {
        private const string KEY_BGM_VOLUME = "GameSettings_BGMVolume";
        private const string KEY_SFX_VOLUME = "GameSettings_SFXVolume";

        // 默认音量值
        private const float DEFAULT_BGM_VOLUME = 0.7f;
        private const float DEFAULT_SFX_VOLUME = 0.8f;

        // 私有缓存，避免频繁读取 PlayerPrefs
        private float _bgmVolume = -1f; // -1 表示未初始化
        private float _sfxVolume = -1f;

        /// <summary>
        /// 背景音乐音量
        /// </summary>
        public float BGMVolume
        {
            get
            {
                if (_bgmVolume < 0)
                    _bgmVolume = PlayerPrefs.GetFloat(KEY_BGM_VOLUME, DEFAULT_BGM_VOLUME);
                return _bgmVolume;
            }
            set
            {
                _bgmVolume = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat(KEY_BGM_VOLUME, _bgmVolume);
                PlayerPrefs.Save();

                SoundManager.Instance.SetBGMVolume(_bgmVolume);
            }
        }

        public float SFXVolume
        {
            get
            {
                if (_sfxVolume < 0)
                    _sfxVolume = PlayerPrefs.GetFloat(KEY_SFX_VOLUME, DEFAULT_SFX_VOLUME);
                return _sfxVolume;
            }
            set
            {
                _sfxVolume = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat(KEY_SFX_VOLUME, _sfxVolume);
                PlayerPrefs.Save();

                SoundManager.Instance.SetSFXVolume(_sfxVolume);
            }
        }

        public void ResetToDefaults()
        {
            BGMVolume = DEFAULT_BGM_VOLUME;
            SFXVolume = DEFAULT_SFX_VOLUME;
        }
    }
}