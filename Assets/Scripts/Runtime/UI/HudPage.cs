using System;
using DG.Tweening;
using Runtime.Manager;
using Runtime.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Emaj.Fossil.UI
{
    public class HudPage : BasePage
    {
        [SerializeField] private Button helpBtn;
        [SerializeField] private Button pauseBtn;
        [SerializeField] private Button backpackBtn;
        [SerializeField] private TextMeshProUGUI timerTxt;
        [SerializeField] private TextMeshProUGUI fossilCountTxt;
        [SerializeField] private TextMeshProUGUI getFossilCountTxt;

        private Tweener tweener;


        public override void Awake()
        {
            base.Awake();
            pauseBtn.onClick.AddListener(ShowPause);
            helpBtn.onClick.AddListener(ShowHelp);
            backpackBtn.onClick.AddListener(ShowClaimedPopUp);
        }

        private void ShowClaimedPopUp()
        {
            UIManager.Instance.cataloguePopUp.Show();
        }

        private void ShowHelp()
        {
            UIManager.Instance.helpPopUp.Show();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.helpSfx);
        }

        private void ShowPause()
        {
            UIManager.Instance.pausePopUp.Show();
            Pause(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
        }

        public void StartTimer(float time)
        {
            Debug.Log("START TIMER");

            tweener?.Kill();

            tweener = DOTween.To(() => time, x => time = x, 0, time)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    int minutes = (int) time / 60;
                    int seconds = (int) time % 60;
                    timerTxt.text = $"{minutes:00}:{seconds:00}";
                })
                .OnComplete(() => { GameManager.Instance.GameOver(); });
        }

        public void Pause(bool pause)
        {
            if (pause)
                tweener.Pause();
            else
                tweener.Play();
        }

        public void SetFossil(int fossilCounter)
        {
            fossilCountTxt.text = fossilCounter.ToString();
        }

        public void GetFossil(int fossilCounter)
        {
            getFossilCountTxt.text = fossilCounter.ToString();
        }
    }
}