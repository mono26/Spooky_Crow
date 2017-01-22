using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField]
    private Rigidbody2D bulletPrefab;

    [SerializeField]
    private int size;

    private List<Rigidbody2D> bullets;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrepareBullet();
        }
        else
            Destroy(gameObject);
    }

    private void PrepareBullet()
    {
        bullets = new List<Rigidbody2D>();
        for (int i = 0; i < size; i++)
            AddBullet();
    }

    public Rigidbody2D GetBullet()
    {
        if (bullets.Count == 0)
            AddBullet();
        return AllocateBullet();
    }

    public void ReleaseBullet(Rigidbody2D bullet)
    {
        bullet.gameObject.SetActive(false);
        bullets.Add(bullet);
    }

    private void AddBullet()
    {
        Rigidbody2D instance = Instantiate(bulletPrefab);
        instance.gameObject.SetActive(false);
        bullets.Add(instance);
    }

    private Rigidbody2D AllocateBullet()
    {
        Rigidbody2D bullet = bullets[bullets.Count - 1];
        bullets.RemoveAt(bullets.Count - 1);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}
