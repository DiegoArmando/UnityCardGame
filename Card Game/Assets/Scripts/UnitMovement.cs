using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
      
    private bool selected = false;
    private int menuPosX = 3*Screen.width / 4;
    private int menuPosY = 3*Screen.height / 4;
    private int buttonSize = 50;
    private int unitSize = 40;
    public float unit_X = 0.0f;
    public float unit_Y = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(unit_X, 2.0f, unit_Y);

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
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) - buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Left")) {
            this.transform.position += (new Vector3(-1.0f, 0.0f, 0.0f));
            selected = false;
            unit_X -= 1.0f;
        }
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) + buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Right"))
        {
            this.transform.position += (new Vector3(1.0f, 0.0f, 0.0f));
            selected = false;
            unit_X += 1.0f;
        }
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, 1.0f));
            selected = false;
            unit_Y += 1.0f;;
        }
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, -1.0f));
            selected = false;
            unit_Y -= 1.0f;
        }
    }
}
