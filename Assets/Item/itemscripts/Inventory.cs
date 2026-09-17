using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
	

    private ItemEffectDatabase theItemEffectDatabase;
    private Slot slot;
	
	
    
    // 슬롯들.
    private Slot[] slots;


    // Use this for initialization
    void Start()
    {	
		theItemEffectDatabase =FindObjectOfType<ItemEffectDatabase>();
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
		slotuse();
		
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }

    }
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }

        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }

    }
     public void slotuse()
    {
	
            if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				
				if(slots[0].item != null)
               {
                    theItemEffectDatabase.UseItem(slots[0].item);
					slots[0].SetSlotCount(-1);
                
                }


            }
			 if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				
				if(slots[1].item != null)

               {
                    theItemEffectDatabase.UseItem(slots[1].item);
					slots[1].SetSlotCount(-1);
                
                }


            }
			 if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				
				if(slots[2].item != null)

               {
                    theItemEffectDatabase.UseItem(slots[2].item);
					slots[2].SetSlotCount(-1);
                
                }


            }
			 if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				
				if(slots[3].item != null)

               {
                   theItemEffectDatabase.UseItem(slots[3].item);
					slots[3].SetSlotCount(-1);
                
                }


            }

			
        
    
		
    }
	
	
}