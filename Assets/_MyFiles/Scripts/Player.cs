using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField]
    int stepInCurrentOrder;
    [SerializeField]
    FoodItem foodItemPrepping;
    [SerializeField]
    FoodSelectMenu foodSelectMenu;
    [SerializeField]
    FoodItemTakeoutWindow foodTakeoutWindow;


    public enum PlayerState
    {
        None,
        MakingBatchOrder,
        MakingSingleOrder
    }

    public PlayerState pState;

    private void Awake()
    {
        instance = this;
        pState = PlayerState.None;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            GetOutOfMakingBatchOrder();
            FinishMakingBatchOrder();
            FinishMakingSingleOrder();
            FoodMenuInventoryManager.Instance.menuFoodInventory.gameObject.SetActive(false);
            if (pState != PlayerState.MakingSingleOrder)
            {
                FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(false);

            }
        }
    }


    public void GetOutOfMakingBatchOrder()
    {
        if (foodItemPrepping != null)
        {
            if (pState == PlayerState.MakingBatchOrder && stepInCurrentOrder < foodItemPrepping.GetPrepStepCount())
            {
                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                Debug.Log("State Changed Worked!");
                pState = PlayerState.None;
            }
        }
    }

    public void ResetFoodImagesAndFoodPrepButtons()
    {
        foreach (Transform child in FoodMenuInventoryManager.Instance.foodPrepImages)
        {
            child.gameObject.SetActive(false);

            foreach (Transform children in child)
            {
                // Deactivate each child GameObject
                children.gameObject.SetActive(false);
            }
        }

        FoodMenuInventoryManager.Instance.foodPrepImages.gameObject.SetActive(false);

        FoodMenuInventoryManager.Instance.foodPrepMenuContent.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

    }

    public void FinishMakingBatchOrder()
    {
        if (foodItemPrepping != null)
        {
            if (pState == PlayerState.MakingBatchOrder && stepInCurrentOrder >= foodItemPrepping.GetPrepStepCount())
            {
                foodSelectMenu.addFoodToCookingClick();
                foodSelectMenu.addToCookingStationSlotClick();

                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                Debug.Log("State Changed Worked!");
                pState = PlayerState.None;
            }
        }
    }

    public void FinishMakingSingleOrder()
    {
        if (foodItemPrepping != null)
        {
            if (pState == PlayerState.MakingSingleOrder && stepInCurrentOrder >= foodItemPrepping.GetPrepStepCount())
            {
                foodTakeoutWindow.giveCustomerSingleOrder();

                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                Debug.Log("State Changed Worked!");
                pState = PlayerState.None;
            }
        }
    }


    public int GetCurrentStepInOrder()
    {
        return stepInCurrentOrder;
    }

    public int incrementCurrentStepInOrder()
    {
        return stepInCurrentOrder++;
    }

    public int decrementCurrentStepInOrder()
    {
        return stepInCurrentOrder--;
    }

    public void SetFoodItemPrepping(FoodItem foodItemPreppingF)
    {
        foodItemPrepping = foodItemPreppingF;
    }

    public void SetFoodSelectMenu(FoodSelectMenu foodSelectMenuF)
    {
        foodSelectMenu = foodSelectMenuF;
    }

    public void SetFoodItemTakeoutWindow(FoodItemTakeoutWindow foodItemTakeoutWindowF)
    {
        foodTakeoutWindow = foodItemTakeoutWindowF;
    }
}
