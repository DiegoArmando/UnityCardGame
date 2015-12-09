using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {


	public GameObject unit;

    float halfSec;
	float totalTime;

	int xPos;
	int zPos;

	public int height = 0;

    private GameManager gm;

	// Use this for initialization
	void Start () {
		totalTime = 0;
        halfSec = totalTime + 0.5f;

        // Initalize game manger
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

        // check who owns the tile and adds tint depending on owner
        int owner = gm.CheckOwner(xPos, zPos);
        switch (owner)
        {
            case 0:
                //change to white
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            
            case 1:
                //change to player 1's color
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case 2:
                //change to player 2's color
                gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;

            default:
                print("Owner invalid");
                break;
        }
	
	}

	public void setPos(object[] pos)
	{
		xPos = (int)pos[0];
		zPos = (int)pos[1];
	}

    // Change tile's height by d
    public void ChangeTileHeight(int d)
    {
        transform.position = transform.position + new Vector3(0.0f, 0.5f*d, 0.0f);
		height = d;
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            if (totalTime > halfSec/* && gm.GetSelectMode() == 2*/)
            {
				//gm.currentHand.GetComponent("HandScript").
                //transform.position = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
                halfSec = totalTime + 0.5f;
                // Increase height at xPos,zPos by 1

				HandScript hand = ((HandScript)gm.currentHand.GetComponent("HandScript"));

				if(hand.hasSelected)
				{
					CardScript card = ((CardScript)hand.selected.GetComponent("CardScript"));

					print ("type: " + card.Type);
					//print ("public type: " + card.cardType);
					//print ("Champion type: " + CardScript.typeEnum.Champion);
					//print ("Is type equal to champion type? : " + card.Type.CompareTo(CardScript.typeEnum.Champion));

					if(gm.CheckOccupy(xPos, zPos) == false && card.Type == 0)
					{
					print ("Space " + xPos + ", " + zPos + " is unoccupied.");
					GameObject newUnit = Instantiate(unit);
					//newUnit.SetActive(true);

					UnitMovement moveScript = (UnitMovement)newUnit.GetComponent("UnitMovement");

					moveScript.PositionUpdate(xPos, zPos);
					print ("Setting the unit's position to " + xPos + ", " + zPos);

					//Set the unit's properties here
					//moveScript.setUnitType(card.

                    moveScript.setPlayerID(gm.GetWhoseTurn());
					((HandScript)gm.currentHand.GetComponent("HandScript")).Discard();
					}
					else if(gm.CheckOccupy(xPos, zPos) == true && card.Type == 1)
					{
						((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).doSpell(card.spellType, xPos, zPos);
						((HandScript)gm.currentHand.GetComponent("HandScript")).Discard();
						//GetComponentInParent(MakeGrid).doSpell(card.spellType, xPos, zPos);
					}
					else
					{
						print ("Invalid move");
					}

				}
				else
				{
					print ("Space " + xPos + ", " + zPos + " is occupied or a card is not selected.");
				}
                //gm.ChangeHeight(xPos, zPos, 1);

            }
            /*
			else
			{
				print(Time.deltaTime.ToString() + " " + halfSec.ToString());
			}
            */

        }
        else if(Input.GetMouseButton(1))
        {
            if (totalTime > halfSec && gm.GetSelectMode() == 2)
            {
                //transform.position = transform.position + new Vector3(0.0f, -0.5f, 0.0f);
                //halfSec = totalTime + 0.5f;
                // Decrease height at xPos,zPos by 1
                //gm.ChangeHeight(xPos, zPos, -1);

            }
        }
    }


    //void OnMouseDown()
    //{
      //  print(transform.position.y);
       // transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        
    //}
}
