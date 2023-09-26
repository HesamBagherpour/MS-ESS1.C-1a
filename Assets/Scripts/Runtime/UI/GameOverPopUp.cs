using Runtime.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GameOverPopUp : BasePage
    {
        [SerializeField] private Button retryBtn;
        [SerializeField] private Button mainMenuBtn;

        public override void Awake()
        {
            base.Awake();
            retryBtn.onClick.AddListener(RetryLevel);
            mainMenuBtn.onClick.AddListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            GameManager.Instance.LoadSceneByName("MainMenu");
        }

        private void RetryLevel()
        {
            GameManager.Instance.LoadSceneByName(SceneManager.GetActiveScene().name);
        }
    }
}