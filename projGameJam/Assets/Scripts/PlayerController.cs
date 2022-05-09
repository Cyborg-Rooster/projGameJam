using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed, MoveLimiter;
    public bool PlayerOne;
    public int Points;

    public float Horizontal, Vertical;
    int WalkForce = 1;

    Rigidbody2D Rigidbody;
    Animator Animator;
    SpriteRenderer SpriteRenderer;
    TriggerManager TriggerManager = new TriggerManager();

    [SerializeField] GameManager GameManager;

    RaycastHit2D ObjectInteractive;

    public Tilemap currentTilemap;
    Vector3Int posiInt = new Vector3Int();
    bool tile;
    public GameObject effects;

    GameObject painelOne;
    GameObject painelTwo;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        painelOne = GameObject.Find("PanelOne");
        painelTwo = GameObject.Find("PanelTwo");
        painelOne.SetActive(true);
        painelTwo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ObjectInteractive = TriggerManager.CheckIfExistCollider(transform, Horizontal, Vertical, "Interactive");
        Debug.Log(ObjectInteractive.collider == null);
        if (PlayerOne)
        {
            Horizontal = Input.GetAxisRaw("PlayerOneHorizontal");
            Vertical = Input.GetAxisRaw("PlayerOneVertical");

            if (Input.GetKeyDown(KeyCode.LeftShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.LeftShift)) WalkForce--;

            if (ObjectInteractive)
                if (Input.GetKeyDown(KeyCode.V)) AddPoint();

            if (painelOne.activeSelf)
                if (Input.GetKeyDown(KeyCode.G))
                    painelOne.SetActive(false);
        }
        else
        {
            Horizontal = Input.GetAxisRaw("PlayerTwoHorizontal");
            Vertical = Input.GetAxisRaw("PlayerTwoVertical");
            if (Input.GetKeyDown(KeyCode.RightShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.RightShift)) WalkForce--;

            if (ObjectInteractive)
                if (Input.GetKeyDown(KeyCode.L)) AddPoint();

            if (painelTwo.activeSelf)
                if (Input.GetKeyDown(KeyCode.P))
                    painelTwo.SetActive(false);
        }

        if (CheckIfMoveButtonWasPressed()) SetAnimation();
        else if (!CheckIfMoveButtonWasPressed() && Horizontal == 0 && Vertical == 0) Animator.SetBool("IsWalking", false);

        posiInt = currentTilemap.WorldToCell(transform.position);
        Color cor = currentTilemap.GetColor(posiInt);
        tile = currentTilemap.HasTile(posiInt);

        if (tile) 
        {
            effects.SetActive(false);
            cor.a = 0.3f;
            currentTilemap.color = cor;
        }
        else
        {
            effects.SetActive(true);
            cor.a = 1f;
            currentTilemap.color = cor;
        }
    }

    void FixedUpdate()
    {
        if (Horizontal != 0 && Vertical != 0)
        {
            Horizontal *= MoveLimiter;
            Vertical *= MoveLimiter;
        }

        Rigidbody.velocity = new Vector2
        (
            Horizontal * MoveSpeed * WalkForce,
            Vertical * MoveSpeed * WalkForce
        );
    }

    private void AddPoint()
    {
        if (ObjectInteractive.collider.tag == "Interactive" && PlayerOne && !ObjectInteractive.collider.GetComponent<InteractiveController>().active)
        {
            Points += ObjectInteractive.collider.GetComponent<InteractiveController>().GetActiveOrDesactiveAndReturnPoints();
            GameManager.AddPoint(PlayerOne, Points);
        }
        else if (ObjectInteractive.collider.tag == "Door") ObjectInteractive.collider.GetComponent<InteractiveController>().SetActive();

        if (ObjectInteractive.collider.tag == "Interactive" && !PlayerOne && ObjectInteractive.collider.GetComponent<InteractiveController>().active)
        {
            Points += ObjectInteractive.collider.GetComponent<InteractiveController>().GetActiveOrDesactiveAndReturnPoints();
            GameManager.AddPoint(PlayerOne, Points);
        }
        else if (ObjectInteractive.collider.tag == "Door") ObjectInteractive.collider.GetComponent<InteractiveController>().SetActive();

    }

    private void SetAnimation()
    {
        if (Horizontal < 0) SpriteRenderer.flipX = true;
        else if (Horizontal > 0) SpriteRenderer.flipX = false;

        Animator.SetBool("IsWalking", true);

        Animator.SetFloat("PosX", Horizontal);
        Animator.SetFloat("PosY", Vertical);

        /*if (TriggerManager.Down) Animator.SetTrigger("Down");
        else if (TriggerManager.DownLeft || TriggerManager.RightDown) Animator.SetTrigger("DownRight");
        else if (TriggerManager.Left || TriggerManager.Right) Animator.SetTrigger("Right");
        else if (TriggerManager.UpRight || TriggerManager.LeftUp) Animator.SetTrigger("UpRight");
        else if (TriggerManager.Up) Animator.SetTrigger("Up");*/
    }

    private bool CheckIfMoveButtonWasPressed()
    {
        if (PlayerOne) return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
              Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
        else return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
              Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
    }
}
