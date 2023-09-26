using System;
using Runtime.Manager;
using Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Emaj.Fossil.UI
{
    public class PausePopUp : BasePage
    {
        [SerializeField] private Button resumeBtn;
        [SerializeField] private Button resetBtn;
        [Space(4)] [SerializeField] private Button musicBtn;
        [SerializeField] private Sprite musicOnSpr, musicOffSpr;
        [SerializeField] private Image musicImg;
        [Space(4)] [SerializeField] private Button sfxBtn;
        [SerializeField] private Image sfxImg;
        [SerializeField] private Sprite sfxOnSpr, sfxOffSpr;

        private void Start()
        {
            resumeBtn.onClick.AddListener(ResumeGame);
            resetBtn.onClick.AddListener(ResetLevel);
            musicBtn.onClick.AddListener(Music);
            sfxBtn.onClick.AddListener(SFX);
        }

        public override void Show(Action doneAction = null)
        {
            base.Show(doneAction);
            Application.runInBackground = false;
            musicImg.sprite = AudioManager.Instance.MuteSFX ? musicOffSpr : musicOnSpr;
        }

        public override void Hide(Action doneAction = null)
        {
            base.Hide(doneAction);
            Application.runInBackground = true;
        }

        private void ResetLevel()
        {
            //TODO RESET LEVEL
        }

        private void ResumeGame()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
            Hide(() => UIManager.Instance.hudPage.Pause(false));
        }

        private void Music()
        {
            AudioManager.Instance.Mute_Music();
            musicImg.sprite = AudioManager.Instance.MuteMusic ? musicOffSpr : musicOnSpr;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
        }

        private void SFX()
        {
            AudioManager.Instance.Mute_SFX();
            sfxImg.sprite = AudioManager.Instance.MuteSFX ? sfxOffSpr : sfxOnSpr;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
        }
    }
}