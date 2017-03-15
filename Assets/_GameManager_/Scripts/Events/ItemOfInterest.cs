using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOfInterest : MonoBehaviour {
	[SerializeField] private Transform pointOfInterest = null;
	[SerializeField] [Range(0.1f, 10f)] private float translationSpeed = 2f;
	[SerializeField] [Range(-2f, 2f)] private float xDistanceAdjustment = 0f;
	[SerializeField] [Range(-2f, 2f)] private float yDistanceAdjustment = 0f;
	[SerializeField] [Range(-2f, 2f)] private float zDistanceAdjustment = 0f;

	[SerializeField] [Range(-180f, 180f)] private float xRotationAdjustment = 0f;
	[SerializeField] [Range(-180f, 180f)] private float yRotationAdjustment = 0f;
	[SerializeField] [Range(-180f, 180f)] private float zRotationAdjustment = 0f;
	[SerializeField] private int idTitle = 0;
	[SerializeField] private int idText = 0;
	[SerializeField] private List<int> idTags = null;

	[ReadOnly] [SerializeField] private string clueTitle = null;
	[ReadOnly] [SerializeField] private string clueText = null;
	[ReadOnly] [SerializeField] private List<string> tags = null;

	private Rigidbody ownRigidbody;
	private Vector3 startingPoint;

	private bool colisionState = true;
	private bool comingAndGoing = false;
	private bool wasHit = false;
	private bool isBeingHold = false;

	private LanguageSelection bagOfWords = null;

	void Awake() {
		bagOfWords = GameObject.FindGameObjectWithTag ("GameController")
			.GetComponent<LanguageSelection> ();

		OnLanguageLoad ();
	}

	private void OnLanguageLoad () {
		clueTitle = bagOfWords.getDialog (idTitle);
		clueText =  bagOfWords.getDialog (idText);

		Debug.Log (bagOfWords.getDialog (idTitle));

		tags = new List<string>();
		foreach (int idTag in idTags) {
			tags.Add (bagOfWords.getDialog (idTag));
		}
	}

	void Start () {
		this.gameObject.AddComponent<Rigidbody> ();
		ownRigidbody = GetComponent<Rigidbody> ();
		startingPoint = transform.position;
	}
		
	void OnEnable() {
		InteractScript.OnClick += InteractScript_OnClick;
		LanguageSelection.OnLanguageLoad += LanguageSelection_OnLanguageLoad;

	}

	void LanguageSelection_OnLanguageLoad ()
	{
		OnLanguageLoad ();
	}

	void OnDisable() {
		InteractScript.OnClick -= InteractScript_OnClick;
		LanguageSelection.OnLanguageLoad -= LanguageSelection_OnLanguageLoad;
	}

	void InteractScript_OnClick (RaycastHit hitObject)
	{
		if ((hitObject.transform == transform && InteractScript.isHoldingObject == false) || 
			(hitObject.transform == transform && isBeingHold)) {
			if (hitObject.transform == transform && isBeingHold && Panels.s_cluePanel) {
				InteractScript.isHoldingObject = false;
				isBeingHold = false;
				Panels.s_cluePanel = false;
			} else {
				InteractScript.isHoldingObject = true;
				Panels.s_cluePanel = true;
				isBeingHold = true;
			}
			Debug.Log ("HIT " + this.gameObject.name);
			ItWasHit ();
		}
	}

	Vector3 TargetPosition (float x, float y, float z) {
		Vector3 ret;
		pointOfInterest.localPosition += new Vector3 (xDistanceAdjustment, yDistanceAdjustment, zDistanceAdjustment);
		ret = new Vector3(pointOfInterest.position.x, pointOfInterest.position.y, pointOfInterest.position.z);
		pointOfInterest.localPosition -= new Vector3 (xDistanceAdjustment, yDistanceAdjustment, zDistanceAdjustment);
		return ret; 
	}

	void MoveIt() {
		float step = translationSpeed * Time.deltaTime;

		if (comingAndGoing == true) {
			if (colisionState) {
				ColisionChanger ();
			}
			transform.rotation = Camera.main.transform.rotation *  Quaternion.Euler(xRotationAdjustment, yRotationAdjustment, zRotationAdjustment);
			this.transform.position = Vector3.MoveTowards (this.transform.position, TargetPosition (xDistanceAdjustment, yDistanceAdjustment, zDistanceAdjustment), step);

		} else {

			this.transform.position = Vector3.MoveTowards (this.transform.position, startingPoint, step);

			if (this.transform.position == startingPoint) {
				if (!colisionState) {
					ColisionChanger ();
					wasHit = false;
					this.gameObject.AddComponent<Rigidbody>();
					ownRigidbody = GetComponent<Rigidbody>();
					//InteractScript.isHoldingObject = false;
					//isBeingHold = false;
				}
			}
		}
	}


	void MyGUI(bool state) {
		Debug.Log ("MyGUI is " + state);
	}

	void ColisionChanger() {
		Physics.IgnoreLayerCollision (8, 9, colisionState);
		colisionState = !colisionState;
		Debug.Log (colisionState);
	}

	public void ItWasHit() {
		wasHit = true;
		comingAndGoing = !comingAndGoing;
		Destroy (ownRigidbody);
	}

	void Update () {
		if (wasHit) {
			MoveIt ();
		}
		if (isBeingHold) {
			if (Panels.s_title != this.clueTitle) {
				Panels.s_title = this.clueTitle;
				Panels.s_text = this.clueText;
				Panels.s_tags = this.tags;
				Debug.Log ("Panel title and text sent!");
			}
		}
	}
}
