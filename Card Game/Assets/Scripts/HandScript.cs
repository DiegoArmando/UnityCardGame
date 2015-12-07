//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandScript : MonoBehaviour {
	public GameObject playerDeck;
	private DeckScript playerDeckScript;
	Ray ray;

	private List<GameObject> _hand = new List<GameObject> ();

	void Start(){
		//grab playerHand's Handscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();

	}

	public GameObject drawCard (GameObject card) {
		Vector3 offset = new Vector3 (0, 0, _hand.Count*1.25f);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
		return card;
	}

	void manageHand(){
		for (int i=0; i<_hand.Count; i++) {
			Vector3 offset = new Vector3(0, 0, i*1.25f);
			GameObject card = _hand[i];
			card.transform.position = this.transform.position + offset;
		}
	}

	void Update(){
		for (int i=0; i<_hand.Count; i++) {
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
