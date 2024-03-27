using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Tooltip("Speed at which the ball moves")]
    [SerializeField] private float movementSpeed = 700f;

    [HideInInspector] public new Rigidbody2D rigidbody2D { get; private set; } //this rigidbody has a public getter but a private setter. Means no other class can modify it
    private GameManager gameManager;

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* This function sets a random path for the ball to follow at the start of the game */
    private void SetRandomTrajectory()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Random.Range(-1f, 1f); //Randomly generate a float value between -1 and 1 (both inclusive) and assign it to the X-component of the direction vector
        direction.y = -1f; //Set the Y-component of the direction vector

        this.rigidbody2D.AddForce(direction.normalized * this.movementSpeed); //normalize it because we only care about direction not magnitude
    }

    /* Called whenever the ball collides with any gameobject in the scene
     If brick is hit, reduces the health of that brick and increments the score via GameManager
     If the ball misses the paddle and hits ground, total lives are reduced via GameManager */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Brick brick = collision.gameObject.GetComponent<Brick>();

        if(brick)
        {
            brick.DecreaseHealth();
            gameManager.IncreaseScore(brick);
        }
        else if(collision.gameObject.tag == "DeathWall")
        {
            gameManager.ReduceLives();
        }
    }

    /* Resets the position and velocity of the ball */
    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody2D.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1.5f);
    }
}
