using UnityEngine;
using UnityEngine.SceneManagement;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class SplashScene : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 24;
            Input.backButtonLeavesApp = true;
        }

        private void Start()
        {
            SceneManager.LoadScene("RandomizerScene");
        }
    }
}
