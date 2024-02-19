using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleBehavior : MonoBehaviour
{
    //whenever an apple (rotten or not) is touched, it will move to a different location on screen
    //if the apple is a regular one, increments the score
    //if the apple is rotten (checked through the tag on the apple object in question), decrements score
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Rotten")
        {
            GameManager.Instance.Score--;
        }
        else
        {
            GameManager.Instance.Score++;
        }
        transform.position = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-4f, 4f));
    }
}
