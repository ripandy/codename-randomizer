using UnityEngine;
using UnityEngine.SceneManagement;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class SplashScene : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 15;
            Input.backButtonLeavesApp = true;
        }

        private void Start()
        {
            SceneManager.LoadScene("RandomizerScene");
        }
    }
}
