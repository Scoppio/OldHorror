using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOfInterest : MonoBehaviour {
	


	[SerializeField] private Transform pointOfInterest;
	[SerializeField] [Range(0.1f, 10f)] private float translationSpeed;

	private Rigidbody ownRigidbody;
	private Vector3 startingPoint;
	private bool colisionState;
	private bool comingAndGoing;
	private bool wasHit;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<Rigidbody>();
		ownRigidbody = GetComponent<Rigidbody>();
		startingPoint = transform.position;
		comingAndGoing = false;
	}

	void MoveIt() {
		float step = translationSpeed * Time.deltaTime;

		if (comingAndGoing == true) {
			if (colisionState) {
				ColisionChanger ();
			}
			this.transform.position = Vector3.MoveTowards (this.transform.position, pointOfInterest.position, step);
			transform.rotation = Camera.main.transform.rotation;
		} else {
			this.transform.position = Vector3.MoveTowards (this.transform.position, startingPoint, step);
			if (this.transform.position == startingPoint) {
				if (!colisionState) {
					ColisionChanger ();
					wasHit = false;
					this.gameObject.AddComponent<Rigidbody>();
					ownRigidbody = GetComponent<Rigidbody> ();
				}
			}
		}
	}

	void ColisionChanger() {
		Physics.IgnoreLayerCollision (8, 9, colisionState);
		colisionState = !colisionState;
	}

	void ItWasHit() {
		wasHit = true;
		comingAndGoing = !comingAndGoing;
		print ("Coming and Going is now " + comingAndGoing.ToString ());
		Destroy (ownRigidbody);
	}

	// Update is called once per frame
	void Update () {
		if (wasHit) {
			MoveIt ();
		}
	}
}
