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
    public int playerID = 0;


	// Use this for initialization
	void Start () {
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
        int height = gm.CheckHeight(unit_X, unit_Y);
        this.transform.position = new Vector3(unit_X, 2 + (float)height/2, unit_Y);
        if (!gm.CheckOccupy(unit_X, unit_Y)) { gm.ChangeOccupy(unit_X,unit_Y,true); }
	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected) { selected = false; }
            else if (!selected) 
            {  
                selected = true;
                menuPosX = (int)Input.mousePosition.x;
                menuPosY = (int)(Screen.height - Input.mousePosition.y);
                print(menuPosX + "," + menuPosY);
            }
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
        gm.ChangeOccupy(unit_X, unit_Y, false);
        unit_X = new_x;
        unit_Y = new_y;
        gm.ChangeOccupy(unit_X, unit_Y, true);
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
            /*
            else if (gm.CheckHeight(unit_X - 1, unit_Y) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            */
            else if (gm.CheckOccupy(unit_X-1,unit_Y)) 
            {
                print("Unit cannot move to an occupied space");
            }
            else 
            {
                this.transform.position += (new Vector3(-1.0f, 0.0f, 0.0f));
                selected = false;
                gm.ChangeOccupy(unit_X, unit_Y, false);
                unit_X--;
                gm.ChangeOccupy(unit_X, unit_Y, true);
                gm.ChangeOwner(unit_X, unit_Y, playerID);
            }

        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2) + buttonSize, (menuPosY - buttonSize/2), buttonSize, buttonSize), "Right"))
        {
            if (unit_X + 1 >= gm.GetBoardSize()) 
            {
                print("Unit cannot move off the board");
            }
            /*
            else if (gm.CheckHeight(unit_X + 1, unit_Y) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            */
            else if (gm.CheckOccupy(unit_X + 1, unit_Y))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(1.0f, 0.0f, 0.0f));
                selected = false;
                gm.ChangeOccupy(unit_X, unit_Y, false);
                unit_X++;
                gm.ChangeOccupy(unit_X, unit_Y, true);
                gm.ChangeOwner(unit_X, unit_Y, playerID);
            }
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            if (unit_Y + 1 >= gm.GetBoardSize())
            {
                print("Unit cannot move off the board");
            }
            /*
            else if (gm.CheckHeight(unit_X, unit_Y + 1) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            */
            else if (gm.CheckOccupy(unit_X, unit_Y + 1))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(0.0f, 0.0f, 1.0f));
                selected = false;
                gm.ChangeOccupy(unit_X, unit_Y, false);
                unit_Y++;
                gm.ChangeOccupy(unit_X, unit_Y, true);
                gm.ChangeOwner(unit_X, unit_Y, playerID);
            }
        }
        // Down button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize/2), (menuPosY - buttonSize/2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            if (unit_Y - 1 < 0)
            {
                print("Unit cannot move off the board");
            }
            /*
            else if (gm.CheckHeight(unit_X, unit_Y - 1) > gm.CheckHeight(unit_X, unit_Y))
            {
                print("Unit cannot move to higher heights");
            }
            */ 
            else if (gm.CheckOccupy(unit_X, unit_Y - 1))
            {
                print("Unit cannot move to an occupied space");
            }
            else
            {
                this.transform.position += (new Vector3(0.0f, 0.0f, -1.0f));
                selected = false;
                gm.ChangeOccupy(unit_X, unit_Y, false);
                unit_Y--;
                gm.ChangeOccupy(unit_X, unit_Y, true);
                gm.ChangeOwner(unit_X, unit_Y, playerID);
            }
        }
    }
}
