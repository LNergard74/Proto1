using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject howToPlay;
    private bool howToPlayToggle = false;
    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HowToPlayToggle()
    {
        howToPlayToggle = !howToPlayToggle;
        howToPlay.SetActive(howToPlayToggle);
    }
}
