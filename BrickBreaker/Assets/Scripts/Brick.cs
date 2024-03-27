using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] public int Health;
    [HideInInspector] public SpriteRenderer spriteRenderer { get; private set; }
    [SerializeField] public Sprite[] brickStates;
    [HideInInspector] public int Points = 100;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Sets the sprite of the brick with respect to its health */
    private void SetSprite()
    {
        this.spriteRenderer.sprite = this.brickStates[Health - 1];
    }

    /* Decreases the health of the brick, and sets its sprite accordingly, destroys it if the health is zero or lesser */
    public void DecreaseHealth()
    {
        this.Health--;

        if (this.Health <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.SetSprite();
        }
    }

}
