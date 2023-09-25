using System.Collections.Generic;
using Cinemachine;
using Runtime.CommonCharacterAssets;
using UnityEngine;

namespace Runtime.Manager
{
    public class CharacterManager : MonoBehaviour
    {
        public List<Character> characters;
        public CinemachineVirtualCamera cinemachineVirtualCamera;
        public LevelManager LevelManager;

        public int characterSpeed = 4;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            
            var player = Instantiate(characters[0], gameObject.transform);
            player.characterSpeed = characterSpeed;
            cinemachineVirtualCamera.Follow = player.transform.GetChild(0);
            LevelManager.player = player.transform;
        }
        
    }
}