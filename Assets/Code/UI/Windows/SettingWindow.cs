using Code.Managers;
using Code.Scene;
using Code.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class SettingWindow : UIBase
    {
        public Button exitBtn;
        public Button closeBtn;
        public Button resetBtn;

        public Slider bgmSlider;
        public Slider sfxSlider;

        public TextMeshProUGUI bgmValueText;
        public TextMeshProUGUI sfxValueText;

        public override void OnInit()
        {
            exitBtn.onClick.AddListener(() => { SceneFlow.Instance.StartLoadScene(SceneType.Home); });
            closeBtn.onClick.AddListener(Close);
            resetBtn.onClick.AddListener(OnResetSettingsClicked);

            bgmSlider.minValue = 0f;
            bgmSlider.maxValue = 1f;
            bgmSlider.value = GameSettings.Instance.BGMVolume;
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);

            sfxSlider.minValue = 0f;
            sfxSlider.maxValue = 1f;
            sfxSlider.value = GameSettings.Instance.SFXVolume;
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

            UpdateVolumeTexts();
        }

        private void OnBGMVolumeChanged(float value)
        {
            GameSettings.Instance.BGMVolume = value;
            UpdateBGMText();
        }

        private void OnSFXVolumeChanged(float value)
        {
            GameSettings.Instance.SFXVolume = value;
            UpdateSFXText();
        }

        private void UpdateVolumeTexts()
        {
            UpdateBGMText();
            UpdateSFXText();
        }

        private void UpdateBGMText()
        {
            if (bgmValueText != null)
                bgmValueText.text = Mathf.RoundToInt(GameSettings.Instance.BGMVolume * 100) + "%";
        }

        private void UpdateSFXText()
        {
            if (sfxValueText != null)
                sfxValueText.text = Mathf.RoundToInt(GameSettings.Instance.SFXVolume * 100) + "%";
        }

        public void OnResetSettingsClicked()
        {
            GameSettings.Instance.ResetToDefaults();
            bgmSlider?.SetValueWithoutNotify(GameSettings.Instance.BGMVolume);
            sfxSlider?.SetValueWithoutNotify(GameSettings.Instance.SFXVolume);

            UpdateVolumeTexts();
        }
    }
}