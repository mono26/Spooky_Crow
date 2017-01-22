using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    Rigidbody2D my_RigidBody;
    public float damage = 100;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody2D>();
    }
    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        BulletPool.Instance.ReleaseBullet(my_RigidBody);
    }

}