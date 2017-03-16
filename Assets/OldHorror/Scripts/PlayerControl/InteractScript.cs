using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

	public delegate void ClickAction(RaycastHit hitObject);
	public static event ClickAction OnClick;
	public static bool isHoldingObject = false;
	private RaycastHit hitObject;

	[SerializeField] private Texture2D cursorTexture = null;
	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	void Awake() {
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Physics.Raycast(Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0)), out hitObject);
			if ( OnClick != null) {
				if (!isHoldingObject) {
					OnClick (hitObject);
					Debug.Log ("OnClick()!");
				}
			}
		}
	}
}
