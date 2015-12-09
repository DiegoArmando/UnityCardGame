//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandScript : MonoBehaviour {
	public GameObject playerDeck;
	private DeckScript playerDeckScript;
	
	public GameObject selected;
	public bool hasSelected;
	
	public int playerID;
	
	//array of arrays of card artwork
	private string[][][] cardArt = new string[2][][];

	private List<GameObject> _hand = new List<GameObject> ();
	int removeIndex;

    private Vector3 oldPos;

    void Awake()
    {
        oldPos = this.transform.position;

        
    
		//grab playerHand's Handscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();
		hasSelected = false;
	
		setArt();
	}



    void Start()
    {
    }

	public GameObject drawCard (GameObject card) {
		Vector3 offset = new Vector3 ( _hand.Count*2.5f, 0, 0);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
		
		int i = card.GetComponent<CardScript>().Type;
		int j;
		if (i == 0)
			j = card.GetComponent<CardScript> ().unitType;
		else
			j = card.GetComponent<CardScript> ().spellType;
		print ("values: " + (playerID - 1) + ", " + i + " " + j);
		print ("Card art value: " + cardArt [playerID - 1] [i] [j]);
		string file = cardArt [playerID-1] [i] [j];
		Texture img = (Texture)Resources.Load (file);
		card.GetComponent<Renderer> ().material.mainTexture = img;
		card.GetComponent<CardScript> ().in_hand = true;
		return card;
	}

	void manageHand(){
		for (int i=0; i<_hand.Count; i++) {
			Vector3 offset = new Vector3(i*2.5f, 0, 0);
			GameObject card = _hand[i];
			card.transform.position = this.transform.position + offset;
		}
	}

	void Update(){
		if (this.name.Equals("P1Hand")) { playerID = 1; }
		else if (this.name.Equals("P2Hand")) { playerID = 2; }

		for (int i=0; i<_hand.Count; i++) {
			GameObject card = _hand[i];
			CardScript cardScript = card.GetComponent<CardScript>();
			if(cardScript.is_selected){
				selected = cardScript.gameObject;
				if(hasSelected == false) print ("has selected is now true for player " + playerID);
				hasSelected = true;
				removeIndex = i;
				//print("Selected card");
			}
		}
	}

	public void Discard()
	{

		_hand.RemoveAt(removeIndex);
		playerDeckScript.discard(selected);
		manageHand();
		hasSelected = false;
	}

	void setArt(){
		cardArt[0] = new string[2][];
		//StarOre Units
		// 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout; 4 = Go Up Unit
		cardArt [0] [0] = new string[5];
		cardArt [0] [0] [0] = "staroreguardcard";
		cardArt [0] [0] [1] = "starorebrutecard";
		cardArt [0] [0] [2] = "staroreexcavatorcard";
		cardArt [0] [0] [3] = "starorescoutcard";
		cardArt [0] [0] [4] = "staroreelevatorcard";

		//StarOre Spells
		cardArt [0] [1] = new string[4];
		cardArt [0] [1] [0] =  "staroreevelatecard";
		cardArt [0] [1] [1] = "staroresinkholecard";
		cardArt [0] [1] [2] = "starorelevelcard";
		cardArt [0] [1] [3] = "starorefistcard";

		cardArt [1] = new string[2][];
		//Toris Units
		cardArt [1] [0] = new string[5];
        cardArt[1][0][0] = "torisguardcard"; 
		cardArt [1] [0] [1] = "torisbrutecard";
        cardArt[1][0][2] = "torisexcavatorcard"; 
        cardArt[1][0][3] = "torisscoutcard";
        cardArt[1][0][4] = "toriselevatorcard";

		//Toris Spells
		cardArt [1] [1] = new string[4];
		cardArt [1] [1] [0] = "torisevelatecard";
		cardArt [1] [1] [1] = "torissinkholecard";
		cardArt [1] [1] [2] = "torislevelcard";
		cardArt [1] [1] [3] = "torisfistcard";
	}

    // hides the hand
    public void hideHand()
    {
        this.transform.position += new Vector3(-100, 0, 0);
        for (int i = 0; i < _hand.Count; i++)
        {
            Vector3 offset = new Vector3(i * 2.5f, 0, 0);
            GameObject card = _hand[i];
            card.transform.position = this.transform.position + offset;
        }
    }

    // shows the hand
    public void showHand()
    {
        this.transform.position = oldPos;
        for (int i = 0; i < _hand.Count; i++)
        {
            Vector3 offset = new Vector3(i * 2.5f, 0, 0);
            GameObject card = _hand[i];
            card.transform.position = this.transform.position + offset;
        }
    }

	public void deselect()
	{
		hasSelected = false;
		for (int i = 0; i < _hand.Count; i++)
		{
			GameObject card = _hand[i];
			CardScript cardScript = card.GetComponent<CardScript>();
			cardScript.is_selected = false;
		}
	}

}
