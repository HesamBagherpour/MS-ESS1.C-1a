using System;
using System.Collections.Generic;
using System.Text;
using Runtime.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class CataloguePopUp : BasePage
    {
        [SerializeField] private Button nextBtn;
        [SerializeField] private Button previousBtn;

        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Image fossilImg;

        public LevelDialogueData dialogueData;

        private List<DialogueMessage> _claimedFossils;
        private int _currentIndex;

        public override void Awake()
        {
            base.Awake();

            nextBtn.onClick.AddListener(NextFossil);
            previousBtn.onClick.AddListener(PreviousFossil);
            backBtn.onClick.AddListener(() => Hide());
        }

        public override void Show(Action doneAction = null)
        {
            dialogueData.Load();
            base.Show(doneAction);
            _claimedFossils = dialogueData.GetClaimedSequence();
            _currentIndex = 0;
            UIManager.Instance.hudPage.Pause(true);
            ShowFossilData(_claimedFossils[_currentIndex]);
            CheckButtons();
        }

        public override void Hide(Action doneAction = null)
        {
            base.Hide(doneAction);
            UIManager.Instance.hudPage.Pause(false);
        }

        private void ShowFossilData(DialogueMessage dialogueMessage)
        {
            nameTxt.text = dialogueMessage.messages[0].title;
            fossilImg.sprite = dialogueMessage.messages[0].spr;

            StringBuilder msg = new StringBuilder();

            foreach (Dialogues dialogues in dialogueMessage.messages)
            {
                msg.Append(dialogues.message + "\n");
            }

            messageTxt.text = msg.ToString();

            CheckButtons();
        }

        private void CheckButtons()
        {
            previousBtn.gameObject.SetActive(_currentIndex != 0);
            nextBtn.gameObject.SetActive(_currentIndex < _claimedFossils.Count - 1);
        }

        private void PreviousFossil()
        {
            _currentIndex--;
            CheckButtons();
            ShowFossilData(_claimedFossils[_currentIndex]);
        }

        private void NextFossil()
        {
            _currentIndex++;
            CheckButtons();
            ShowFossilData(_claimedFossils[_currentIndex]);
        }
    }
}