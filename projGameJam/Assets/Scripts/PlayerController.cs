using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed, MoveLimiter;
    public bool PlayerOne;

    public float Horizontal, Vertical;
    int WalkForce = 1;

    Rigidbody2D Rigidbody;
    TriggerManager TriggerManager = new TriggerManager();

    RaycastHit2D ObjectInteractive;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
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
    }
}
