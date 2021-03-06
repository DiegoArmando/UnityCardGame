﻿using UnityEngine;
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
                Material img1 = (Material)Resources.Load("StaroreTerrainMat");
                gameObject.GetComponent<MeshRenderer>().material = img1;
                break;

            case 2:
                //change to player 2's color
                Material img2 = (Material)Resources.Load("ToriTerrainMat");
                gameObject.GetComponent<MeshRenderer>().material = img2;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
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
		height += d;
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

				if(hand.hasSelected && gm.CheckActions() > 0)
				{
					CardScript card = ((CardScript)hand.selected.GetComponent("CardScript"));

					print ("type: " + card.Type);
					//print ("public type: " + card.cardType);
					//print ("Champion type: " + CardScript.typeEnum.Champion);
					//print ("Is type equal to champion type? : " + card.Type.CompareTo(CardScript.typeEnum.Champion));

					if( card.Type == 0)
					{
						if(gm.CheckOccupy(xPos, zPos) == false)
						{
							if(((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).checkValid(xPos, zPos, gm.GetWhoseTurn()) == false)
							{
								gm.ShowTBMessage("You must place a unit\nadjacent to controlled territory");
							}
							else
							{
								print ("Space " + xPos + ", " + zPos + " is unoccupied.");
								GameObject newUnit = Instantiate(unit);

								UnitMovement moveScript = (UnitMovement)newUnit.GetComponent("UnitMovement");

								moveScript.SetPosition(xPos, zPos);
								print ("Setting the unit's position to " + xPos + ", " + zPos);

	                   	 		moveScript.setPlayerID(gm.GetWhoseTurn());
								moveScript.unitType = card.unitType;

								//If our unit goes up one, bump that tile up one
								if(card.unitType == 2)
								{
									((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).doSpell(0, xPos, zPos);
								}
								//If our unit goes down one, bump that tile down one
								else if(card.unitType == 4)
								{
									((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).doSpell(1, xPos, zPos);
								}
                                else
                                {
                                    gm.useAction();
                                }
								((HandScript)gm.currentHand.GetComponent("HandScript")).Discard();
                                
							}
						}
						else
						{
							gm.ShowTBMessage("You can only place units\non unoccupied tiles");
						}
					}
                    else if (card.Type == 1)
                    {
                        if (gm.CheckOccupy(xPos, zPos) == true)
                        {
                            ((MakeGrid)GameObject.Find("GameManager").GetComponent("MakeGrid")).doSpell(card.spellType, xPos, zPos);
                            ((HandScript)gm.currentHand.GetComponent("HandScript")).Discard();
                        }
                        else
                        {
                            gm.ShowTBMessage("Spells can only be\nplayed on occupied tiles");
                        }
                    }
                    else
                    {
                        print("Invalid move");
                    }

				}
				else if (gm.CheckActions() <= 0)
				{
					gm.ShowTBMessage("You ran out of action points");
				}
            }
        }
    }
}
