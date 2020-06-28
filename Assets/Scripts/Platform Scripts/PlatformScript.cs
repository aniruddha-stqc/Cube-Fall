using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float move_Speed = 1f;
    public float bound_Y = 6f;

    public bool moving_Platform_Left, moving_Platform_Right, is_Breakable, is_Spiked, is_Platform;

    private Animator anim;


    void Awake()
    {
       if(is_Breakable)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        //Keep moving up
        Vector2 temp = transform.position;
        temp.y += move_Speed * Time.deltaTime;
        transform.position = temp;

        // Deactive objects that move out of screen
        if(temp.y >= bound_Y)
        {
            gameObject.SetActive(false);
        }
    }

    void BreakableDeactivate()
    {
        Invoke("DeactivateGameObject", 0.3f);
    }

    void DeactivateGameObject()
    {
        SoundManager.instance.IceBreakSound();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Player")
        {

            // if player drops on spike platform then game over and restart
            if (is_Spiked)
            {

                target.transform.position = new Vector2(1000f, 1000f);
                SoundManager.instance.GameOverSound();
                GameManager.instance.RestartGame();

            }

        }

    } // on trigger enter

    


    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            // Break animation start when lands on breakable platform
            if(is_Breakable)
            {
                SoundManager.instance.LandSound();
                anim.Play("Break");
                
            }

            // if platform then play sound
            if (is_Platform)
            {
                SoundManager.instance.LandSound();

            }


        }
    }

    //Auto move the player when lands on moving platform
    private void OnCollisionStay2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            if(moving_Platform_Left)
            {
                target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-1f);
            }

            if (moving_Platform_Right)
            {
                target.gameObject.GetComponent<PlayerMovement>().PlatformMove(1f);
            }
        }
    }
}
