using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel = 0;
    public static float time = 0;
    public static bool isGameOver = false;
    public static string[] levels = { "Level1", "Level2", "Level3" };
    public static int numSpawners;

    public AudioClip winAudio;
    public AudioClip loseAudio;
    public Text resultText;
    public Text timerText;
    


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime;

            float normTime = ((int)(time * 100)) / 100f;

            timerText.text = normTime.ToString() + "s";

            switch (currentLevel)
            {
                case 0:
                    if (numSpawners == 0)
                    {
                        GameWon();
                    }
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
    }

    public void GameWon()
    {
        isGameOver = true;

        AudioSource.PlayClipAtPoint(winAudio, Camera.main.transform.position);

        resultText.text = "You Win!";
        resultText.gameObject.SetActive(true);
        resultText.color = Color.green;

        if (currentLevel == levels.Length - 1)
        {
            return;
        }

        Invoke("LoadNextLevel", 2);
    }

    public void GameLost()
    {
        isGameOver = true;

        AudioSource.PlayClipAtPoint(loseAudio, Camera.main.transform.position);

        resultText.text = "You Lose!";
        resultText.gameObject.SetActive(true);
        resultText.color = Color.red;

        Invoke("LoadCurrentLevel", 2);
    }

    void LoadNextLevel()
    {
        currentLevel++;
        ResetGameState();
        SceneManager.LoadScene(levels[currentLevel]);
    }

    void LoadCurrentLevel()
    {
        ResetGameState();
        SceneManager.LoadScene(levels[currentLevel]);
    }

    void ResetGameState()
    {
        isGameOver = false;
        time = 0;
        resultText.gameObject.SetActive(false);
        numSpawners = 0;
    }
}
