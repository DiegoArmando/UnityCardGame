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
    public int unitType = 0; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
    

	// Use this for initialization
	void Start () {
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
<<<<<<< HEAD
	
=======
>>>>>>> origin/NewUnits
	}
	
	// Update is called once per frame
	void Update () {
        int height = gm.CheckHeight(unit_X, unit_Y);
<<<<<<< HEAD
        this.transform.position = new Vector3(unit_X, 2 + (float)height/2, unit_Y);

=======
        this.transform.position = new Vector3(unit_X * 1.05f, 1.5f + height/2.0f, unit_Y * 1.05f);
        if (!gm.CheckOccupy(unit_X, unit_Y)) { gm.ChangeOccupy(unit_X,unit_Y,true); }
        gm.ChangeOwner(unit_X, unit_Y, playerID);
>>>>>>> origin/NewUnits
	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected) { selected = false; }
            else if (!selected) 
            {
                GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
                foreach (GameObject unit in units)
                {
                    ((UnitMovement)unit.GetComponent("UnitMovement")).Deselect();
                }
                selected = true;
                menuPosX = (int)Input.mousePosition.x;
                menuPosY = (int)(Screen.height - Input.mousePosition.y);
            }
        }
    }

<<<<<<< HEAD
    // Deselect the unit
    void Deselect()
    {
        if (selected) { selected = false; };
    }

    // Update unit Position to (new_x, new_y)
    public void PositionUpdate(int new_x, int new_y)
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

=======
    void OnGUI()
    {
        // Left button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Left"))
        {
            PositionUpdate(unit_X - 1, unit_Y);
        }
        // Left x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize * 2, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Left\nx2"))
        {
            PositionUpdate(unit_X - 2, unit_Y);
>>>>>>> origin/NewUnits
        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right"))
        {
<<<<<<< HEAD
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
=======
            PositionUpdate(unit_X + 1, unit_Y);
        }
        // Right x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize * 2, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right\nx2"))
        {
            PositionUpdate(unit_X + 2, unit_Y);
>>>>>>> origin/NewUnits
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
<<<<<<< HEAD
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
=======
            PositionUpdate(unit_X, unit_Y + 1);
        }
        // Up x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize * 2, buttonSize, buttonSize), "Up\nx2"))
        {
            PositionUpdate(unit_X, unit_Y + 2);
>>>>>>> origin/NewUnits
        }
        // Down button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            PositionUpdate(unit_X, unit_Y - 1);
        }
        // Down x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) + buttonSize * 2, buttonSize, buttonSize), "Down\nx2"))
        {
            PositionUpdate(unit_X, unit_Y - 2);
        }
        // UpLeft button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up\nLeft"))
        {
            PositionUpdate(unit_X - 1, unit_Y + 1);
        }
        // UpRight button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up\nRight"))
        {
            PositionUpdate(unit_X + 1, unit_Y + 1);
        }
        // DownLeft button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down\nLeft"))
        {
            PositionUpdate(unit_X - 1, unit_Y - 1);
        }
        // DownRight button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down\nRight"))
        {
            PositionUpdate(unit_X + 1, unit_Y - 1);
        }
    }

    // Deselect the unit
    public void Deselect()
    {
        if (selected) { selected = false; };
    }

    // Update unit Position to (new_x, new_y)
    public void PositionUpdate(int new_x, int new_y)
    {
        if (new_x < 0 || new_x >= gm.GetBoardSize() || new_y < 0 || new_y >= gm.GetBoardSize())
        { gm.ShowTBMessage("Unit cannot move off the board"); }
        else if (gm.CheckOccupy(new_x, new_y))
        { gm.ShowTBMessage("Unit cannot move to an occupied space"); }
        else
        {
<<<<<<< HEAD
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
=======
            selected = false;
            gm.ChangeOccupy(unit_X, unit_Y, false);
            unit_X = new_x;
            unit_Y = new_y;
            gm.ChangeOccupy(unit_X, unit_Y, true);
>>>>>>> origin/NewUnits
        }
    }

    // set the unit type to u
    // u must be 0, 1, 2, or 3
    // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
    public void setUnitType(int u)
    {
        unitType = u;
    }

}
