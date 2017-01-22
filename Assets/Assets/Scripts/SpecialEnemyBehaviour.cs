using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyBehaviour : MonoBehaviour
{

    public float ySpeed;
    public float health = 150;
    public int scoreValue = 50;

    private GameObject player;
    private Rigidbody2D my_RigidBody;
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        player = GameObject.Find("Player");
        my_RigidBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
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

    void Die()
    {
        EnemyPool.Instance.ReleaseEnemy(my_RigidBody);
        scoreKeeper.Score(scoreValue);
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = player.GetComponent<Rigidbody2D>().position;
        var myPosition = my_RigidBody.position;
        var direccion = (playerPosition - myPosition).normalized;
        gameObject.GetComponent<Rigidbody2D>().position += (direccion * ySpeed) * Time.deltaTime;
    }
}