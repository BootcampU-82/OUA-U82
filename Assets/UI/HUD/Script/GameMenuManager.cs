using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject pausePanelBlur;

    public bool spiderCanAttack = false;


    [SerializeField] GameObject hintUI;
    bool hintOpen=false;
    [SerializeField] float hintSpeed;
    [SerializeField] float hintScale;

    public bool isPuzzleComplete;
    
    public bool isPaused = false;


    private static GameMenuManager instance;

    public static GameMenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMenuManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<GameMenuManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (Input.GetKey(KeyCode.Tab))
        {

            Debug.Log("Pressed.");

            if (hintOpen)
            {
                Debug.Log("open" + hintOpen);
                hintUI.transform.DOScale(hintScale, hintSpeed);
                hintUI.transform.gameObject.SetActive(false);
                hintOpen = false;
            }
            else
            {
                Debug.Log("closed" + hintOpen);
                hintUI.transform.gameObject.SetActive(true);
                hintUI.transform.DOScale(1.75f, hintSpeed);
                hintOpen = true;
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        pausePanelBlur.SetActive(true);
        pausePanel.SetActive(true);

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        pausePanelBlur.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    
}
