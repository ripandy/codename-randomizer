using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 24;
    }

    private void Start()
    {
        SceneManager.LoadScene("RandomizerScene");
    }
}
