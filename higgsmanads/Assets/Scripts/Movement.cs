using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;

    public float speedMultiplier = 1.0f;

    public Vector2 initialDirection;
    public LayerMask obstacleLayer;// ray cast to only check specific layer(you want to go up but there is a wall in there)
                                    //checking pacman collide with walls or obstacle

    public new Rigidbody2D rigidbody2D { get; private set; }
    //public new Rigidbody2D rigidbody2 { get; private set; } //to prevent inherit problem from parent class
    public Vector2 direction { get; private set; }

    public Vector2 nextDirection { get; private set; }//higgsman automatically move up when available,que up,
    //keep track of direction you trying to move in next,that way it will automatically move
    public Vector3 startingPosition { get; private set; } //when game is reset ,

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody2D.isKinematic = false; // used for ghosts that they can pass through doors without collision
        this.enabled = true;
    }

    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }


    private void FixedUpdate() // in fixed time interval unity calls for frame independent movement
                               //(same consistent experience for low framerate,high framerate users)
                               //(physics should be done in fixedupdate to provide consistent movement)
    {
        Vector2 position = this.rigidbody2D.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody2D.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction,bool forced = false) //higgsman  set direct based on user input 
                                                   //ghost set direction based on theirAI behaviour
    {
        if (forced || !Occupied(direction)) // pile is not occupied or you forced
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }
    //check if tile on moved direction is occupied
    public bool Occupied(Vector2 direction) {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f,this.obstacleLayer);
        return hit.collider != null;
    }

}
