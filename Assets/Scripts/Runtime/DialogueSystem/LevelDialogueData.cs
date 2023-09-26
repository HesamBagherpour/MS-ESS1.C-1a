using System.Collections.Generic;
using UnityEngine;

namespace Runtime.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Level Dialogue Data", menuName = "Dialogue System/Level Dialogue Data")]
    public class LevelDialogueData : ScriptableObject
    {
        public List<DialogueMessage> dialogueSequence;

        public DialogueMessage GetDialogue(string id)
        {
            DialogueMessage item = dialogueSequence.Find(item => item.id.Trim() == id.Trim());

            item.claimed = true;

            Save(id);

            return item;
        }

        public List<DialogueMessage> GetClaimedSequence() => dialogueSequence.FindAll(item => item.claimed);

        public void Save(string id)
        {
            PlayerPrefs.SetInt(id, 1);
        }

        public void Load()
        {
            foreach (DialogueMessage dialogueMessage in dialogueSequence)
            {
                dialogueMessage.claimed = PlayerPrefs.HasKey(dialogueMessage.id);
            }
        }

        [ContextMenu("Reset Data ")]
        public void ResetData()
        {
            foreach (DialogueMessage dialogueMessage in dialogueSequence)
            {
                dialogueMessage.claimed = false;
            }
        }
    }
}