using System.Collections;
using UnityEngine;

public class SplashScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initialization());
    }

    IEnumerator Initialization()
    {
        GameManager.Instance.Initialize();

        yield return new WaitForSeconds(3f);

        GameManager.Instance.SetScene("RandomizerScene");
    }
}
