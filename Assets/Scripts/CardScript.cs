//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;


public class CardScript : MonoBehaviour {
	//identity of the card determined by integers
	public int cardType;
	public int unitType;
	public int spellType;
	
	//bool determines if card is selected
	public bool in_hand;
	public bool is_selected;

	//this gameObject's renderer component
	private Renderer rend;

	//this gameObject's position in hand
	private Vector3 handPos;


	void Start() {
		is_selected = false;
		rend = gameObject.GetComponent<Renderer> ();
	}
	
	public void setCard(int type, int unitorspell){
		//this is called when the deck is instantiated to give this card its properties
		cardType = type;
		if (cardType == 0)
			unitType = unitorspell;
		else
			spellType = unitorspell;
	}

	void OnMouseEnter(){
		//card moves toward camera when hovered over
		if (in_hand) {
			handPos = gameObject.transform.position;
			gameObject.transform.position = gameObject.transform.position + (Camera.main.transform.forward * -2);
		}
	}

	void OnMouseExit(){
		//card returns to normal hand position when hoverover is done
		if (in_hand) {
			gameObject.transform.position = handPos;
		}
	}
	
	void OnMouseDown(){
		//	sets card as selected; will be picked up by the hand script
		if (in_hand) {
			is_selected = true;
			Material mat = rend.material;
			Color basecolor = Color.white;
			Color finalcolor = basecolor * Mathf.LinearToGammaSpace(1.0f);
			mat.SetColor ("_EmissionColor", finalcolor);
		}
	}
}
