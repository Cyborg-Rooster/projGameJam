using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed, MoveLimiter;
    public bool PlayerOne;

    float Horizontal, Vertical;
    int WalkForce = 1;

    Rigidbody2D Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerOne)
        {
            Horizontal = Input.GetAxisRaw("PlayerOneHorizontal");
            Vertical = Input.GetAxisRaw("PlayerOneVertical");
            if (Input.GetKeyDown(KeyCode.LeftShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.LeftShift)) WalkForce--;
        }
        else
        {
            Horizontal = Input.GetAxisRaw("PlayerTwoHorizontal");
            Vertical = Input.GetAxisRaw("PlayerTwoVertical");
            if (Input.GetKeyDown(KeyCode.RightShift)) WalkForce++;
            if (Input.GetKeyUp(KeyCode.RightShift)) WalkForce--;
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
}
