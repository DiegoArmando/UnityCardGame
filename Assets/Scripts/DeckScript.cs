//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckScript : MonoBehaviour {
	//information about the card object prefab
	public GameObject cardObject;
	private CardScript cardScript;
	//information about the instance of the player's hand
	public GameObject playerHand;
	private HandScript playerScript;
	
	private List<GameObject> _deck = new List<GameObject> ();
	private List<GameObject> _discard = new List<GameObject> ();
	
	
	//initialize deck
	void Start(){
		//grab cardObject's cardScript component
		cardScript = cardObject.GetComponent<CardScript> ();
		//grab playerHand's Handscript component
		playerScript = playerHand.GetComponent<HandScript> ();
		
		GameObject temp;

		//create a deck of 20 cards
		for (int i=0; i<20; i++) {
			//instantiate a card object and give it its unique properties
			temp = (GameObject)Instantiate(cardObject);
			//set cardObject's texture
			Texture img = (Texture)Resources.Load("CardBack");
			temp.GetComponent<Renderer>().material.mainTexture = img;
			cardScript.setCard(0, Random.Range(0, 5));
			_deck.Add (temp);
		}
		//shuffle the deck
		Shuffle ();
	}
	
	void Update() {
		//checks if spacebar was pressed,
		//in which case it sends the top card of the deck to playerHand
		if (Input.GetKeyDown (KeyCode.Space)) {
			int i = _deck.Count-1;
			GameObject card = _deck[i];
			_deck.RemoveAt(i);
			//deselects card, if it was accidentally selected before
			cardScript = card.GetComponent<CardScript> ();
			cardScript.is_selected = false;
			playerScript.drawCard(card);
		}
	}
	
	public GameObject discard(GameObject card){
		//called by playerHand, sends card to discard pile
		float voffset = _discard.Count * 0.025f;
		Vector3 offset = new Vector3 (-2.5f, voffset, 0);
		card.transform.position = this.transform.position + offset;
		_discard.Add (card);
		return card;
	}
	
	private void Shuffle(){
		//called during initialization, shuffles the cardObjects in decklist
		List<GameObject> temp = new List<GameObject> ();
		int i = 0;
		while (_deck.Count != 0) {
			int rand = Random.Range (0, _deck.Count);
			GameObject card = _deck[rand];
			_deck.RemoveAt(rand);
			temp.Add(card);

			Vector3 pos;
			float offset;
			offset = (float)i * 0.025f;
			pos = new Vector3(0, offset , 0);
			card.transform.position = this.transform.position + pos;
			i++;
		}
		_deck = temp;
	}
	
}
