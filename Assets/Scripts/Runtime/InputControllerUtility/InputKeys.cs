using System;
using System.Collections.Generic;

namespace Runtime.InputControllerUtility
{
    [Serializable]
    public class InputKeys
    {
        public string actionName;
        public List<string> keys;

        public InputKeys(string actionName, List<string> keys)
        {
            this.actionName = actionName;
            this.keys = keys;
        }
    }
}