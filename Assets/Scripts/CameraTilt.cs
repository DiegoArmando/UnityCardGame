using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour {

	public GameObject rotationPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.up = Vector3.forward;
	
		if (Input.GetKey ("down")) {
			transform.RotateAround(rotationPoint.transform.position, Vector3.left, 20 * Time.deltaTime);
		}

		if (Input.GetKey ("up")) {
			transform.RotateAround(rotationPoint.transform.position, Vector3.right, 20 * Time.deltaTime);
		}

		if (Input.GetKey ("left")) {
			transform.RotateAround(rotationPoint.transform.position, Vector3.forward, 20 * Time.deltaTime);
		}

		if (Input.GetKey ("right")) {
			transform.RotateAround(rotationPoint.transform.position, Vector3.back, 20 * Time.deltaTime);
		}


	}
}
