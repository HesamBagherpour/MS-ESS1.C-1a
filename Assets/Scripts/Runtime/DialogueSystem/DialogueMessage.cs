using System;
using UnityEngine;

namespace Runtime.DialogueSystem
{
    [Serializable]
    public class DialogueMessage
    {
        public string id;
        public Dialogues[] messages;
        [NonSerialized] public Action onEndAction;
        public bool claimed;
    }

    [Serializable]
    public class Dialogues
    {
        public string title;
        [TextArea] public string message;
        public Sprite spr;
        public string btnMessage;
    }
}