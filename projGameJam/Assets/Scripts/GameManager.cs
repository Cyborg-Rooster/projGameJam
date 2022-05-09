using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class GameManager : MonoBehaviour
{ 
    [SerializeField] Text TextPlayerOneScore;
    [SerializeField] Text TextPlayerTwoScore;
    [SerializeField] GameObject TextTime;

    [SerializeField] FinishGame finishGame;

    public bool gameStarted = false;

    public int minutes;
    public float seconds;

    private void Start()
    {
        gameStarted = true;
    }

    public void AddPoint(bool playerOne, int point)
    {
        if (playerOne) TextManager.SetText(TextPlayerOneScore, point);
        else TextManager.SetText(TextPlayerTwoScore, point);
    }

    private void Update()
    {
        if(gameStarted)
        {
            if (minutes > 0 || seconds > 0)
            {
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                }
                seconds -= Time.deltaTime;
            }
            else gameStarted = false;
            TextManager.SetText(TextTime, $"{minutes}:{seconds:00}");
        }

        if (!gameStarted) finishGame.changeScene();
    }
}
