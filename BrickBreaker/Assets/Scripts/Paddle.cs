 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Tooltip("Speed at which the paddle moves")]
    [SerializeField] private float movementSpeed = 30f;
    [Tooltip("Maximum angle at which the ball can bounce with respect to Up Vector")]
    [SerializeField] private float maxBounceAngle = 75f;

    [HideInInspector] public new Rigidbody2D rigidbody2D { get; private set; } // this rigidbody has a public getter but a private setter. Means no other class can modify it
    [HideInInspector] public Vector2 direction { get; private set; } // this Vector2 has a public getter but a private setter.Means no other class can modify it

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    /* Physics updates are recommended to do in FixedUpdate because:
    Different machines run at different clock speeds and the delta time is variable
    To ensure a uniform experience across all machines, we should do the physics update in FixedUpdate
    Because unlike Update(), FixedUpdate is called at a fixed interval of time */
    private void FixedUpdate()
    {
        MovePaddle();
    }

    /* This function checks for player input from keyboard */
    private void CheckInput()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // If Left Arrow or A is pressed: Sets the direction vector to Left
        {
            this.direction = Vector2.left;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // If Right Arrow or D is pressed: Sets the direction vector to Right
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero; // Or else if nothing is pressed: Sets the direction to Zero
        }
    }

    /* This function moves the paddle based on the player input from keyboard */
    private void MovePaddle()
    {
        if(this.direction != Vector2.zero)
        {
            this.rigidbody2D.AddForce(this.direction * this.movementSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        // Calculate the angle at which the ball bounces based on where it hit the paddle
        if(ball)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody2D.velocity);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion newRotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody2D.velocity = newRotation * Vector2.up * ball.rigidbody2D.velocity.magnitude;
        }
    }

    /* Resets the position and velocity of the paddle */
    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody2D.velocity = Vector2.zero;
    }
}
