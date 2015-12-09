//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandScript : MonoBehaviour {
	//reference to the player's deck
	public GameObject playerDeck;
	private DeckScript playerDeckScript;

	//int denotes player number
	int player = 0;

	//array of arrays of card artwork
	private string[][][] cardArt = new string[2][][];

	//List for the hand
	private List<GameObject> _hand = new List<GameObject> ();

	void Start(){
		//grab playerDeck's deckscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();
		//set array of artwork
		setArt ();
	}
	
	public GameObject drawCard (GameObject card) {
		//called by playerDeck to take a card from the deck and add it to the hand****FIX FOR MULTIPLAYER****
		Vector3 offset = new Vector3 (_hand.Count*-2.25f, 0, 0);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
		//reveal card art

		int i = card.GetComponent<CardScript> ().cardType;
		int j;
		if (i == 0)
			j = card.GetComponent<CardScript> ().unitType;
		else
			j = card.GetComponent<CardScript> ().spellType;
		string file = cardArt [player] [i] [j];
		Texture img = (Texture)Resources.Load (file);
		card.GetComponent<Renderer> ().material.mainTexture = img;
		card.GetComponent<CardScript> ().in_hand = true; /////////////////////////*
		return card;
	}
	
	void manageHand(){
		//called when a card is selected to readjust the positioning of the unselected cards
		for (int i=0; i<_hand.Count; i++) {
			Vector3 offset = new Vector3(i*-2.25f, 0, 0);
			GameObject card = _hand[i];
			card.transform.position = this.transform.position + offset;
		}
	}
	
	void Update(){
		for (int i=0; i<_hand.Count; i++) {
			//checks if each card in hand is selected
			//if it is, it is added to the discard list and the hand is managed
			GameObject card = _hand[i];
			CardScript cardScript = card.GetComponent<CardScript>();
			if(cardScript.is_selected){
				_hand.RemoveAt(i);
				cardScript.in_hand = false;
				playerDeckScript.discard(card);
				manageHand();
			}
		}
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

		//waiting for art for StarOre Spells
		cardArt [0] [1] = new string[5];
		cardArt [0] [1] [0] = "CardBack";
		cardArt [0] [1] [1] = "CardBack";
		cardArt [0] [1] [2] = "CardBack";
		cardArt [0] [1] [3] = "CardBack";
		cardArt [0] [1] [4] = "CardBack";

		cardArt [1] = new string[2][];
		//and Units
		cardArt [1] [0] = new string[5];
		cardArt [1] [0] [0] = "CardBack";
		cardArt [1] [0] [1] = "CardBack";
		cardArt [1] [0] [2] = "CardBack";
		cardArt [1] [0] [3] = "CardBack";
		cardArt [1] [0] [4] = "CardBack";

		//and spells
		cardArt [1] [1] = new string[5];
		cardArt [1] [1] [0] = "CardBack";
		cardArt [1] [1] [1] = "CardBack";
		cardArt [1] [1] [2] = "CardBack";
		cardArt [1] [1] [3] = "CardBack";
		cardArt [1] [1] [4] = "CardBack";
	}
}
