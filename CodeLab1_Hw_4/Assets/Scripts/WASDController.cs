using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    public Rigidbody2D myRB;
    public int forceAmount = 10;
    public SpriteRenderer mySprite;
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the sprite's y to its default, right-side up, when W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            myRB.AddForce(Vector2.up * forceAmount);
            mySprite.flipY = false;
        }
        
        //Changes the sprite's x to its default, left, when A is pressed
        if (Input.GetKey(KeyCode.A))
        {
            myRB.AddForce(Vector2.left * forceAmount);
            mySprite.flipX = false;
        }
        
        //Flips the sprite's y so it's upside down when S is pressed
        if (Input.GetKey(KeyCode.S))
        {
            myRB.AddForce(Vector2.down * forceAmount);
            mySprite.flipY = true;
        }
        
        //Flips the sprite's x so it's facing right when D is pressed
        if (Input.GetKey(KeyCode.D))
        {
            myRB.AddForce(Vector2.right * forceAmount);
            mySprite.flipX = true;
        }

        myRB.velocity *= .999f;
    }
}
