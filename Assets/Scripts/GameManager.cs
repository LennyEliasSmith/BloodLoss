using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Main.Game;

public class GameManager : MonoBehaviour
{
    public string MainSceneName = "Main";
    public string UISceneName = "UIScene";

    public GameObject player;
    public Transform[] spawnPoints;

    //public GameConstants.Gamestates gamestates;

    public UIManager uIManager;


    public void OnEnable()
    {
        if (!SceneManager.GetSceneByName(UISceneName).isLoaded)
        {
            SceneManager.LoadScene(UISceneName, LoadSceneMode.Additive);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Application.Quit();
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName(UISceneName).isLoaded)
        {
            uIManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        }
        GameConstants.gamestates = GameConstants.Gamestates.PAUSED;
    }
}
