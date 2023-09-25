using System;
using UnityEngine;

namespace Emaj.Fossil.Collectable
{
    [Serializable]
    public abstract class CollectableBase : MonoBehaviour
    {
         public abstract string Id { get; set; }
        public abstract void Collectable();
    }
}