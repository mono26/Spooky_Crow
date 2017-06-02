using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public Transform my_PlayerToFollow;
    public float speed;
    private float zThreshold;
    private float xThreshold;

    public Vector3 moveTemp;
    public float movementThreshold;

    private void Start()
    {
        transform.position = new Vector3(my_PlayerToFollow.position.x, 30f, my_PlayerToFollow.position.z);       
    }
    private void Update()
    {
        if (my_PlayerToFollow.position.x > transform.position.x)
            xThreshold = my_PlayerToFollow.position.x - transform.position.x;
        else
            xThreshold = transform.position.x - my_PlayerToFollow.position.x;

        if (my_PlayerToFollow.position.z > transform.position.z)
            zThreshold = my_PlayerToFollow.position.z - transform.position.z;
        else
            zThreshold = transform.position.z - my_PlayerToFollow.position.z;

        if (xThreshold > movementThreshold || zThreshold > movementThreshold)
        {
            moveTemp = my_PlayerToFollow.position;
            moveTemp.y = 30.0f;     //Valor establecido por la escena.
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.fixedDeltaTime);
        }
    }
}
