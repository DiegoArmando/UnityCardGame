//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckScript : MonoBehaviour {

	public GameObject cardObject;
	public GameObject playerHand;
	private HandScript playerScript;
	
	private List<GameObject> _deck = new List<GameObject> ();
	private List<GameObject> _discard = new List<GameObject> ();


	//initialize deck
	void Start(){
		//grab playerHand's Handscript component
		playerScript = playerHand.GetComponent<HandScript> ();

		GameObject temp;
		Vector3 pos;
		float offset;

		//create a deck of 20 cards
		for (int i=0; i<20; i++) {
			offset = (float)i * 0.025f;
			pos = new Vector3(0, offset , 0);
			//instantiate a card object and give it its unique properties
			temp = (GameObject)Instantiate(cardObject);
			//set cardObject's texture
			Texture img;
			if(i<10){
				img = (Texture)Resources.Load("starorescoutcard");
			}
			else img = (Texture)Resources.Load("starorebrutecard");
			temp.GetComponent<Renderer>().material.mainTexture = img;
			temp.transform.position = this.transform.position + pos;
			temp.transform.rotation = this.transform.rotation;
			_deck.Add (temp);
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log("space");
			int i = _deck.Count-1;
			GameObject card = _deck[i];
			_deck.RemoveAt(i);
			playerScript.drawCard(card);
		}
	}

	public GameObject discard(GameObject card){
		float voffset = _discard.Count * 0.025f;
		Vector3 offset = new Vector3 (0, voffset, 2.0f);
		card.transform.position = this.transform.position + offset;
		card.transform.rotation = this.transform.rotation;
		_discard.Add (card);
		return card;
	}

	public void Shuffle(){
		//called during initialization, shuffles the cardObjects in decklist
		List<GameObject> temp = new List<GameObject> ();
		while (_deck.Count != 0) {
			int i = Random.Range (0, _deck.Count);
			GameObject card = _deck[i];
			_deck.RemoveAt(i);
			temp.Add(card);
		}
		_deck = temp;
	}
	
}
