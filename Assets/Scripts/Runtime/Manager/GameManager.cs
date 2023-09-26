using LoLSDK;
using Runtime.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerState playerState;

        private void Start()
        {
            DontDestroyOnLoad(this);
            Debug.Log("debug 2");
        }

        public void SetLevelData(float time)
        {
            UIManager.Instance.hudPage.Show(() =>
            {
                UIManager.Instance.hudPage.StartTimer(time);
                AudioManager.Instance.PlayMusic(AudioManager.Instance.Music.inGameMusic[Random.Range(0, AudioManager.Instance.Music.inGameMusic.Length)]);
            });
        }

        private int SceneIndex(string sceneName)
        {
            return sceneName switch
            {
                "Level1" => 0,
                "Level2" => 1,
                "Level3" => 2,
                _ => 3
            };
        }

        public void GameOver()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.timesUpSfx);
            UIManager.Instance.gameOverPopUp.Show();
        }

        public void Save()
        {
            LOLSDK.Instance.SaveState(playerState);
        }

        public void GameComplete()
        {
            LOLSDK.Instance.CompleteGame();
        }

        public void LoadSceneByName(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }

    public class PlayerState
    {
        public string lastSceneName;
    }
}