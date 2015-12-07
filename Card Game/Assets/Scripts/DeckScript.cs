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

		for (int i=0; i<20; i++) {
			offset = (float)i * 0.025f;
			pos = new Vector3(0, offset , 0);
			temp = (GameObject)Instantiate(cardObject);
			//temp.transform.parent = this.transform;
			temp.transform.position = this.transform.position + pos;
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
		_discard.Add (card);
		return card;
	}

	public void Shuffle(){
	
	}
	
}
