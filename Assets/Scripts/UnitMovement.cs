﻿using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
      
    private bool selected = false;
    private int menuPosX = 3*Screen.width / 4;
    private int menuPosY = 3*Screen.height / 4;
    private int buttonSize = 50;
    private GameManager gm;
    
    private int unit_X = 0;
    private int unit_Y = 0;
    private int playerID = 0; // 0 = neither; 1 = player1; 2 = player2
    public int unitType = 0; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
    

	// Use this for initialization
	void Awake()
	{
		gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}


	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        int height = gm.CheckHeight(unit_X, unit_Y);
        this.transform.position = new Vector3(unit_X * 1.05f, 1.5f + height/2.0f, unit_Y * 1.05f);
        if (!gm.CheckOccupy(unit_X, unit_Y)) { gm.ChangeOccupy(unit_X,unit_Y,true); }
        gm.ChangeOwner(unit_X, unit_Y, playerID);
        
        string mat = "";
        if (playerID == 1)
        {
            switch (unitType)
            {
                case 0:
                    mat = "StaroreGuardMat";
                    break;
                case 1:
                    mat = "StaroreBruteMat";
                    break;
                case 2:
                    mat = "StaroreExcavatorMat";
                    break;
                case 3:
                    mat = "StaroreScoutMat";
                    break;
                case 4:
                    mat = "StaroreElevatorMat";
                    break;
                default:
                    break;
            }
        }
        else if (playerID == 2)
        {
                switch (unitType)
            {
                case 0:
                    mat = "ToriGuardMat";
                    break;
                case 1:
                    mat = "ToriBruteMat";
                    break;
                case 2:
                    mat = "ToriExcavatorMat";
                    break;
                case 3:
                    mat = "ToriScoutMat";
                    break;
                case 4:
                    mat = "ToriElevatorMat";
                    break;
                default:
                    break;
            }
        }
        Material img = (Material)Resources.Load(mat);
        gameObject.GetComponent<MeshRenderer>().material = img;
	}

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            HandScript hand = ((HandScript)gm.currentHand.GetComponent("HandScript"));
            if (!hand.hasSelected)
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
            else
            {
                CardScript card = ((CardScript)hand.selected.GetComponent("CardScript"));
                if (card.Type == 1)
                {
                    if (gm.CheckActions() > 0)
                    {
                        ((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).doSpell(card.spellType, unit_X, unit_Y);
                        ((HandScript)gm.currentHand.GetComponent("HandScript")).Discard();
                    }
                }
            }
        }
    }

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
        }
        // Right button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right"))
        {
            PositionUpdate(unit_X + 1, unit_Y);
        }
        // Right x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2) + buttonSize * 2, (menuPosY - buttonSize / 2), buttonSize, buttonSize), "Right\nx2"))
        {
            PositionUpdate(unit_X + 2, unit_Y);
        }
        // Up button
        if (selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize, buttonSize, buttonSize), "Up"))
        {
            PositionUpdate(unit_X, unit_Y + 1);
        }
        // Up x2 button (scout only)
        if (unitType == 3 && selected && GUI.Button(new Rect((menuPosX - buttonSize / 2), (menuPosY - buttonSize / 2) - buttonSize * 2, buttonSize, buttonSize), "Up\nx2"))
        {
            PositionUpdate(unit_X, unit_Y + 2);
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
        if (gm.CheckActions() > 0)
        {
            if (new_x < 0 || new_x >= gm.GetBoardSize() || new_y < 0 || new_y >= gm.GetBoardSize())
            { gm.ShowTBMessage("Unit cannot move off the board"); }
            else if (gm.CheckOccupy(new_x, new_y))
            { gm.ShowTBMessage("Unit cannot move to\nan occupied space"); }
            else if (gm.CheckHeight(unit_X, unit_Y) < gm.CheckHeight(new_x, new_y))
            {
                if (gm.CheckActions() < 2) { 
                    selected = false;
                    gm.ShowTBMessage("You do not have enough action\npoints to climb up terrain");
                }
                else
                {
                    selected = false;
                    gm.ChangeOccupy(unit_X, unit_Y, false);
                    unit_X = new_x;
                    unit_Y = new_y;
                    gm.ChangeOccupy(unit_X, unit_Y, true);
                    gm.useAction();
                    gm.useAction();
                }
            }
            else
            {
                selected = false;
                gm.ChangeOccupy(unit_X, unit_Y, false);
                unit_X = new_x;
                unit_Y = new_y;
                gm.ChangeOccupy(unit_X, unit_Y, true);
                gm.useAction();
            }
        }
        else { selected = false; }
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

    // Update unit Position to (new_x, new_y)
    public void SetPosition(int new_x, int new_y)
    {
        unit_X = new_x;
        unit_Y = new_y;
    }
}
