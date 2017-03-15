using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

	public delegate void ClickAction(RaycastHit hitObject);
	public static event ClickAction OnClick;
	public static bool isHoldingObject = false;
	private RaycastHit hitObject;

	public Texture2D cursorTexture = null;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0))
		{
			Physics.Raycast(Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0)), out hitObject);
			if ( OnClick != null) {
				OnClick(hitObject);
				Debug.Log ("OnClick()!");
			}
		}
	}
}
