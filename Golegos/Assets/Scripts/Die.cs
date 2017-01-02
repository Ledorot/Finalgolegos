using UnityEngine;
using System.Collections;
using System;

namespace Golegos
{
	[RequireComponent (typeof(Rigidbody))]
	public class Die : MonoBehaviour
	{

		// Lookup table to turn die rotations into Die face.
		private static Int32[][] rotationLookup = {
			new [] { 3, 6, 4, 1 },
			new []{ 5, 6, 2, 1 },
			new []{ 4, 6, 3, 1 },
			new [] {
				2,
				6,
				5,
				1
			}
		};

		// How many frames to let pass until determining a die is truly stopped.
		private static Int32 framesUntilTrueSleep = 10;

		// Cache objects
		GameObject go;
		Rigidbody rb;
		Renderer rend;

		// Tracks if the die has stopped moving.
		Boolean isAsleep;
		// Tracks how many frames the die has been asleep.
		Int32 asleepCount;

		// Editor exposed value of the die.  Stays zero until the die slows down.
		// Use this.FullyAsleep instead of relying on the value to determine if it's fully settled.
		public Int32 value;

		// Use this for initialization
		void Awake ()
		{
			go = this.gameObject;
			rb = go.GetComponent<Rigidbody> ();
			rend = go.GetComponentInChildren<Renderer> ();
			isAsleep = true;
			value = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
			// Check to see if we are in a sleep state via the physics engine.
			Boolean sleeping = (rb.velocity.sqrMagnitude <= rb.sleepThreshold);

			// Add to the sleep count if we are asleep or reset the count if we are not.
			if (isAsleep)
				asleepCount++;
			else
				asleepCount = 0;
		
			//  See if we've had a state change.  If we haven't then don't go further.
			if (sleeping == isAsleep)
				return;

			// Update the state.
			isAsleep = sleeping;

			if (sleeping) {
				// We just went to sleep.  Check the die and update value.
				value = Read ();
			} else {
				// Something woke us up.
				isAsleep = false;
			}
		}

		public int Read ()
		{
			// Change the rotation from Quaternion into Degrees.
			Vector3 rot = this.transform.eulerAngles;

			// Divide & modulo to simulate telling which face is most visible.
			Int32 xcoord = (Int32)Math.Round (rot.x / 90) % 4;
			Int32 zcoord = (Int32)Math.Round (rot.z / 90) % 4;

			// Pull from the lookup table.
			return Die.rotationLookup [zcoord] [xcoord];
		}

		// Use this function to determine if a die has fully settled into place.
		public Boolean FullySettled {
			get {
				return (asleepCount >= framesUntilTrueSleep);
			}
		}

		// Editor helper function used to double-check a die read after it settles.
		// This can really go away.
		public void ReadNow ()
		{
			value = Read ();
		}

		// Helper function to allow the die Material to change after instantiation.
		// Was used to watch out for differences between the old and new Die code.
		// This can likely go away.
		public void SetMaterial (Material newMat)
		{
			rend.material = newMat;
		}
	}
}