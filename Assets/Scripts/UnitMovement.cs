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
    public int playerID = 0; // 0 = neither; 1 = player1; 2 = player2
    public int unitType = 0; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
    

	// Use this for initialization
	void Awake()
	{
		gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}


	void Start () {
        
		print ("I have been created");

	}
	
	// Update is called once per frame
	void Update () {
        int height = gm.CheckHeight(unit_X, unit_Y);
        this.transform.position = new Vector3(unit_X * 1.05f, 1.5f + height/2.0f, unit_Y * 1.05f);
        if (!gm.CheckOccupy(unit_X, unit_Y)) { gm.ChangeOccupy(unit_X,unit_Y,true); }
        gm.ChangeOwner(unit_X, unit_Y, playerID);
        print("(" + unit_X + "," + unit_Y + "):" + gm.CheckOwner(unit_X, unit_Y));
	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (gm.GetWhoseTurn() != playerID) { gm.ShowTBMessage("This unit is not yours"); }
            else if (selected) { selected = false; }
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

    void OnGUI()
    {
        // Left button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Left"))
        {
            if (gm.CheckActions() > 0)
            { 
                PositionUpdate(unit_X - 1, unit_Y);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Left x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize * 2, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Left\nx2"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X - 2, unit_Y);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X + 1, unit_Y);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Right x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize * 2, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right\nx2"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X + 2, unit_Y);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X, unit_Y + 1);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Up x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize * 2, buttonSize, buttonSize), "Up\nx2"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X, unit_Y + 2);
                gm.useAction();
            }
            else { selected = false; }

        }
        // Down button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X, unit_Y - 1);
                gm.useAction();
            }
            else { selected = false; }
        }
        // Down x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) + buttonSize * 2, buttonSize, buttonSize), "Down\nx2"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X, unit_Y - 2);
                gm.useAction();
            }
            else { selected = false; }
        }
        // UpLeft button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up\nLeft"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X - 1, unit_Y + 1);
                gm.useAction();
            }
            else { selected = false; }
        }
        // UpRight button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up\nRight"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X + 1, unit_Y + 1);
                gm.useAction();
            }
            else { selected = false; }
        }
        // DownLeft button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) - buttonSize, (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down\nLeft"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X - 1, unit_Y - 1);
                gm.useAction();
            }
            else { selected = false; }
        }
        // DownRight button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2) + buttonSize, buttonSize, buttonSize), "Down\nRight"))
        {
            if (gm.CheckActions() > 0)
            {
                PositionUpdate(unit_X + 1, unit_Y - 1);
                gm.useAction();
            }
            else { selected = false; }
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
		/*if (gm == null)
		{
			print ("Game Manager doesn't exist yet");
			gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
		}*/
        if (new_x < 0 || new_x >= gm.GetBoardSize() || new_y < 0 || new_y >= gm.GetBoardSize())
        { gm.ShowTBMessage("Unit cannot move off the board"); }
        else if (gm.CheckOccupy(new_x, new_y))
        { gm.ShowTBMessage("Unit cannot move to an occupied space"); }
        else
        {
            selected = false;
            gm.ChangeOccupy(unit_X, unit_Y, false);
            unit_X = new_x;
            unit_Y = new_y;
            gm.ChangeOccupy(unit_X, unit_Y, true);
        }
    }

    // set the unit type to u
    // u must be 0, 1, 2, or 3
    // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
    public bool setUnitType(int u)
    {
        if (u < 0 || u > 3) {return false;}
        unitType = u;
        return true;
    }

    // set the player id (owner) to p
    // p must be 1 or 2
    // 1 = player1; 2 = player2
    public bool setPlayerID(int p)
    {
        if (p < 1 || p > 2) { return false; }
        playerID = p;
        return true;
    }

}
