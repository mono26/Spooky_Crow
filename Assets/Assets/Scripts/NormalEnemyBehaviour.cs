using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBehaviour : MonoBehaviour {

    public float ySpeed;
    public float health = 100;

    private GameObject player;
    private Rigidbody2D my_RigidBody;

    void Awake()
    {
        player = GameObject.Find("Player");
        my_RigidBody = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var playerPosition = player.GetComponent<Rigidbody2D>().position;
        var myPosition = my_RigidBody.position;
        var direccion = (playerPosition - myPosition).normalized;
        gameObject.GetComponent<Rigidbody2D>().position += (direccion * ySpeed) * Time.deltaTime ;
    }
}
