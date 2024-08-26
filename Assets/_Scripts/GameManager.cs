using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI livesText;
 
    public int lives;
    public bool isWin;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        isWin = false;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        livesText.text = lives.ToString();
    }

    void Update()
    {
        livesText.text = lives.ToString();

        if (lives <= 0)
        {
            Time.timeScale = 0;
            loseScreen.SetActive(true);            
        }

        if(isWin == true)
        {
            winScreen.SetActive(true);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
