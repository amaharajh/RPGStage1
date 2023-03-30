using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Sign
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bounds;
    private bool isMoving;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;
    public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
       
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds<= 0)
            {
                anim.SetBool("paused", true);
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
                
            }
            if (!playerInRange)
            {
                anim.SetBool("paused", false);
                Move();
            }
        }
        else
        {
            if(playerInRange==false)
            {
                waitTimeSeconds -= Time.deltaTime;
                if(waitTimeSeconds <= 0)
                {
                    ChooseDifferentDirection();
                    isMoving = true;
                    waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                }
            }
            if(playerInRange==true)
            {
                
                playerDirection();  
                isMoving = false; 
                anim.SetBool("paused", true); 
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            
            }
        }
    }

    private void playerDirection()
    {
        Vector3 playerDirection = player.transform.position - myTransform.position;
         Vector3 roundedDirection = new Vector3(
            Mathf.Round(playerDirection.x),
            Mathf.Round(playerDirection.y),
            Mathf.Round(playerDirection.z));
        if (roundedDirection.x > 0)
        {
            // Player is to the right of this GameObject
             roundedDirection = Vector3.right;
             Debug.Log("changing to right"); 
        }
        else if (roundedDirection.x < 0)
        {
            // Player is to the left of this GameObject
            roundedDirection = Vector3.left;
             Debug.Log("changing to Left"); 
        }

        if (roundedDirection.y > 0)
        {
            // Player is above this GameObject
            roundedDirection = Vector3.up;
            
             Debug.Log("changing to up"); 
        }
        else if (roundedDirection.y < 0)
        {
            // Player is below this GameObject
            roundedDirection = Vector3.down;
            
             Debug.Log("changing to down"); 
        }
        UpdateAnim(roundedDirection);
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                // Walking to the right
                directionVector = Vector3.right;
                break;
            case 1:
                // Walking up
                directionVector = Vector3.up;
                break;
            case 2:
                // Walking Left
                directionVector = Vector3.left;
                break;
            case 3:
                // Walking down
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    void UpdateAnim(Vector3 directionvector)
    {
        anim.SetFloat("moveX", directionvector.x);
        anim.SetFloat("moveY", directionvector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }

}
