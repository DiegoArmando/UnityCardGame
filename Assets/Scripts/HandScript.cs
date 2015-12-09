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
	private string[][] cardArt = new string[2][];

	private List<GameObject> _hand = new List<GameObject> ();
	int removeIndex;

    private Vector3 oldPos;

	void Start(){
		//grab playerHand's Handscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();
		print ("Has selected has been set to false in player " + playerID);
		hasSelected = false;
	
		setArt();

        oldPos = this.transform.position;

        if (this.name.Equals("P1Hand")) { playerID = 1; }
        else if (this.name.Equals("P2Hand")) { playerID = 2; }
	}

	public GameObject drawCard (GameObject card) {
		Vector3 offset = new Vector3 ( _hand.Count*1.25f, 0, 0);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
		
		int i = card.GetComponent<CardScript>().Type;
		int j;
		if (i == 0)
			j = card.GetComponent<CardScript> ().unitType;
		else
			j = card.GetComponent<CardScript> ().spellType;
		//Debug.Log (j);
		string file = cardArt [i] [j];
		Texture img = (Texture)Resources.Load (file);
		card.GetComponent<Renderer> ().material.mainTexture = img;
		return card;
	}

	void manageHand(){
		for (int i=0; i<_hand.Count; i++) {
			Vector3 offset = new Vector3(i*1.25f, 0, 0);
			GameObject card = _hand[i];
			card.transform.position = this.transform.position + offset;
		}
	}

	void Update(){
		//print ("HAND IS BEING UPDATED: " + playerID);
		for (int i=0; i<_hand.Count; i++) {
			GameObject card = _hand[i];
			CardScript cardScript = card.GetComponent<CardScript>();
			if(cardScript.is_selected)
			{
				selected = cardScript.gameObject;
				if(hasSelected = false) print ("has selected is now true for player " + playerID);
				hasSelected = true;
				removeIndex = i;

			}
			else
			{
				Debug.Log ("Card " + i + " is not flagged for selection");
				//cardScript.
			}
		}
	}

	public void Discard()
	{
		_hand.RemoveAt(removeIndex);
		playerDeckScript.discard(selected);
		manageHand();
		hasSelected = false;
		print ("has selected is now false for player " + playerID);
	}

	void setArt(){
		cardArt[0] = new string[5];
		cardArt [0] [0] = "starorescoutcard";
		cardArt [0] [1] = "starorebrutecard";
		cardArt [0] [2] = "staroreelevatorcard";
		cardArt [0] [3] = "staroreexcavatorcard";
		cardArt [0] [4] = "staroreguardcard";
	}

    // hides the hand
    public void hideHand()
    {
        this.transform.position += new Vector3(-100, 0, 0);
        for (int i = 0; i < _hand.Count; i++)
        {
            Vector3 offset = new Vector3(i * 1.25f, 0, 0);
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
            Vector3 offset = new Vector3(i * 1.25f, 0, 0);
            GameObject card = _hand[i];
            card.transform.position = this.transform.position + offset;
        }
    }

}
