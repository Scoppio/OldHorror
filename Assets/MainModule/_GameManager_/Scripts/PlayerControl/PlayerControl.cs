// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	//
	// VARIABLES
	//

	public Camera camera;

	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isZooming;		// Is the camera zooming?
	private RaycastHit hit;

	//
	// UPDATE
	//


	void Update () 
	{
		

		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			//mouseOrigin = Input.mousePosition;
			//isRotating = true;
		}

		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)) {
				Transform objectHit = hit.transform;
				print ("Raycast hit " + hit);
				// Do something with the object that was hit by the raycast.
			}
		}

		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			//isZooming = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(2)) isZooming=false;

		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);
		}
	}
}