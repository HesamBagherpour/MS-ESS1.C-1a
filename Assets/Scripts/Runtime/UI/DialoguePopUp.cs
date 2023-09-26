using System;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runtime.DialogueSystem;
using Runtime.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class DialoguePopUp : BasePage
    {
        [Space] public Image img;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI messageText;
        public TextMeshProUGUI btnText;
        public Button nextBtn;

        private DialogueMessage dialogueMessage;
        private int messageId;

        [SerializeField] private float delayBetweenTyping = 0.02f;

        public void Show(DialogueMessage dialogueMessage, Action endAction)
        {
            messageId = 0;
            this.dialogueMessage = dialogueMessage;

            UIManager.Instance.hudPage.Pause(true);

            dialogueMessage.onEndAction += () =>
            {
                Hide();
                endAction();
            };

            Show(TypingMessage().Forget);
        }

        private async UniTaskVoid TypingMessage()
        {
            titleText.text = dialogueMessage.messages[messageId].title;
            btnText.text = dialogueMessage.messages[messageId].btnMessage;
            img.sprite = dialogueMessage.messages[messageId].spr;

            img.gameObject.SetActive(img.sprite != null);

            nextBtn.transform.localScale = Vector3.zero;

            StringBuilder msg = new StringBuilder();

            foreach (char ctx in dialogueMessage.messages[messageId].message)
            {
                msg.Append(ctx.ToString());
                messageText.text = msg.ToString();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.typingSfx);
                await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenTyping), ignoreTimeScale: false);
            }

            nextBtn.onClick.RemoveAllListeners();

            nextBtn.onClick.AddListener(() =>
            {
                messageId++;

               AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.clickSfx);

                if (messageId > dialogueMessage.messages.Length - 1)
                {
                    dialogueMessage.onEndAction?.Invoke();
                    UIManager.Instance.hudPage.Pause(false);
                    return;
                }

                TypingMessage().Forget();
            });

            nextBtn.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBounce);
        }
    }
}