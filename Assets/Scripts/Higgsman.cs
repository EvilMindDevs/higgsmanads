using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
[RequireComponent(typeof(Movement))]
public class Higgsman : MonoBehaviour
{
    public AnimatedSprite deathSequence;
    public SpriteRenderer spriteRenderer { get; private set; }

    public new Collider2D collider { get; private set; }
    public Movement movement { get; private set; }
    public Joystick joystick;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<Collider2D>();
        this.movement = GetComponent<Movement>();
    }
    //higgsman user input  based movement handling
    private void Update()
    {
        float verticalmove = joystick.Vertical;
        if (verticalmove >= .2f)
        {
            this.movement.SetDirection(Vector2.up);
        }
        if (verticalmove <= -.2f)
        {
            this.movement.SetDirection(Vector2.down);
        }

        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    this.movement.SetDirection(Vector2.up);
        //}
        //else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    this.movement.SetDirection(Vector2.down);
        //}

        //else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    this.movement.SetDirection(Vector2.right);
        //}
        //else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    this.movement.SetDirection(Vector2.left);
        //}
        float horizontalMove = joystick.Horizontal;
        if (horizontalMove >= .5f)
        {
            this.movement.SetDirection(Vector2.right);
        }
        if (horizontalMove <= -.5f)
        {
            this.movement.SetDirection(Vector2.left);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.enabled = true;
        this.spriteRenderer.enabled = true;
        this.collider.enabled = true;
        this.deathSequence.enabled = false;
        this.deathSequence.spriteRenderer.enabled = false;
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        this.enabled = false;
        this.spriteRenderer.enabled = false;
        this.collider.enabled = false;
        this.movement.enabled = false;
        this.deathSequence.enabled = true;
        this.deathSequence.spriteRenderer.enabled = true;
        this.deathSequence.Restart();
    }
}
