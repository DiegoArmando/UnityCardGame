using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
      
    private bool selected = false;
    private int menuPosX = 3*Screen.width / 4;
    private int menuPosY = 3*Screen.height / 4;
    private int buttonSize = 50;
    private GameManager gm;
    
    public int unit_X = 0;
    public int unit_Y = 0;


	// Use this for initialization
	void Start () {
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	
	}
	
	// Update is called once per frame
	void Update () {
        int height = gm.CheckHeight(unit_X, unit_Y);
        this.transform.position = new Vector3(unit_X, 2 + (float)height/2, unit_Y);

	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
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
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) - buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Left")) 
        {
            if (unit_X - 1 < 0) 
            {
                print("Unit cannot move off the board");
            }
            else if (gm.CheckHeight(unit_X - 1, unit_Y) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            else if (gm.CheckOccupy(unit_X-1,unit_Y)) 
            {
                print("Unit cannot move to an occupied space");
            }
            else 
            {
                this.transform.position += (new Vector3(-1.0f, 0.0f, 0.0f));
                selected = false;
                unit_X--;
            }

        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) + buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Right"))
        {
            if (unit_X + 1 > gm.GetBoardSize()) 
            {
                print("Unit cannot move off the board");
            }
            else if (gm.CheckHeight(unit_X + 1, unit_Y) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            else if (gm.CheckOccupy(unit_X + 1, unit_Y))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(1.0f, 0.0f, 0.0f));
                selected = false;
                unit_X++;
            }
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            if (unit_Y + 1 > gm.GetBoardSize())
            {
                print("Unit cannot move off the board");
            }
            else if (gm.CheckHeight(unit_X, unit_Y + 1) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            else if (gm.CheckOccupy(unit_X, unit_Y + 1))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(0.0f, 0.0f, 1.0f));
                selected = false;
                unit_Y++;
            }
        }
        // Down button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            if (unit_Y - 1 < 0)
            {
                print("Unit cannot move off the board");
            }
            else if (gm.CheckHeight(unit_X, unit_Y - 1) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            else if (gm.CheckOccupy(unit_X, unit_Y - 1))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(0.0f, 0.0f, -1.0f));
                selected = false;
                unit_Y--;
            }
        }
    }
}
