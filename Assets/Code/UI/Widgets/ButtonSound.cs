using Code.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Widgets
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour
    {
        public AudioClip audioClip;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PlayAudio);
        }

        private void PlayAudio()
        {
            SoundManager.Instance.PlaySfx(audioClip);
        }
    }
}