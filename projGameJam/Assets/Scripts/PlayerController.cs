using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectInteractive = TriggerManager.CheckIfExistCollider(transform, Horizontal, Vertical, "Interactive");
        if (PlayerOne)
        {
            Horizontal = Input.GetAxisRaw("PlayerOneHorizontal");
            Vertical = Input.GetAxisRaw("PlayerOneVertical");

            if (Input.GetKeyDown(KeyCode.LeftShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.LeftShift)) WalkForce--;

            if (ObjectInteractive)
                if (Input.GetKeyDown(KeyCode.K)) AddPoint();
        }
        else
        {
            Horizontal = Input.GetAxisRaw("PlayerTwoHorizontal");
            Vertical = Input.GetAxisRaw("PlayerTwoVertical");
            if (Input.GetKeyDown(KeyCode.RightShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.RightShift)) WalkForce--;

            if (ObjectInteractive)
                if (Input.GetKeyDown(KeyCode.KeypadPeriod)) AddPoint();
        }

        if (CheckIfMoveButtonWasPressed()) SetAnimation();
        else if (!CheckIfMoveButtonWasPressed() && Horizontal == 0 && Vertical == 0) Animator.SetBool("IsWalking", false);
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
        if (PlayerOne) 
        {
            ObjectInteractive.collider.GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Player one interacted with object."); 
        }
        else 
        {
            ObjectInteractive.collider.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("Player two interacted with object.");  
        }

        Points += ObjectInteractive.collider.GetComponent<InteractiveController>().points;
        GameManager.AddPoint(PlayerOne, Points);

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
