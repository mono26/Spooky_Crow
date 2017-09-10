using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IndicatorManager : MonoBehaviour

{
    public int maxTracked;
    Queue<GameObject> trackedObjects;
    [SerializeField]
    float tiempoRestante;
	// Use this for initialization
	void Start ()
    {
        // GameObject[] trackedObjects = new GameObject[maxTracked];
        trackedObjects = new Queue<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        Debug.Log(trackedObjects.Count());
    }

    public IEnumerator TurnOffCR()
    {
        yield return new WaitForSeconds(5f);
        RemoveObject();
    }
    public void addObject(GameObject obj)
    {
        StartCoroutine(TurnOffCR());
        trackedObjects.Enqueue(obj);
        Debug.Log(trackedObjects.Count());
        obj.GetComponent<ScreenIndicator>().enabled = true;
        if (trackedObjects.Count() >= maxTracked)
        {
            RemoveObject();
          
        }
    }
    public void RemoveObject()
    {
        if (trackedObjects.Count != 0)
        {
            trackedObjects.Peek().GetComponent<ScreenIndicator>().enabled = false;
            Debug.Log("Removi el objeto: " + trackedObjects.Peek());
            trackedObjects.Dequeue();
        }
    }
}
