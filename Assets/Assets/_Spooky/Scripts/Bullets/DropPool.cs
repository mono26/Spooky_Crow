using System.Collections.Generic;
using UnityEngine;

public class DropPool : MonoBehaviour
{
    private static DropPool instance;

    public static DropPool Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject dropPrefab;
    [SerializeField]
    private Transform poolsPosition;

    [SerializeField]
    private int size;

    private List<GameObject> drops;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrepareDrop();
        }
        else
            Destroy(gameObject);
    }

    private void PrepareDrop()
    {
        drops = new List<GameObject>();
        for (int i = 0; i < size; i++)
            AddDrop();
    }

    public GameObject GetDrop()
    {
        if (drops.Count == 0)
            AddDrop();
        return AllocateDrop();
    }

    public void ReleaseDrop(GameObject drop)
    {
        drop.gameObject.SetActive(false);
        drops.Add(drop);
    }

    private void AddDrop()
    {
        GameObject instance = Instantiate(dropPrefab);
        instance.gameObject.SetActive(false);
        drops.Add(instance);
    }

    private GameObject AllocateDrop()
    {
        GameObject drop = drops[drops.Count - 1];
        drops.RemoveAt(drops.Count - 1);
        drop.gameObject.SetActive(true);
        return drop;
    }
}
