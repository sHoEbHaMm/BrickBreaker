using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Tooltip("Speed at which the ball moves")]
    [SerializeField] private float movementSpeed = 500f;

    [HideInInspector] public new Rigidbody2D rigidbody2D { get; private set; } //this rigidbody has a public getter but a private setter. Means no other class can modify it

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 1.5f);
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
}
