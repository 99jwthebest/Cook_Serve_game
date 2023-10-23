using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public FoodItem foodItem;

    void Pickup()
    {
        InventoryManager.Instance.Add(foodItem);
        SetFoodItem(foodItem);
        InventoryManager.Instance.ListFoodItems();
        gameObject.SetActive(false);

       

        InventoryManager.Instance.foodButtonIdPrevious += 1;
        Debug.Log("in add " + InventoryManager.Instance.foodButtonIdPrevious);

        //else if(InventoryManager.Instance.foodButtonIdPrevious == 7)
        //{
        //    InventoryManager.Instance.foodButtonIdPrevious = 0;
        //}
        InventoryManager.Instance.AutoSelectNextItemSlot();
    }

    void SetFoodItem(FoodItem foodItemF)
    {
        InventoryManager.Instance.foodItemIM = foodItemF;
    }

    public void ClickButton()
    {
        SoundEffects.instance.PlayClickButtonSound2();
        Pickup();
    }
}
