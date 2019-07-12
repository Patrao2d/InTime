using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Floats
    public float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpTime;
    private float h;
    private float timeOfJump;

    // Rigidbody
    private Rigidbody2D rb;

    // Animator
    private Animator anim;

    // Booleans
    private bool onFloor = false;
    private bool isLanding = false;
    private bool isJumping = false;
    private bool isRunning = false;
    [HideInInspector] public bool isSliding = false;
    private bool isBreaking = false;
    [HideInInspector] public bool isShieldActive = false;
    

    // Transform
    private Transform groundCheck;

    // GameObjects
    [SerializeField] private GameObject shieldGO;

    // Character Colliders
    [SerializeField] private CapsuleCollider2D onFloorCollider;
    [SerializeField] public CapsuleCollider2D slidingCollider;
    [SerializeField] private BoxCollider2D jumpingCollider;

    private static Player _instance;

    public static Player instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        groundCheck = transform.Find("GroundCheck");

        timeOfJump = -1000.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if character is on floor
        onFloor = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // Case player is on floor and landing sets landing to false
        if (onFloor && isLanding == true)
        {
            isLanding = false;
            anim.SetBool("isLanding", isLanding);
        }

        // When player is on floor force the gravity to 1
        if (onFloor)
        {
            rb.gravityScale = 1.0f;
        }

        // Code from the teacher, gets the velocity of the rb then changes X to the result of h * speed
        Vector2 currentVelocity = rb.velocity;
        currentVelocity = new Vector2(h * speed, currentVelocity.y);

        // Tells the character to jump
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isRunning = false;
            anim.SetBool("isRunning", isRunning);

            isJumping = true;
            anim.SetBool("isJumping", isJumping);

            SetJumpingCollider();
            // Based on the amount of time we press the jump button the Player jumps higher or not
            if (onFloor)
            {
                rb.gravityScale = 1.0f;
                currentVelocity.y = jumpSpeed;
                timeOfJump = Time.time;
            }
            else if ((Time.time - timeOfJump) < jumpTime)
            {
                rb.gravityScale = 1.0f;
            }
            else
            {
                rb.gravityScale = 4.0f;
            }
        }
        else
        {
            timeOfJump = -1000.0f;
            rb.gravityScale = 4.0f;
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }
        rb.velocity = currentVelocity;

        // Tells the character to slide and when in the air adds gravity to the player to fall faster
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isSliding = true;
            anim.SetBool("isSliding", isSliding);
            SetSlidingCollider();
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
            rb.gravityScale = 12.0f;
        }
        else
        {
            isSliding = false;
            anim.SetBool("isSliding", isSliding);
        }

        // Sets the shield to active or false and when he is active the player is invulnerable
        if (isShieldActive == true)
        {
            shieldGO.SetActive(true);
        } else
        {
            shieldGO.SetActive(false);
        }


        // For some reason this wasn't working in the GameManager inside the build so i had to duplicate the code
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            GameManager.instance.ReloadLevel();
        }
        // For some reason this wasn't working in the GameManager inside the build so i had to duplicate the code
        if (Input.GetKeyDown(KeyCode.T))
        {
            CountdownTimer.instance.timer += 1000;
        }


    }
    void FixedUpdate()
    {
        // Checks if the player is losing height if so uses the landing animation
        if (rb.velocity.y < -1 && !onFloor && isLanding == false)
        {
            isLanding = true;
            anim.SetBool("isLanding", isLanding);
            Debug.Log("a cair");
        }

        // Slow down player
        if (Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.LeftArrow))
            {
            h = -0.1f;
            SetOnFloorCollider();
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
            isBreaking = true;
            anim.SetBool("isBreaking", isBreaking);
          
        }

        // Player runs faster 
        else if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow) )
        {
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
            isBreaking = false;
            anim.SetBool("isBreaking", isBreaking);
            SetOnFloorCollider();
            h += 0.04f ;
            if (h >= 2)
            {
                h = 2;                
            }
        }

        // Player runs on the default speed
        else
        {
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
            isBreaking = false;
            anim.SetBool("isBreaking", isBreaking);

            h = 0.5f;
            SetOnFloorCollider();
        }       

        // Makes player move
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        

    }

    void SetOnFloorCollider()
    {
        // Uses the floor collider 
        jumpingCollider.enabled = false;
        onFloorCollider.enabled = true;
        slidingCollider.enabled = false;
    }

    void SetSlidingCollider()
    {
        // Uses the sliding collider
        jumpingCollider.enabled = false;
        onFloorCollider.enabled = false;
        slidingCollider.enabled = true;
        Debug.Log("sliding yo");
    }

    void SetJumpingCollider()
    {
        // Uses the jumping collider
        jumpingCollider.enabled = true;
        onFloorCollider.enabled = false;
        slidingCollider.enabled = false;
    }
}
