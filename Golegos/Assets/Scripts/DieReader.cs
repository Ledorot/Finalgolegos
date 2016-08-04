using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class DieReader : MonoBehaviour {
	// This is a little bit (0.01f) bigger than than half the distance between alternate corners of a cube of length 1.2 (Accounting for extended faces)
	static readonly float RaycastOriginOffset = ((1.2f * Mathf.Sqrt(3)) / 2f) + 0.01f;
	// The layer the die faces are on
	public LayerMask DieLayer;
	// The currently read value of the die
	public int Value;

	void Update () {
		// Do a read every frame.
		// This is PURELY for visualization purposes. There will be no need for a Value variable later.
		Value = Read ();
	}

	// Return the value of the rolled die
	public int Read () {
		// Look down at the die from directly above, at a distance a small amount away from the maximum closeness of a die face
		Ray cast = new Ray (transform.position + Vector3.up * RaycastOriginOffset, Vector3.down);
		RaycastHit hit;

		// If we saw a die face
		if (Physics.Raycast (cast, out hit, Mathf.Infinity, DieLayer)) {
			// Visualize the raycast
			Debug.DrawLine (cast.origin, hit.point, Color.red);
			int face;
			// If the face's name makes sense, parse it, otherwise, read 0
			if (int.TryParse (hit.collider.gameObject.name, out face)) {
				return face;
			}
		}
		// If we weren't able to read a valid value, return a 0 (for safety)
		return 0;
	}
}
