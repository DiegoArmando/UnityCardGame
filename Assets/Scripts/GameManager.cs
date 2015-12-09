using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private const int gridSize = 6;
    
    private static int textboxWidth = 150;
    private const int textboxHeight = 30;
    private int textboxPosX = (int)(Screen.width / 2.0f - 150);
    private int textboxPosY = (int)(textboxHeight / 2.0f + 25);
    private string textboxMessage = "";
    private bool showTB = false;
    private float showTimeStart;

    private string description = "";
    private bool showDB = false;
    private float showDBTimeStart;

    private const int scoreboxWidth = 225;
    private const int scoreboxHeight = 25;
    private int scoreboxPosX = Screen.width / 2 - scoreboxWidth / 2;
    private int scoreboxPosY = scoreboxHeight / 2;

    private const int uiboxWidth = 125;
    private const int uiboxHeight = 25;
    private const int uiboxPosX = 20;
    private const int uiboxPosY = 12;

    private int[,] gridHeight = new int[gridSize,gridSize];
    private bool[,] gridOccupided = new bool[gridSize, gridSize];
    private int[,] gridOwner = new int[gridSize, gridSize];
 
    private int playerTurn = 0; // 0 = neither; 1 = player1; 2 = player2
    private int actions = 2;
    private int p1Score = 0;
    private int p2Score = 0;
    private int winner = 0;
    public bool hiddenCards = true;
    private int turnCounter = 20;

	public GameObject currentHand;

	// Use this for initialization
	void Start () {
        // initalize the two grid arrays
        for (int i = 0; i < gridSize; ++i)
        {
            for (int j = 0; j < gridSize; ++j)
            {
                gridHeight[i, j] = 0;
                gridOccupided[i, j] = false;
                gridOwner[i, j] = 0;
            }
        }
        gridOwner[0, 0] = 1;
        gridOwner[gridSize-1, gridSize-1] = 2;
        currentHand = GameObject.Find("P1Hand");
	}
	
	// Update is called once per frame
	void Update () {
	
        textboxPosX = (int)(Screen.width / 2.0f - 150);

        calcScore();

        if (Time.time - showTimeStart > 2.0f) { showTB = false; }
        if (Time.time - showDBTimeStart > 1.0f) { showDB = false; }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("P1Hand").GetComponent("HandScript").SendMessage("deselect");
            GameObject.Find("P2Hand").GetComponent("HandScript").SendMessage("deselect");
            if (!hiddenCards)
            {
                GameObject.Find("P1Hand").GetComponent("HandScript").SendMessage("hideHand");
                GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("hideDeck");
                GameObject.Find("P2Hand").GetComponent("HandScript").SendMessage("hideHand");
                GameObject.Find("P2Deck").GetComponent("DeckScript").SendMessage("hideDeck");
                hiddenCards = true;
            }
            else
            {
                switch (playerTurn)
                {
                    case 0:
                        SwitchTurns(1);
                        GameObject.Find("P1Hand").GetComponent("HandScript").SendMessage("showHand");
                        GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("showDeck");
                        GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("Draw");
                        GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("Draw");
                        hiddenCards = false;
                        break;
                    case 1:
                        SwitchTurns(2);
                        GameObject.Find("P2Hand").GetComponent("HandScript").SendMessage("showHand");
                        GameObject.Find("P2Deck").GetComponent("DeckScript").SendMessage("showDeck");
                        if (turnCounter == 20) { GameObject.Find("P2Deck").GetComponent("DeckScript").SendMessage("Draw"); }
                        GameObject.Find("P2Deck").GetComponent("DeckScript").SendMessage("Draw");
                        hiddenCards = false;
                        break;
                    case 2:
                        SwitchTurns(1);
                        GameObject.Find("P1Hand").GetComponent("HandScript").SendMessage("showHand");
                        GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("showDeck");
                        GameObject.Find("P1Deck").GetComponent("DeckScript").SendMessage("Draw");
                        hiddenCards = false;
                        if (turnCounter > 0) { turnCounter--; }
                        break;
                    default:
                        print("Invalid turn ID");
                        break;
                }
            }
        }

        if (turnCounter <= 0) { winner = getWinner(); }
	}

    void OnGUI()
    {
        if (showTB)
        { GUI.Box(new Rect(20, uiboxPosY + 175, 200, 50), textboxMessage); }

        GUI.Box(new Rect(scoreboxPosX, scoreboxPosY, scoreboxWidth, scoreboxHeight), "Player 1: " + p1Score + " \t Player 2: " + p2Score);

        GUI.Box(new Rect(uiboxPosX, uiboxPosY, uiboxWidth, uiboxHeight), "Player " + playerTurn);

        GUI.Box(new Rect(uiboxPosX, uiboxPosY + 25, uiboxWidth, uiboxHeight), "Action Points: " + actions);

        GUI.Box(new Rect(uiboxPosX, uiboxPosY + 50, uiboxWidth, uiboxHeight), "Turns Left: " + turnCounter);

        if (showDB)
        {GUI.Box(new Rect(uiboxPosX, uiboxPosY + 230, 200, textboxHeight*4), description);}

        if(hiddenCards && playerTurn != 0)
        { GUI.Box(new Rect(textboxPosX, textboxPosY, 300, textboxHeight), "Press T to continue to the next player's turn"); }
        else if (playerTurn == 0) { GUI.Box(new Rect(textboxPosX, textboxPosY, 300, textboxHeight), "Press T to start the game!"); }
        else if (actions <= 0) { GUI.Box(new Rect(textboxPosX, textboxPosY, 300, textboxHeight), "Press T to end your turn"); }
        if (turnCounter <= 0)
        {
            if (winner > 0)
            { GUI.Box(new Rect(uiboxPosX, uiboxPosY + 100, 200, textboxHeight + 10), "The winner is Player " + winner + "!\nCONGRADULATIONS!"); }
            else
            { GUI.Box(new Rect(uiboxPosX, uiboxPosY + 100, 200, textboxHeight + 10), "DRAW!\nThere is no winner."); }
            
        }
        
    }

    // Acessor fnction to get board size
    public int GetBoardSize() { return gridSize; }

    // Check Height at grid position [x,y]
    public int CheckHeight(int x, int y)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return 0; }
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return 0; }
        return gridHeight[x, y];
    }

    // Change Height at grid position [x,y];
    // d = amount of change in height
    // returns false if height goes above 5 or below -5
    // Change Height at grid position [x,y] by d;
    public bool ChangeHeight(int x, int y, int d)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return false; }
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        gridHeight[x, y] += d;
        return true;
    }

    // Check Occupancy of grid postion [x,y]
    public bool CheckOccupy(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return true; }
        return gridOccupided[x, y];
    }

    // Change Occupancy of grid position [x,y] to value of b
    public bool ChangeOccupy(int x, int y, bool b)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        gridOccupided[x, y] = b;
        return true;
    }


    // Check the Owner of block at grid position [x,y]
    public int CheckOwner(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return 0; }
        return gridOwner[x, y];
    }

    // Change the Owner of block at grid position [x,y] to o
    // o must be either 0, 1, or 2
    // 0 = neither; 1 = player1; 2 = player2
    public bool ChangeOwner(int x, int y, int o)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        if (o < 0 || o > 2) { return false; }
        gridOwner[x, y] = o;
        return true;
    }

    // Change the message in center textbox
    public void ShowTBMessage(string message)
    {
        textboxWidth = message.Length * 7;
        textboxMessage = message;
        showTB = true;
        showTimeStart = Time.time;
    }

    // Get the which player's turn it is
    // 0 = neither; 1 = player1; 2 = player2
    public int GetWhoseTurn() { return playerTurn; }

    // Switches the player's turn; t = whose turn it is
    // 0 = neither(Start/End Game); 1 = player1; 2 = player2;
    public void SwitchTurns(int t)
    {
        if (turnCounter > 0 && t >= 0 && t <= 2) { 
            playerTurn = t;
            ShowTBMessage("It is Player " + t + "'s turn");
            actions = 2;
            if (t == 1) { currentHand = GameObject.Find("P1Hand"); }
            else if (t == 2) { currentHand = GameObject.Find("P2Hand"); }
        }
    }

    // check the number of actions remaining for player
    public int CheckActions()
    {
        if (actions <= 0) { ShowTBMessage("You've ran out of action points"); }
        return actions;
    }

    // deducts an action 
    public bool useAction()
    {
        if (actions <= 0) { return false; }
        actions--;
        return true;
    }

    // calculate score
    public void calcScore()
    {
        int p1calc = 0;
        int p2calc = 0;
        for (int i = 0; i < gridSize; i++){
            for (int j = 0; j < gridSize; j++) {
                if (gridOwner[i, j] == 1) {
                    if (gridHeight[i,j] <= 0) 
                    { p1calc += -(gridHeight[i, j]) + 1; }
                }
                else if (gridOwner[i, j] == 2){
                    if (gridHeight[i, j] <= 0) 
                    { p2calc += -(gridHeight[i, j]) + 1; }
                }
            }
        }
        p1Score = p1calc;
        p2Score = p2calc;
    }
    
    // get the winner
    // 0 = draw; 1 = player1; 2 = player2
    public int getWinner()
    {
        if (p1Score > p2Score) { return 1; }
        else if (p1Score < p2Score) { return 2; }
        else { return 0; }
    }

    // Change the message in description box
    public void ShowDBMessage(int type, int unitType, int spellType)
    {
        if (type == 0) {
            if (playerTurn == 1) {
                switch (unitType) {
                    case 0:
                        description = "StarOre Guard\n-UNIT-\n-Regular unit\n-Master Buford Beauregarde\nthe 2nd, son of former member\nof the Royal Guard Master\nBuford Beauregarde the 1st.";
                        break;
                    case 1:
                        description = "StarOre Brute\n-UNIT-\n-Regular unit\n-Master Buford Beauregarde\nthe first, former member of\nthe Royal Guard.";
                        break;
                    case 2:
                        description = "StarOre Excavator\n-UNIT-\n-Descends the tile he's\nstanding on\n-The Honorable Quincy James\nMatthews the fourth.\nAn expert in excavation magic.";
                        break;
                    case 3:
                        description = "StareOre Scout\n-UNIT-\n-Can move farther\n-Sir Horace Pennyweather.\nMiss Meredith’s\npersonal bodyguard.";
                        break;
                    case 4:
                        description = "StarOre Elevator\n-UNIT-\n-Ascends the tile she's\nstanding on\n-Lady Elizabeth Hillridge.\nAn expert in elevation magic.";
                        break;
                    default:
                        break;
                }
            }
            else if (playerTurn == 2) {
                switch (unitType)
                {
                    case 0:
                        description = "Tori's Guard\n-UNIT-\n-Regular unit\n\n-Jackie Porter.\nTori’s old college roommate.";
                        break;
                    case 1:
                        description = "Tori's Brute\n-Regular unit\n\n-Jason McHenry. Jackie’s rich neighbor.";
                        break;
                    case 2:
                        description = "Tori's Excavator\n-UNIT-\n-Descends the tile she's\nstanding on\n\n-Rebecca Jordan.\nTori’s college best friend.";
                        break;
                    case 3:
                        description = "Tori's Scout\n-UNIT-\n-Can move farther\n\n-Harrison Bennett.\nTori’s neighbor.";
                        break;
                    case 4:
                        description = "Tori's Elevator\n-UNIT-\n-Ascends the tile he's\nstanding on\n\n-Lenny Brotelli.\nRebecca’s boyfriend.";
                        break;
                    default:
                        break;
                }
            }
        }
        else if (type == 1) {
            switch (spellType)
            {
                case 0:
                    description = "Sinkhole\n-SPELL-\n-Descends the tile targeted.";
                    break;
                case 1:
                    description = "Elevate\n-SPELL-\n-Ascends the tile targeted.";
                    break;
                case 2:
                    description = "Level\n-SPELL-\n-Change all surrounding tiles\nto targeted tile's height.";
                    break;
                case 3:
                    description = "Fist From Below\n-SPELL-\n-Ascends the target tile and\nall tiles surrounding the targeted\ntile.";
                    break;
                default:
                    break;
            }
        }
        showDB = true;
        showDBTimeStart = Time.time;
    }
}
