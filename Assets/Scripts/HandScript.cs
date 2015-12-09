//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandScript : MonoBehaviour {
	//reference to the player's deck
	public GameObject playerDeck;
	private DeckScript playerDeckScript;
	
	private List<GameObject> _hand = new List<GameObject> ();


	void Start(){
		//grab playerDeck's deckscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();	
	}
	
	public GameObject drawCard (GameObject card) {
		//called by playerDeck to take a card from the deck and add it to the hand****FIX FOR MULTIPLAYER****
		Vector3 offset = new Vector3 (_hand.Count*-2.25f, 0, 0);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
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
			if(cardScript.Selected){
				_hand.RemoveAt(i);
				playerDeckScript.discard(card);
				manageHand();
			}
		}
	}
	
	
}
