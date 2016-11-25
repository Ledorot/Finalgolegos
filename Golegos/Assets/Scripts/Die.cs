using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class Die : MonoBehaviour {

	private static Int32[][] rotationLookup = { new [] {3, 6, 4, 1}, new []{5, 6, 2, 1}, new []{4, 6, 3, 1}, new []{2, 6, 5, 1}};

	// Cache objects
	GameObject go;
	Rigidbody rb;
	Renderer rend;

	Boolean isAsleep;

	public Int32 value;

	// Use this for initialization
	void Awake () {
		go = this.gameObject;
		rb = go.GetComponent<Rigidbody> ();
		rend = go.GetComponentInChildren<Renderer> ();
		isAsleep = true;
		value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Boolean sleeping = (rb.velocity.sqrMagnitude <= rb.sleepThreshold);

		//  See if we've had a state change.  If we haven't then don't go further.
		if (sleeping == isAsleep)
			return;

		// Update the state.
		isAsleep = sleeping;

		if (sleeping) {
			// We just went to sleep.  Check the die and update value.
			value = Read();
		} else {
			isAsleep = false;
		}
	}

	public int Read() {
		Vector3 rot = this.transform.eulerAngles;

		Int32 xcoord = (Int32) Math.Round (rot.x / 90) % 4;
		Int32 zcoord = (Int32) Math.Round (rot.z / 90) % 4;

		return Die.rotationLookup [zcoord] [xcoord];
	}

	public void SetMaterial(Material newMat)
	{
		rend.material = newMat;
	}
}
