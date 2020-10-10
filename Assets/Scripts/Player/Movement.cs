using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up,
    down,
    left,
    right,
    none
}

public class Movement : MonoBehaviour
{
    //Movement Parameters
    private float xInput, yInput;
    private Rigidbody2D theRB;
    [SerializeField] private float movementSpeed = 5f;
    private Direction playerDirection = Direction.up;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }


    private void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);

        theRB.MovePosition(theRB.position + move);
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (yInput != 0 && xInput != 0)
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (yInput != 0 || xInput != 0)
        {
            if (xInput < 0)
            {
                playerDirection = Direction.left;
            }
            else if (xInput > 0)
            {
                playerDirection = Direction.right;
            }
            else if (yInput < 0)
            {
                playerDirection = Direction.down;
            }
            else if (yInput > 0)
            {
                playerDirection = Direction.up;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            //save for idle animation
        }
    }
}
