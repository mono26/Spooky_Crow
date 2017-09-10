using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    private static BulletsPool instance;

    public static BulletsPool Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform poolsPosition;

    [SerializeField]
    private int size;

    private List<GameObject> bullets;

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
        bullets = new List<GameObject>();
        for (int i = 0; i < size; i++)
            AddBullet();
    }

    public GameObject GetBullet()
    {
        if (bullets.Count == 0)
            AddBullet();
        return AllocateBullet();
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        bullets.Add(bullet);
    }

    private void AddBullet()
    {
        GameObject instance = Instantiate(bulletPrefab);
        instance.gameObject.SetActive(false);
        bullets.Add(instance);
    }

    private GameObject AllocateBullet()
    {
        GameObject bullet = bullets[bullets.Count - 1];
        bullets.RemoveAt(bullets.Count - 1);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}
