using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Main.Game;

public class UIManager : MonoBehaviour
{
    AudioManager audioManager;
    public GameManager gameManager;

    public TextMeshProUGUI bottomText;

    public GameObject startText;
    public CanvasGroup startTextGroup;

    public GameObject pauseMenu;

    public CanvasGroup mainMenuGroup;
    public GameObject mainMenu;

    public GameObject endText;
    public CanvasGroup endTextGroup;
    public bool isEnding;

    public CanvasGroup fadeImage;
    public float fadeSpeed;
    public IEnumerator fadeIn;
    public IEnumerator fadeOut;
    public IEnumerator endTextEnumerator;
    // Start is called before the first frame update
    void OnEnable()
    {
        bottomText.text = "";
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetTrack(audioManager._whiteNoise);

    }

    private void Start()
    {
        fadeIn = FadeInImage();
        fadeOut = FadeOutImage();
        endTextEnumerator = EndText();
        fadeImage.alpha = 0;
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
            if (startText.activeInHierarchy && !mainMenu.activeInHierarchy)
            {
                StartCoroutine(StartMethod(startTextGroup, startText));
                GameConstants.gamestates = GameConstants.Gamestates.RUNNING;
            }

            if(endText.activeInHierarchy && !mainMenu.activeInHierarchy)
            {
                StartCoroutine(StartMethod(endTextGroup, endText));
                StartCoroutine(FadeInImage());
                if(fadeImage.alpha >= 0)
                {
                    mainMenu.SetActive(true);
                    mainMenuGroup.alpha = 1;
                    StartCoroutine(FadeOutImage());
                    GameConstants.gamestates = GameConstants.Gamestates.PAUSED;
                }
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

    public void MainMenu()
    {

    }

    public void StartGame()
    {
        audioManager.audioSource.clip = audioManager._ambienceTrack;
        audioManager.audioSource.Play();

        StartCoroutine(StartMethod(mainMenuGroup, mainMenu));

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

    IEnumerator FadeInImage()
    {
       
       while(fadeImage.alpha < 1)
        {
            fadeImage.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator FadeOutImage()
    {
        while (fadeImage.alpha > 0)
        {
            fadeImage.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator StartMethod(CanvasGroup group, GameObject uiObject)
    {
        while (group.alpha > 0)
        {
            group.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        uiObject.SetActive(false);
    }
    IEnumerator EndMethod(CanvasGroup group, GameObject uiObject)
    {
       
        uiObject.SetActive(true);
        while (group.alpha < 0)
        {
            group.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        
    }

    IEnumerator EndText()
    {
        isEnding = true;
        endText.SetActive(true);
        while (endTextGroup.alpha < 1)
        {
            endTextGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
