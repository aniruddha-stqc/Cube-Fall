using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    // Maximum movement to right, left and down
    public float min_X = -1.8f, max_X = 2.0f, min_Y = -7.0f;

    private bool out_Of_Bounds;
    
    void Update()
    {
        CheckBounds(); 
    }

    private void CheckBounds()
    {
        Vector2 temp = transform.position;

        //don't allow right movement beyond max
        if(temp.x > max_X)
        {
            temp.x = max_X;
        }
        //don't allow left movement beyond min
        if (temp.x < min_X)
        {
            temp.x = min_X;
        }

        // reset position of player 
        transform.position = temp;

        //if player drops below then die
        if (temp.y <= min_Y)
        {
            if(!out_Of_Bounds)
            {
                out_Of_Bounds = true;
                SoundManager.instance.DeathSound();
                GameManager.instance.RestartGame();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        // if player hits top spikes then die
        if(target.tag == "TopSpike")
        {
            transform.position = new Vector2(1000f, 1000f);
            SoundManager.instance.DeathSound();
            GameManager.instance.RestartGame();
        }
    }
}
