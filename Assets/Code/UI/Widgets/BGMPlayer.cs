using Code.Managers;
using UnityEngine;

namespace Code.UI.Widgets
{
    public class BGMPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip bgmClip;
        [SerializeField] private bool loop = true;

        private void Start()
        {
            if (bgmClip != null)
            {
                SoundManager.Instance.PlayBGM(bgmClip, loop);
            }
            else
            {
                Debug.LogWarning("BGMPlayer: 未指定背景音乐 AudioClip！", this);
            }
        }
    }
}