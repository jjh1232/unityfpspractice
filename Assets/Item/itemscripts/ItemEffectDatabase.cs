using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemEffectDatabase : MonoBehaviour
{

	
    [SerializeField]
    private StatusController thePlayer;
	public AudioClip drink;
	public AudioClip shield;
    static public bool MSTOP = false;

	private const string HP = "HP",CP = "CP", STUN="STUN",SHIELD = "SHIELD";

    public void UseItem(Item _item)
    {

        switch (_item.name)
        {
            case HP:
                thePlayer.IncreaseHP(20);
				sound.instance.RandomizeSfx(drink);
                break;
            case CP:
                thePlayer.IncreaseCP(20);
				sound.instance.RandomizeSfx(drink);
                break;

            case STUN:
                MSTOP = true;
                
                break;
            case SHIELD:
                thePlayer.Shield();
				sound.instance.RandomizeSfx(shield);
                break;
        }
    }
}
