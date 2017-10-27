using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : AIDrop
{
    public int lootReward;
    public int lootIndex;

    [SerializeField]
    private float dropDeadTime = 5.0f;

    private void OnEnable()
    {
        dropDeadTime = 5.0f;
        SetParentVariables();
    }
    private void Update()
    {
        dropDeadTime -= Time.deltaTime;
        if(dropDeadTime <= 0)
        {
            PoolsManagerDrop.Instance.ReleaseDrop(this.gameObject);
        }
    }
    public void SetLoot(int _reward)
    {
        lootReward = _reward;
    }
    public void IncreaseLoot(int _increase)
    {
        lootReward += _increase;
    }
    public void PlaceLoot(Transform place)
    {
        transform.position = place.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spooky"))
        {
            GameManager.Instance.GiveMoney(this.lootReward);
            PoolsManagerDrop.Instance.ReleaseDrop(this.gameObject);
        }
    }

    private void SetParentVariables()
    {
        this.objectReward = lootReward;
        this.objectIndex = lootIndex;
    }
}
