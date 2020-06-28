using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody;
    public float moveSpeed = 2f;
    private Vector2 touchOrigin;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = 0f;

        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        horizontal = Input.GetAxisRaw("Horizontal");


        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            //Speed is different for mobiles
            moveSpeed = 7f;
            //Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {
                //Store the first touch detected.
                Touch myTouch = Input.touches[0];

                //Check if the phase of that touch equals Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
                    touchOrigin = myTouch.position;
                }

                //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    //Set touchEnd to equal the position of this touch
                    Vector2 touchEnd = myTouch.position;

                    //Calculate the difference between the beginning and end of the touch on the x axis.
                    float x = touchEnd.x - touchOrigin.x;

                    //Calculate the difference between the beginning and end of the touch on the y axis.
                    //float y = touchEnd.y - touchOrigin.y;

                    //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                    touchOrigin.x = -1;

                    //Check if the difference along the x axis is greater than the difference along the y axis.
                    //if (Mathf.Abs(x) > Mathf.Abs(y))
                        //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                        horizontal = x > 0 ? 1 : -1;
                    //else
                        //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                       // vertical = y > 0 ? 1 : -1;
                }
            }

        #endif //End of mobile platform dependendent compilation section started above with #elif

        //controls movement on cursor arrow press
        if (horizontal > 0f)
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
        }
        //controls movement on cursor arrow press
        if (horizontal < 0f)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
        }
        //Check if we are running either in the Unity editor or in a standalone build.
    }

    //Used on moving platform
    public void PlatformMove(float x)
    {
        myBody.velocity = new Vector2(x, myBody.velocity.y);
    }

}
