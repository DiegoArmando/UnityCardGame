using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
    
    public GameObject buttons = null;
    
    private bool selected = false;
    private int unitPosX = Screen.width / 2;
    private int unitPosY = Screen.height / 2;
    private int buttonSize = 64;
    private int unitSize = 40;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected) { selected = false; }
            else if (!selected) {  selected = true;}
        }
    }

    void Deselect()
    {
        if (selected) { selected = false; };
    }

    void OnGUI()
    {
        if (selected && GUI.Button(new Rect((unitPosX - buttonSize/2) - buttonSize, (unitPosY - buttonSize/2), buttonSize, buttonSize), "Left")) {
            this.transform.position += (new Vector3(-1.0f, 0.0f, 0.0f));
            selected = false;
            unitPosX -= unitSize;
        }
        if (selected && GUI.Button(new Rect((unitPosX - buttonSize/2) + buttonSize, (unitPosY - buttonSize/2), buttonSize, buttonSize), "Right"))
        {
            this.transform.position += (new Vector3(1.0f, 0.0f, 0.0f));
            selected = false;
            unitPosX += unitSize;
        }
        if (selected && GUI.Button(new Rect((unitPosX - buttonSize/2), (unitPosY - buttonSize/2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, 1.0f));
            selected = false;
            unitPosY -= unitSize;
        }
        if (selected && GUI.Button(new Rect((unitPosX - buttonSize/2), (unitPosY - buttonSize/2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, -1.0f));
            selected = false;
            unitPosY += unitSize;
        }
    }
}
