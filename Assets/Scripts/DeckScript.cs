﻿//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckScript : MonoBehaviour {

	public GameObject cardObject;
	private CardScript cardScript;
	public GameObject playerHand;
	private HandScript playerScript;
	
	private List<GameObject> _deck = new List<GameObject> ();
	private List<GameObject> _discard = new List<GameObject> ();

    private Vector3 oldPos;
    private int playerID;
    private GameManager gm;

	//initialize deck
    void Awake()
    {
        oldPos = this.transform.position;

        if (this.name.Equals("P1Deck")) { playerID = 1; }
        else if (this.name.Equals("P2Deck")) { playerID = 2; }
    }

	void Start(){
		//grab playerHand's Handscript component
		cardScript = cardObject.GetComponent<CardScript> ();
		playerScript = playerHand.GetComponent<HandScript> ();

		GameObject temp;

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

        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space) && playerID == gm.GetWhoseTurn()) {
			Debug.Log("space");
			int i = _deck.Count-1;
			GameObject card = _deck[i];
			_deck.RemoveAt(i);
			cardScript = card.GetComponent<CardScript> ();
			cardScript.is_selected = false;
			playerScript.drawCard(card);
		}
	}

	public GameObject discard(GameObject card){
		float voffset = _discard.Count * 0.025f;
		Vector3 offset = new Vector3 (0, voffset, 4.0f);
		card.transform.position = this.transform.position + offset;
		card.transform.rotation = this.transform.rotation;
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
			card.transform.rotation = this.transform.rotation;
			i++;
		}
		_deck = temp;
	}

    // hides the deck
    public void hideDeck()
    {
        this.transform.position += new Vector3(-100, 0, 0);
        for (int i = 0; i < _discard.Count; i++)
        {
            GameObject card = _discard[i];
            float voffset = _discard.Count * 0.025f;
            Vector3 offset = new Vector3(0, voffset, 4.0f);
            card.transform.position = this.transform.position + offset;
            card.transform.rotation = this.transform.rotation;
        }
        for (int i = 0; i < _deck.Count; i++)
        {
            GameObject card = _deck[i];
            float offset = (float)i * 0.025f;
            Vector3 pos = new Vector3(0, offset, 0);
            card.transform.position = this.transform.position + pos;
            card.transform.rotation = this.transform.rotation;
        }
    }

    // shows the deck
    public void showDeck()
    {
        this.transform.position = oldPos;
        for (int i = 0; i < _discard.Count; i++)
        {
            GameObject card = _discard[i];
            float voffset = _discard.Count * 0.025f;
            Vector3 offset = new Vector3(0, voffset, 4.0f);
            card.transform.position = this.transform.position + offset;
            card.transform.rotation = this.transform.rotation;
        }
        for (int i = 0; i < _deck.Count; i++)
        {
            GameObject card = _deck[i];
            float offset = (float)i * 0.025f;
            Vector3 pos = new Vector3(0, offset, 0);
            card.transform.position = this.transform.position + pos;
            card.transform.rotation = this.transform.rotation;
        }
    }
	
}
