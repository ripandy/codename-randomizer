/**
	File	: GameManager.cs
	Author 	: Ripandy Adha (ripandy.adha@gmail.com)
 */
 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// GameManager singleton class. Manager class to handle and store global game state.
/// </summary>
[CreateAssetMenu(fileName = "GameManager", menuName = "Haunted House/GameManager", order = 0)]
public class GameManager : ScriptableObject
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
                instance = Resources.Load<GameManager>(typeof(GameManager).ToString());
            if (!instance)
            {
                var res = Resources.FindObjectsOfTypeAll<GameManager>();
                if (res.Length > 0)
                    instance = res[0];
            }
            if (!instance)
                instance = CreateInstance<GameManager>();
            return instance;
        }
    }

    public int CurrentSceneIndex { get; private set; }

    void Awake()
    {
        Debug.Log("Initializing GameManager..");
        CurrentSceneIndex = 0;
    }

    public void Initialize()
    {
        Debug.Log("Initializing connection with server..");
    }

    public void Reset()
    {
        ResetScene(true);
    }

    public void SetScene(int newSceneIndex)
    {
        if (newSceneIndex != CurrentSceneIndex && newSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(newSceneIndex);
            CurrentSceneIndex = newSceneIndex;
            Debug.Log("loading scene-" + newSceneIndex + " : " + SceneManager.GetSceneByBuildIndex(newSceneIndex).name);
        }
    }

    public void SetScene(string newSceneName)
    {
        int idx = 0;
        int len = SceneManager.sceneCountInBuildSettings;
        bool found = false;
        string name = "";

        while (!found && idx < len)
        {
            name = SceneManager.GetSceneByBuildIndex(idx).name;
            if (string.Equals(newSceneName, name))
            {
                found = true;
            }
            else
            {
                idx++;
            }
        }

        SetScene(idx);
    }

    public void ResetScene(bool quitting = false)
    {
        if (quitting)
        {
            CurrentSceneIndex = 0;
        }
        else
        {
            SetScene(0);
        }
    }
}
