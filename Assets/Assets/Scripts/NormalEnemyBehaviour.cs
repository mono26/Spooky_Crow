﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBehaviour : MonoBehaviour {

    public float ySpeed;
    public float health = 100;
    public int scoreValue = 30;

    private Rigidbody2D my_RigidBody;
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start ()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Bullet bullet = col.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            health -= bullet.GetDamage();
            bullet.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }
    

    // Update is called once per frame
    void Update ()
    {
        VerticalMovement();
    }

    void VerticalMovement()
    {
        Vector2 verticalMovement = Vector2.down;
        gameObject.GetComponent<Rigidbody2D>().position += (verticalMovement * ySpeed) * Time.deltaTime;
    }
    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }

    }
