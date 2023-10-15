using LoLSDK;
using Runtime.LevelGenerate;
using Runtime.Singleton;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Manager
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        public int LevelTime;
        private int levelNumebr;
        public GameObject portal;
        public Transform player;

        public int  fossilCount;
        private int fossilCounter;

        private void Start()
        {
            LevelGenerator[] levelGenerators = gameObject.GetComponentsInChildren<LevelGenerator>();
            foreach (var item in levelGenerators)
            {
                item.levelFinish += LevelFinished;
            }

            levelNumebr = levelGenerators.Length;
            GameManager.Instance.SetLevelData(LevelTime);
            fossilCounter = 0;
            UIManager.Instance.hudPage.SetFossil(fossilCount);
            UIManager.Instance.hudPage.GetFossil(fossilCounter);
        }

        private void LevelFinished()
        {
            levelNumebr--;
            if (levelNumebr != 0) return;
            GameObject gameObject = Instantiate(portal, player.transform.position + player.transform.forward * 3,
                Quaternion.identity);

            float angle = Vector3.Angle(player.transform.position, gameObject.transform.position);
            gameObject.transform.localScale = new Vector3(3, 0, 3);
            gameObject.transform.rotation = Quaternion.Euler(90, angle, 0);
        }

        public void GetFossil()
        {
            fossilCounter++; 
            GameManager.Instance.playerState.maxProgress = fossilCounter;
            //GameManager.Instance.Save();
            LOLSDK.Instance.SaveState(GameManager.Instance.playerState);
            UIManager.Instance.hudPage.GetFossil(fossilCounter);
        }
    }
}