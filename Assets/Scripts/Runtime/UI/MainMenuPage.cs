    using System;
using System.Collections.Generic;
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
        [SerializeField] private Text newGame;
        [SerializeField] private Text Continue;
        private Sequence tweener;
        
        public List<string> keyData;

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
            // Access items in the data dictionary
            newGame.text = GameManager.Instance.loader.dataDictionary[keyData[0]];
            Continue.text = GameManager.Instance.loader.dataDictionary[keyData[1]];
        }

        
        private void OnLoad(PlayerState loadedPlayerState)
        {
            if (loadedPlayerState == null)
            {
                Debug.Log("no data is loaded!");
                continueBtn.gameObject.SetActive(false);
                continueBtn.onClick.RemoveAllListeners();
                return;
            }
            LoadSceneByName(loadedPlayerState.lastSceneName);
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
            seq.Append(startBtn.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo));
            seq.Join(continueBtn.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo));

            tweener = seq.Play();
        }
    }
}