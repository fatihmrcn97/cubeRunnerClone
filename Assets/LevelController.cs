using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;


    public GameObject startMenu, gameMenu, gameOvertMenu, finisMenu;
    public Text currentLevelTxt, NextLevelTxt;
    private int currentLevel;
    public Slider levelProgressBar;
    public float maxDistance;
    public GameObject finishLine;

    public TextMeshProUGUI coinCOunt;
    int totalScore=1;

    private void Awake()
    { 
        if (instance==null)
            instance = this;
    }

    private void Start()
    { 
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        coinCOunt.text = PlayerPrefs.GetInt("coin").ToString();
        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            currentLevelTxt.text = (currentLevel + 1).ToString();
            NextLevelTxt.text = (currentLevel + 2).ToString();

        }
    }
    private void Update()
    {
        if (PlayerController.instance.gameCont)
        {
            PlayerController player = PlayerController.instance;
            float distance = finishLine.transform.position.x - PlayerController.instance.transform.position.x;
            levelProgressBar.value = 1 - (distance / maxDistance);
        }
    }


    public void StartLevel()
    {
        maxDistance = finishLine.transform.position.x - PlayerController.instance.transform.position.x;
        startMenu.SetActive(false);
        gameMenu.SetActive(true); 
        PlayerController.instance.SetSpeed(PlayerController.instance.runningSpeed);
        PlayerController.instance.gameCont = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOvertMenu.SetActive(true);
        PlayerController.instance.gameCont = false;
    }

    public void FinishGame()
    {
   
        Debug.Log("buraya geldimi finisg game");
        PlayerController.instance.gameCont = false;
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        coinCOunt.text = totalScore.ToString();
        gameMenu.SetActive(false);
        finisMenu.SetActive(true);
    }

    public void ChangeScore(int increment)
    {
        totalScore += increment;
        PlayerPrefs.SetInt("coin", totalScore);
        coinCOunt.text =  PlayerPrefs.GetInt("coin").ToString();
    }
}
