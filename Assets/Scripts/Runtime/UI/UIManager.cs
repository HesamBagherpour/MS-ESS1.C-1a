using System;
using Emaj.Fossil.UI;
using Runtime.DialogueSystem;
using Runtime.Singleton;
using UnityEngine;

namespace Runtime.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        public HudPage hudPage;
        public PausePopUp pausePopUp;
        public GameOverPopUp gameOverPopUp;
        public DialoguePopUp dialoguePopUp;
        public CataloguePopUp cataloguePopUp;
        public HelpPopUp helpPopUp;


        public LevelDialogueData dialogueData;


        public void ShowDialogue(string id, Action endAction)
        {
            DialogueMessage dialogue = dialogueData.GetDialogue(id);

            if (dialogue == null)
            {
                Debug.Log($"Dialogue with id = {id} not found");
                return;
            }

            dialoguePopUp.Show(dialogue, () => { endAction?.Invoke(); });
        }
    }
}