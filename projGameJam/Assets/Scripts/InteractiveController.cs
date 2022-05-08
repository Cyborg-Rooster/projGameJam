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
    BoxCollider2D BoxCollider2D;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    public int GetActiveOrDesactiveAndReturnPoints()
    {
        active = !active;
        if (active) SpriteRenderer.sprite = ActivedSprite;
        else SpriteRenderer.sprite = DesactivedSprite;
        return points;
    }

    public void SetActive()
    {
        active = !active;
        if (active) SpriteRenderer.sprite = ActivedSprite;
        else SpriteRenderer.sprite = DesactivedSprite;
        BoxCollider2D.enabled = active;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Transform>(out Transform player))
        {
            if (player.position.y < transform.position.y) SpriteRenderer.sortingOrder = -1;
            else SpriteRenderer.sortingOrder = 3;
        }
    }
}

