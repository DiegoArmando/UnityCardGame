//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;




public class CardScript : MonoBehaviour {
	//identity of the card is either a Champion or a Spell
	public enum typeEnum {
		Champion=1,
		Spell=2
	}
	private typeEnum cardType;
	
	//bool determines if card is selected
	public bool is_selected;

	//return functions
	public typeEnum Type { get { return cardType; } }
	public bool Selected { get { return is_selected; } }	
	
	
	void Start() {
		//receiving card art info ****TO DO****
		//mat = gameObject.GetComponent<Renderer> ().material;
		is_selected = false;
	}
	
	public void setCard(int index){
		//this is called when the deck is instantiated to give this card its properties
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
