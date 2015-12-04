using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
    
    public GameObject buttons = null;
    private bool selected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        buttons.SetActive(selected);
	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected) { selected = false; }
            else if (!selected) {  selected = true;}
        }
    }
}
