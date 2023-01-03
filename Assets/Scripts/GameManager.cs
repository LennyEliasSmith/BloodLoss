using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string MainSceneName = "Main";
    public string UISceneName = "UI";

    public GameObject player;
    public Transform[] spawnPoints;

    public UIManager uIManager;

    public enum Gamestates
    {
        START,
        PAUSED,
        HUNTED,
        END
    }
    public enum PlayerStates
    {
        BLEEDING,
        NORMAL
    }
    public enum StageStates
    {
        STATIC,
        CHANGING
    }

    public Gamestates gamestates;
    public PlayerStates playerStates;
    public StageStates stageStates;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void Respawn()
    {

    }
    

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName(UISceneName).isLoaded)
        {
            uIManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        }
        
    }
}
