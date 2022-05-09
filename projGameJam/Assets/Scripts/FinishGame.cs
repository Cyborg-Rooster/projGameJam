using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerController player1Controller;
    [SerializeField] PlayerController player2Controller;

    GameObject canvasFinal;
    CanvasFinal canvasFinalScript;

    int p1Points;
    int p2Points;
    bool player1Win;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            canvasFinal = GameObject.Find("FinalInterface");
            canvasFinalScript = canvasFinal.GetComponent<CanvasFinal>();

            if (p1Points > p2Points) player1Win = true;
            else player1Win = false;

            canvasFinalScript.setTexts(player1Win, p1Points, p2Points);
        }      
    }

    public void changeScene()
    {
        p1Points = player1Controller.Points;
        p2Points = player2Controller.Points;
        SceneManager.LoadScene(2);
    }
}
