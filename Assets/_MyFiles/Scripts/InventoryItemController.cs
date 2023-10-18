using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public FoodItem foodItem;

    //public Button RemoveButton;
    public int foodItemButtonID;
    public bool equippedFoodItem;
    public GameObject fadeInventoryImage;
    public bool foodToggleIsOn = false;
    
    
    void Awake()
    {
        fadeInventoryImage = GameObject.FindGameObjectWithTag("FadeInventoryImage");
    }

    void Start()
    {
        InventoryManager.Instance.foodBatchInventory.SetActive(false);
        InventoryManager.Instance.foodSingleInventory.SetActive(false);
    }
    
    public void RemoveFoodItem()
    {
        //InventoryManager.Instance.Remove(foodItem);
        //InventoryManager.Instance.FoodItems.Remove(foodItem);

        Destroy(gameObject);
    }

    public void AddItem(FoodItem newFoodItem)
    {
        foodItem = newFoodItem;
        
        Debug.Log("Hello, is this working" +  foodItem);
        //equippedFoodItem = true;
        InventoryManager.Instance.foodSlotsFilled += 1;
        //gameObject.GetComponent<Toggle>().isOn = false;

    }

    public void SetFoodButtonID()
    {
        InventoryManager.Instance.foodButtonIdReceive = foodItemButtonID;
        fadeInventoryImage.SetActive(false);
        foodSlotToggleF();
    }

    public void foodSlotToggleF()
    {
        if (foodToggleIsOn || InventoryManager.Instance.autoSelect)
        {
            transform.parent.GetComponent<Image>().enabled = true;

            if (foodItem == null)
            {
                InventoryManager.Instance.turnOffOtherToggles();
            }
            if (foodItem != null)
            {
                InventoryManager.Instance.turnOffAllToggles();
                fadeInventoryImage.SetActive(true);
            }
        }
        else
        {
            transform.parent.GetComponent<Image>().enabled = false;
        }
    }


    public void Equip_Unequip_FoodSlot()
    {
        if (foodItem == null)
        {
            InventoryManager.Instance.inventoryItemController = this.GetComponent<InventoryItemController>();
            Debug.Log("Is it going into unequipped??!?!");

        }
        else if (foodItem != null)  // this is why it's not working, something with the stupid toggles, damn it!!!!!
        {
            InventoryManager.Instance.RemoveFoodItemSlot();

            InventoryManager.Instance.foodButtonIdPrevious = foodItem.id;

            InventoryManager.Instance.AddFoodBackToInventory();

            foodItem = null;
            fadeInventoryImage.SetActive(true);
            InventoryManager.Instance.foodSlotsFilled -= 1;
            Debug.Log("why the fuck is it going in here??!?!");
        }
    }
    public bool GetFoodSlotToggle()
    {
        return foodToggleIsOn;
    }

    public int GetFoodButtodSlotID()
    {
        return foodItemButtonID;
    }


    public void UseItem()
    {
        switch(foodItem.foodItemType)
        {
            case FoodItem.FoodItemType.Batch:
                break;
            case FoodItem.FoodItemType.Single:
                break;
            default:
                break;
        }
        Debug.Log(foodItem);

        //RemoveItem();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("It enter the area, Boy!!!!");


        if (foodItemButtonID >= 0 && foodItemButtonID < InventoryManager.Instance.foodButtonsBatchCount)
        {
            Debug.Log("BatchOrders!!!");
            InventoryManager.Instance.foodBatchInventory.SetActive(true);
            InventoryManager.Instance.foodSingleInventory.SetActive(false);
        }
        if (foodItemButtonID >= InventoryManager.Instance.foodButtonsBatchCount &&
            foodItemButtonID < InventoryManager.Instance.foodButtonsSingleCount + InventoryManager.Instance.foodButtonsBatchCount)
        {
            Debug.Log("Single Orders!!!!");
            InventoryManager.Instance.foodBatchInventory.SetActive(false);
            InventoryManager.Instance.foodSingleInventory.SetActive(true);
        }

        //foodInventory.SetActive(true);
        transform.parent.GetComponent<Image>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("It exited the area, Dawg!!!!");
        if(!foodToggleIsOn)
        transform.parent.GetComponent<Image>().enabled = false;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("You clicked mothafucka!!!!");
        foodToggleIsOn = true;

        if (InventoryManager.Instance.autoSelect)
        {
            InventoryManager.Instance.inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
            Debug.Log("You stupid fucker!!!!");
            InventoryManager.Instance.autoSelect = false;
        }
        if(foodItem == null)
            InventoryManager.Instance.fadeLoadoutImage.SetActive(true);
        SetFoodButtonID();
        Equip_Unequip_FoodSlot();
    }
}
