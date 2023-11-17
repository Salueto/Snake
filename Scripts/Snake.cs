using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Snake : MonoBehaviour
{
    public GameObject SnakeHeadPrefab;
    public GameObject SnakeBodyPrefab;


    public float speed = 2f;
    int bodySize;

    private List<GameObject> SnakeBodys = new List<GameObject>();
    private Vector2 currentDirection = Vector2.right;
    private Vector2 lastDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        SnakeBodys = new List<GameObject>();
        bodySize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        currentDirection = transform.TransformDirection(new Vector2(horizontal, vertical)).normalized;

        if (currentDirection == Vector2.zero)
        {
            currentDirection = Vector2.zero;
        }
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        //    {
        //        // Moving horizontally
        //        if (Input.GetKeyDown(KeyCode.D))
        //        {
        //            if (lastDirection != Vector2.left)
        //            {
        //                currentDirection = Vector2.right;
        //            }
        //        }
        //        else
        //        {
        //            if (lastDirection != Vector2.right)
        //            {
        //                currentDirection = Vector2.left;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Moving vertically
        //        if (Input.GetKeyDown(KeyCode.W))
        //        {
        //            if (lastDirection != Vector2.down)
        //            {
        //                currentDirection = Vector2.up;
        //            }
        //        }
        //        else
        //        {
        //            if (lastDirection != Vector2.up)
        //            {
        //                currentDirection = Vector2.down;
        //            }

        //        }
        //    }
        //}
    }

    void Move()
    {
        if (currentDirection == Vector2.zero)
        {
            currentDirection = lastDirection;
        }


        //var rotation = 0;
        //if (currentDirection == Vector2.up)
        //{
        //    rotation = 90;
        //}
        //else if (currentDirection == Vector2.down)
        //{
        //    rotation = 270;
        //}
        //else if (currentDirection == Vector2.left)
        //{
        //    rotation = 180;
        //}
        //else if (currentDirection == Vector2.right)
        //{
        //    rotation = 0;
        //}

        // Set the rotation without affecting movement direction
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * speed * Time.deltaTime);

        //transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        //transform.Translate(currentDirection * speed * Time.deltaTime);

        //lastDirection = currentDirection;
    }

    public void AddBodySegment(Transform spawnPosition)
    {
        GameObject newSegment = Instantiate(SnakeBodyPrefab, spawnPosition.position, spawnPosition.rotation);
        bodySize++;
    }

}
