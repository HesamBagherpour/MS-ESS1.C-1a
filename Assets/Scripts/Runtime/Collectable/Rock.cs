using System;
using UnityEngine;

namespace Runtime.Collectable
{
    public class Rock : MonoBehaviour
    {
        public Action Destroy;

        private void OnDestroy()
        {
            Destroy?.Invoke();
        }
    }
}