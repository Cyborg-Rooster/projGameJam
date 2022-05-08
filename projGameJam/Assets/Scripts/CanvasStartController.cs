using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasStartController : MonoBehaviour
{
    [SerializeField] GameObject fadeController;

    public void StartAnimFade()
    {
        fadeController.SetActive(true);
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(1);
        if (SceneManager.GetActiveScene().buildIndex == 2) SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void disableFadeOut()
    {
        gameObject.SetActive(false);
    }
}
