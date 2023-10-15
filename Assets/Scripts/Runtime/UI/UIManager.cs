using System;
using Emaj.Fossil.UI;
using Runtime.DialogueSystem;
using Runtime.Manager;
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

            foreach (var dialogueMessage in dialogue.messages)
            {
                dialogueMessage.message = GameManager.Instance.loader.dataDictionary[dialogueMessage.title];
            }
            
            
            dialoguePopUp.Show(dialogue, () => { endAction?.Invoke(); });
        }
    }
}