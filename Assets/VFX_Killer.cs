using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Killer : MonoBehaviour {

	public float deadTime;
	public float deadTimer;

	// Use this for initialization
	void Start () {
		deadTimer = deadTime;
	}
	
	// Update is called once per frame
	void Update () {
		deadTimer -= Time.deltaTime;
		if(deadTimer < 0)
		{
			Destroy (gameObject);
		}
	}
}
