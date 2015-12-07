using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
      
    private bool selected = false;
    private int menuPosX = 3*Screen.width / 4;
    private int menuPosY = 3*Screen.height / 4;
    private int buttonSize = 50;
    
    public int unit_X = 0;
    public int unit_Y = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int height = ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager")).CheckHeight(unit_X, unit_Y);
        this.transform.position = new Vector3(unit_X, 2 + (float)height/2, unit_Y);

	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
			print ("I AM THE UNIT AND I HAVE BEEN CLICKED");
            if (selected) { selected = false; }
            else if (!selected) {  selected = true;}
        }
    }

    // Deselect the unit
    void Deselect()
    {
        if (selected) { selected = false; };
    }

    // Update unit Position to (new_x, new_y)
    void PositionUpdate(int new_x, int new_y)
    {
        unit_X = new_x;
        unit_Y = new_y;
    }

    void OnGUI()
    {
        // Left button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) - buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Left")) {
            this.transform.position += (new Vector3(-1.0f, 0.0f, 0.0f));
            selected = false;
            unit_X--;
        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) + buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Right"))
        {
            this.transform.position += (new Vector3(1.0f, 0.0f, 0.0f));
            selected = false;
            unit_X++;
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, 1.0f));
            selected = false;
            unit_Y++;
        }
        // Down button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            this.transform.position += (new Vector3(0.0f, 0.0f, -1.0f));
            selected = false;
            unit_Y--;
        }
    }
}
