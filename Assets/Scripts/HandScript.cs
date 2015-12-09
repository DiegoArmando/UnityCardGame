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

        if (this.name.Equals("P1Hand")) { playerID = 1; }
        else if (this.name.Equals("P2Hand")) { playerID = 2; }
    
		//grab playerHand's Handscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();
		hasSelected = false;
	
		setArt();
        
        if (this.name.Equals("P1Hand")) { playerID = 1; }
        else if (this.name.Equals("P2Hand")) { playerID = 2; }
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
		cardArt [0] [0] = new string[5];
		cardArt [0] [0] [0] = "starorescoutcard";
		cardArt [0] [0] [1] = "starorebrutecard";
		cardArt [0] [0] [2] = "staroreelevatorcard";
		cardArt [0] [0] [3] = "staroreexcavatorcard";
		cardArt [0] [0] [4] = "staroreguardcard";

		//StarOre Spells
		cardArt [0] [1] = new string[4];
		cardArt [0] [1] [0] = "starorefistcard";
		cardArt [0] [1] [1] = "starorelevelcard";
		cardArt [0] [1] [2] = "staroresinkholecard";
		cardArt [0] [1] [3] = "staroreevelatecard";

		cardArt [1] = new string[2][];
		//Toris Units
		cardArt [1] [0] = new string[5];
		cardArt [1] [0] [0] = "torisscoutcard";
		cardArt [1] [0] [1] = "torisbrutecard";
		cardArt [1] [0] [2] = "toriselevatorcard";
		cardArt [1] [0] [3] = "torisexcavatorcard";
		cardArt [1] [0] [4] = "torisguardcard";

		//Toris Spells
		cardArt [1] [1] = new string[4];
		cardArt [1] [1] [0] = "torisfistcard";
		cardArt [1] [1] [1] = "torislevelcard";
		cardArt [1] [1] [2] = "torissinkholecard";
		cardArt [1] [1] [3] = "torisevelatecard";
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
