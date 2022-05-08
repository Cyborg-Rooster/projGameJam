using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour
{
    public int points;
    public bool active;

    [SerializeField] Sprite ActivedSprite;
    [SerializeField] Sprite DesactivedSprite;

    SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int GetActiveOrDesactiveAndReturnPoints()
    {
        active = !active;
        if (active) SpriteRenderer.sprite = ActivedSprite;
        else SpriteRenderer.sprite = DesactivedSprite;
        return points;
    }
}
