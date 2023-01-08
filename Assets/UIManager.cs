using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Main.Game;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI bottomText;
    public GameObject startText;

    public GameObject pauseMenu;
    public GameObject startMenu;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        bottomText.text = "";
    }


    public void Update()
    {
        if(GameConstants.gamestates == GameConstants.Gamestates.RUNNING)
        {
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
        }

        if (GameConstants.gamestates == GameConstants.Gamestates.PAUSED)
        {
            if (!Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (startText.activeInHierarchy && !startMenu.activeInHierarchy)
            {
                startText.gameObject.SetActive(false);
                GameConstants.gamestates = GameConstants.Gamestates.RUNNING;
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                GameConstants.gamestates = GameConstants.Gamestates.PAUSED;
            }
            else
            {
                pauseMenu.SetActive(false);
                GameConstants.gamestates = GameConstants.Gamestates.RUNNING;
            }
        }
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
      
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        GameConstants.gamestates = GameConstants.Gamestates.RUNNING;

    }

    public void Quit()
    {
        Application.Quit();
    }


}
