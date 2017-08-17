using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 cameraPosition;
    [SerializeField]
    private float verticalOffset = 2.2f;
    [SerializeField]
    private float horizontalOffset = 2.2f;
    // Use this for initialization
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    public float smoothTimeX = 0.33f;
    public float smoothTimeY = 0.33f;
    public float maxSpeed = 1.0f;
    public float updateRate = 2.0f;

    public Transform topLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    public float timeYmodifier = 0.03f;

    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        topLimit = topLimit.GetComponent<Transform>();
        bottomLimit = bottomLimit.GetComponent<Transform>();
        leftLimit = leftLimit.GetComponent<Transform>();
        rightLimit = rightLimit.GetComponent<Transform>();

        transform.position = new Vector3(0,30,0);
    }

    //// Update is called once per frame
    void FixedUpdate()
    {

        FollowPlayer();
    }

    void FollowPlayer()
    {
        float posX = transform.position.x;
        float posZ = transform.position.z;

            posX = Mathf.Lerp(transform.position.x, player.position.x + horizontalOffset, smoothTimeX * Time.deltaTime);
            posZ = Mathf.Lerp(transform.position.z, player.position.z + verticalOffset, smoothTimeY * Time.deltaTime);

            //posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + horizontalOffset, ref velocity.x, smoothTimeX, maxSpeed);
            //posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + verticalOffset, ref velocity.y, smoothTimeY, maxSpeed);

        posZ = Mathf.Clamp(posZ, bottomLimit.position.z, topLimit.position.z);
        posX = Mathf.Clamp(posX, leftLimit.position.x, rightLimit.position.x);
       transform.position = new Vector3(posX,  30, posZ);

    }
}