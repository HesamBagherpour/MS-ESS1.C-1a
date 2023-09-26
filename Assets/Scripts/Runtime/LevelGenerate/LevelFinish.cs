using Runtime.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.LevelGenerate
{
    public class LevelFinish : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.teleportSfx);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}