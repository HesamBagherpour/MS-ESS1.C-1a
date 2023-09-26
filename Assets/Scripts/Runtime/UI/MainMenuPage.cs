using System;
using DG.Tweening;
using LoLSDK;
using Runtime.Manager;
using Runtime.UI.Extra;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class MainMenuPage : BasePage
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button continueBtn;
        [SerializeField] private CharacterManagerUI characterManager;

        private Sequence tweener;

        public override void Awake()
        {
            base.Awake();
            Show();
            startBtn.onClick.AddListener(StartGame);
            Animate();
        }

        private void Start()
        {
            Helper.StateButtonInitialize<PlayerState>(startBtn, continueBtn, OnLoad);

            characterManager.Initialize();
        }

        private void OnLoad(PlayerState loadedPlayerState)
        {
            if (loadedPlayerState == null)
            {
                Debug.Log("no data is loaded!");
                return;
            }

            continueBtn.onClick.RemoveAllListeners();
            continueBtn.onClick.AddListener(() => { LoadSceneByName(loadedPlayerState.lastSceneName); });
        }

        private void StartGame()
        {
            LoadSceneByName("Level1");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
        }

        private void LoadSceneByName(string name)
        {
            tweener.Kill();
            GameManager.Instance.LoadSceneByName(name);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);
        }

        public override void Show(Action doneAction = null)
        {
            base.Show(doneAction);

            AudioManager.Instance.PlayMusic(AudioManager.Instance.Music.menu);
            Animate();
        }

        public void Animate()
        {
            var seq = DOTween.Sequence();
            //seq.Append(startBtn.transform.DOLocalRotate(transform.forward * -30, 1).SetLoops(-1, LoopType.Yoyo));
            seq.Append(startBtn.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo));
            seq.Join(continueBtn.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo));

            tweener = seq.Play();
        }
    }
}