using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillFoodSelectMenu : MonoBehaviour
{
    public Transform foodLoadoutContent;
    public InventoryItemController inventoryItemController;
    public Transform menuFoodInventory;
    public FoodSelectMenu foodSelectMenu;
    public Transform foodSelectMenuContent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void fillFoodSelectMenu()
    {
        int foodLoadoutCount = foodLoadoutContent.childCount;
        int menuFoodCount = menuFoodInventory.childCount;
        int foodSelectMenuCount = foodSelectMenuContent.childCount;


        // Assuming foodLoadoutContent and menuFoodInventory have the same length.
        int itemCount = Mathf.Min(foodLoadoutCount, menuFoodCount, foodSelectMenuCount);

        for (int i = 0; i < itemCount; i++)
        {
            Transform foodItemSlot = foodLoadoutContent.GetChild(i);
            Transform foodMenuItem = menuFoodInventory.GetChild(i);
            Transform foodSelectMenuSlot = foodSelectMenuContent.GetChild(i);

            InventoryItemController inventoryItemController = foodItemSlot.GetComponentInChildren<InventoryItemController>();
            FoodSelectMenu foodSelectMenu = foodMenuItem.GetComponentInChildren<FoodSelectMenu>();
            FoodSelectMenu foodSelectMenuSlotS = foodSelectMenuSlot.GetComponentInChildren<FoodSelectMenu>();

            if (inventoryItemController != null && foodSelectMenu != null && foodSelectMenuSlotS != null)
            {

                switch (inventoryItemController.foodItem.foodItemType)
                {
                    case FoodItem.FoodItemType.Batch:
                        // Assign the foodItem to the foodSelectMenu
                        foodSelectMenu.foodItem = inventoryItemController.foodItem;
                        foodSelectMenu.foodItemNameSM.text = inventoryItemController.foodItem.foodItemName;
                        foodSelectMenu.foodItemIconSM.sprite = inventoryItemController.foodItem.foodIcon;
                        foodSelectMenu.amountOfFoodThatCanBeMade.text = inventoryItemController.foodItem.foodToBeMade.ToString();
                        if (i == FoodMenuInventoryManager.Instance.foodBatchCount - 1)
                        {
                            Debug.Log("is it going into I == foodBatch!!");

                            foodSelectMenu.foodNeedToCookAmount = FoodMenuInventoryManager.Instance.fillLastBatchWithRemainingFood();
                        }
                        else
                        {
                            foodSelectMenu.foodNeedToCookAmount = FoodMenuInventoryManager.Instance.RandomAmountOfFoodToCook();
                        }
                        foodSelectMenu.foodNeedToCookText.text = foodSelectMenu.foodNeedToCookAmount.ToString();

                        // Assign the foodItem to the foodSelectMenu
                        foodSelectMenuSlotS.foodItem = inventoryItemController.foodItem;
                        foodSelectMenuSlotS.foodItemNameSM.text = inventoryItemController.foodItem.foodItemName;
                        foodSelectMenuSlotS.foodItemIconSM.sprite = inventoryItemController.foodItem.foodIcon;

                        break;
                    case FoodItem.FoodItemType.Single:
                        // Assign the foodItem to the foodSelectMenu
                        foodSelectMenu.foodItem = inventoryItemController.foodItem;
                        foodSelectMenu.amountOfFoodThatCanBeMade.text = inventoryItemController.foodItem.foodToBeMade.ToString();
                        foodSelectMenu.foodNeedToCookAmount = 0;
                        foodSelectMenu.foodNeedToCookText.text = foodSelectMenu.foodNeedToCookAmount.ToString();

                        // Assign the foodItem to the foodSelectMenu
                        foodSelectMenuSlotS.foodItem = inventoryItemController.foodItem;
                        foodSelectMenuSlotS.foodItemNameSM.text = inventoryItemController.foodItem.foodItemName;
                        foodSelectMenuSlotS.foodItemIconSM.sprite = inventoryItemController.foodItem.foodIcon;

                        FoodMenuInventoryManager.Instance.foodSingleCount++;
                        break;
                    default:
                        break;
                }
            }
        }


        // Optionally, you can handle remaining items in menuFoodInventory
        //for (int i = itemCount; i < menuFoodCount; i++)
        //{
        //    Transform foodMenuItem = menuFoodInventory.GetChild(i);
        //    FoodSelectMenu foodSelectMenu = foodMenuItem.GetComponentInChildren<FoodSelectMenu>();

        //    if (foodSelectMenu != null)
        //    {
        //        // Handle remaining items in menuFoodInventory
        //        // You can choose to set foodSelectMenu.foodItem to null or handle it differently.
        //    }
        //}
    }

}
