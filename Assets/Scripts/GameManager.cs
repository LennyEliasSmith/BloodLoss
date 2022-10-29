using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string MainSceneName = "Main";
    public string UISceneName = "UI";

    public void OnEnable()
    {
        if (!SceneManager.GetSceneByName(UISceneName).isLoaded)
        {
            SceneManager.LoadScene(UISceneName, LoadSceneMode.Additive);

        }


    }


}
