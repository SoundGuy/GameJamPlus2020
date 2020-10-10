using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MovementDirection
{
    up,
    down,
    left,
    right,
    none
}
public class MovementByGrid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);


        //for movement by input
        /* if(Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
         {
             if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
             {
                 movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*2, 0f, 0f);
             }

             if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
             {
                 movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
             }
         }*/
    }


    public void MoveCharacter(MovementDirection playerDirection)
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= 1f)
        {
            switch (playerDirection)
            {
                case MovementDirection.up:
                    movePoint.position += new Vector3(0f, 1, 0f);
                    break;
                case MovementDirection.down:
                    movePoint.position += new Vector3(0f, -1, 0f);
                    break;
                case MovementDirection.left:
                    movePoint.position += new Vector3(-1f, 0, 0f);
                    break;
                case MovementDirection.right:
                    movePoint.position += new Vector3(1f, 0, 0f);
                    break;
                case MovementDirection.none:
                    movePoint.position += new Vector3(1f, 0, 0f);
                    break;
                default:
                    break;
            }
        }
    }
}
