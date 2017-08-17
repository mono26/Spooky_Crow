using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public int my_Reward;
    private float my_Time = 5.0f;

    private void OnEnable()
    {
        my_Time = 5.0f;
    }
    private void Update()
    {
        my_Time -= Time.deltaTime;
        if(my_Time <= 0)
        {
            DropPool.Instance.ReleaseDrop(this.gameObject);
        }
    }
    public void SetReward(int _reward)
    {
        my_Reward = _reward;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Crow"))
        {
            GameManager.Instance.GiveMoney(my_Reward);
            DropPool.Instance.ReleaseDrop(this.gameObject);
        }
    }
}
