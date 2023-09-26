using System;
using System.Collections.Generic;
using Runtime.Collectable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        public List<Transform> pos;

        public List<GameObject> prefab;

        private int _numberRock;

        public Action levelFinish;

        private void Start()
        {
            _numberRock = 0;
            foreach (var item in prefab)
            {
                int number = Random.Range(0, pos.Count);
                RockItem rockItem = Instantiate(item, pos[number].position, Quaternion.identity, transform)
                    .GetComponent<RockItem>();
                rockItem.RockCollectabled += RockCollectabled;
                _numberRock++;
                pos.RemoveAt(number);
            }
        }

        private void RockCollectabled()
        {
            _numberRock--;
            if (_numberRock != 0) return;
            levelFinish?.Invoke();
            Debug.Log("_numberRock : " + _numberRock);
        }
    }
}