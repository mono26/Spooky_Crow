using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : AIDrop
{
    public int dropReward;
    public int dropIndex;
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
    public void SetReward(int _reward)
    {
        dropReward = _reward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spooky"))
        {
            GameManager.Instance.GiveMoney(this.dropReward);
            PoolsManagerDrop.Instance.ReleaseDrop(this.gameObject);
        }
    }

    private void SetParentVariables()
    {
        this.objectReward = dropReward;
        this.objectIndex = dropIndex;
    }
}
