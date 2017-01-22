using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool instance;
    public static EnemyPool Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField]
    private Rigidbody2D enemyPrefab;

    [SerializeField]
    private int size;

    private List<Rigidbody2D> enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrepareEnemy();
        }
        else
            Destroy(gameObject);
    }

    private void PrepareEnemy()
    {
        enemies = new List<Rigidbody2D>();
        for (int i = 0; i < size; i++)
            AddEnemy();
    }

    public Rigidbody2D GetEnemy()
    {
        if (enemies.Count == 0)
            AddEnemy();
        return AllocateEnemy();
    }

    public void ReleaseEnemy(Rigidbody2D enemy)
    {
        enemy.gameObject.SetActive(false);
        enemies.Add(enemy);
    }

    private void AddEnemy()
    {
        Rigidbody2D instance = Instantiate(enemyPrefab);
        instance.gameObject.SetActive(false);
        enemies.Add(instance);
    }

    private Rigidbody2D AllocateEnemy()
    {
        Rigidbody2D enemy = enemies[enemies.Count - 1];
        enemies.RemoveAt(enemies.Count - 1);
        enemy.gameObject.SetActive(true);
        return enemy;
    }
}
