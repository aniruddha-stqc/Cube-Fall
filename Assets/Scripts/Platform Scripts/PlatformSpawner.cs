using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform_Prefab;
    public GameObject spikePlatform_Prefab;
    public GameObject[] moving_Platforms;
    public GameObject breakable_Platform;

    public float platform_spawn_Timer = 2f;

    private float current_Platform_Spawn_Timer;

    private int platform_Spawn_Count;

    public float min_X = -1.7f, max_X = 1.7f;
    // Start is called before the first frame update
    void Start()
    {
        current_Platform_Spawn_Timer = platform_spawn_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlatforms();
    }


    void SpawnPlatforms()
    {
        current_Platform_Spawn_Timer += Time.deltaTime;

        if(current_Platform_Spawn_Timer >= platform_spawn_Timer)
        {
            platform_Spawn_Count++;

            Vector3 temp = transform.position;
            // Assign a horizontal position of platform within screen limits
            temp.x = Random.Range(min_X, max_X);

            GameObject newPlatform = null;

            if(platform_Spawn_Count<2)
            {
                //if less than 2 platforms spawn a normal platform
                newPlatform = Instantiate(platform_Prefab, temp, Quaternion.identity);
            }
            else
                if(platform_Spawn_Count == 2)
                {
                    //50-50 chance
                    if (Random.Range(0, 2) > 1.0f)
                    {
                        //create normal platform
                        newPlatform = Instantiate(platform_Prefab, temp, Quaternion.identity);
                    }
                    else
                    {   //create moving platforms any of the Left or Right types
                        newPlatform = Instantiate(moving_Platforms[Random.Range(0, moving_Platforms.Length)], temp, Quaternion.identity);
                        
                    }


                }
                else
                    if(platform_Spawn_Count == 3)
                    {
                        //50-50 chance
                        if (Random.Range(0f, 2f) > 1.0f)
                        {
                            //create normal platform
                            newPlatform = Instantiate(platform_Prefab, temp, Quaternion.identity);
                        }
                        else
                        {   //create platforms of spike type
                            newPlatform = Instantiate(spikePlatform_Prefab, temp, Quaternion.identity);

                        }
                    }
                    else
                        if(platform_Spawn_Count == 4)
                        {
                            //50-50 chance
                            if (Random.Range(0, 2) > 1.0f)
                            {
                                //create normal platform
                                newPlatform = Instantiate(platform_Prefab, temp, Quaternion.identity);
                            }
                            else
                            {   //create platforms of breakable type
                                newPlatform = Instantiate(breakable_Platform, temp, Quaternion.identity);

                            }

                        platform_Spawn_Count = 0;
                        }

            //align below parent
            if(newPlatform)
            {
                newPlatform.transform.parent = transform;
            }
            
            //reset spawn time
            current_Platform_Spawn_Timer = 0f;

        }
    }
}
