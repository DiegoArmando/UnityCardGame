//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;


public class CardScript : MonoBehaviour {
	//identity of the card determined by integers
	public int cardType;
	public int unitType;
	public int spellType;
	
	//bool determines if card is selected
	public bool is_selected;

	
	void Start() {
		is_selected = false;
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
		//highlights when mouse hovers over ****COSMETIC****
		//mat.SetColor("_Emission", Color.grey);
	}
	void OnMouseExit(){
		//removes highlight when exiting ****COSMETIC****
		//mat.SetColor ("_Emission", Color.black);
	}
	
	void OnMouseDown(){
		//	sets card as selected; will be picked up by the hand script
		is_selected = true;
	}
	
}
