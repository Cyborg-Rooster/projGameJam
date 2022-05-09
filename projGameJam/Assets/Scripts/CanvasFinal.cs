using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFinal : MonoBehaviour
{
    public Text parabensText;
    public Text p1PointsText;
    public Text p2PointsText;

    public void setTexts(bool p1Win, int p1Points, int p2Points)
    {
        if (p1Win) parabensText.text = "Parabens Jogador 1!";
        else parabensText.text = "Parabens Jogador 2!";

        p1PointsText.text = $"Pontos jogador 1 : {p1Points}";
        p2PointsText.text = $"Pontos jogador 2 : {p2Points}";
    }
}
