//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandScript : MonoBehaviour {
	public GameObject playerDeck;
	private DeckScript playerDeckScript;
	private GameObject selected;
	public bool hasSelected;
	Ray ray;
	public int playerID;

	private List<GameObject> _hand = new List<GameObject> ();
	int removeIndex;

	void Start(){
		//grab playerHand's Handscript component
		playerDeckScript = playerDeck.GetComponent<DeckScript> ();
		hasSelected = false;

	}

	public GameObject drawCard (GameObject card) {
		Vector3 offset = new Vector3 ( _hand.Count*1.25f, 0, 0);
		card.transform.position = this.transform.position + offset;
		_hand.Add (card);
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
		for (int i=0; i<_hand.Count; i++) {
			GameObject card = _hand[i];
			CardScript cardScript = card.GetComponent<CardScript>();
			if(cardScript.Selected){
				selected = cardScript.gameObject;
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


}
