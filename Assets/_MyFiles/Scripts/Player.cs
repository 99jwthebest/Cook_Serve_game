using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int foodIngredIndex = 0;
    [SerializeField]
    int stepInCurrentOrder;
    [SerializeField]
    FoodItem foodItemPrepping;
    [SerializeField]
    FoodSelectMenu foodSelectMenu;
    [SerializeField]
    FoodItemTakeoutWindow foodTakeoutWindow;
    [SerializeField]
    FoodOrderDetailsSlot foodOrderDetailsSlot;
    public FoodIngredients[] foodIngredientsA;
    public FoodIngredients[] foodIngredientsToCheckA;



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
                ClearIngredientArrays();
                foodIngredIndex = 0;

                //foodIngredients.Clear();
                //foodIngredientsToCheck.Clear();
                CloseAndEmptyFoodOrderDetails();

                ChangeIngredientTabs.instance.selectedTab = 0;
                ChangeIngredientTabs.instance.SelectImageTab();

                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

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
                foodIngredIndex = 0;
                CheckIfIngredientsAreCorrect();
                CloseAndEmptyFoodOrderDetails();

                foodSelectMenu.addFoodToCookingClick();
                foodSelectMenu.addToCookingStationSlotClick();

                ChangeIngredientTabs.instance.selectedTab = 0;
                ChangeIngredientTabs.instance.SelectImageTab();
                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

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
                foodIngredIndex = 0;

                CheckIfIngredientsAreCorrect();
                CloseAndEmptyFoodOrderDetails();

                foodTakeoutWindow.giveCustomerSingleOrder();

                ChangeIngredientTabs.instance.selectedTab = 0;
                ChangeIngredientTabs.instance.SelectImageTab();
                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                Debug.Log("State Changed Worked!");
                pState = PlayerState.None;
            }
        }
    }

    public void CheckIfIngredientsAreCorrect()
    {

        ClearIngredientArrays();

    }

    public void ClearIngredientArrays()
    {
        foodIngredientsA[0] = null;
        foodIngredientsA[1] = null;
        foodIngredientsA[2] = null;
        foodIngredientsA[3] = null;
        foodIngredientsA[4] = null;
        foodIngredientsA[5] = null;



        foodIngredientsToCheckA[0] = null;
        foodIngredientsToCheckA[1] = null;
        foodIngredientsToCheckA[2] = null;
        foodIngredientsToCheckA[3] = null;
        foodIngredientsToCheckA[4] = null;
        foodIngredientsToCheckA[5] = null;
    }

    public void CloseAndEmptyFoodOrderDetails()
    {
        foreach(Transform child in FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans)
        {
            FoodOrderDetailsSlot foodOrderDetailsSlot = child.GetComponent<FoodOrderDetailsSlot>();

            foodOrderDetailsSlot.foodItem = null;
            foodOrderDetailsSlot.foodIngredientName.text = null;
            foodOrderDetailsSlot.foodImageIngredientColor.sprite = null;

            foodOrderDetailsSlot.gameObject.SetActive(false);
        }

        FoodMenuInventoryManager.Instance.foodOrderDetailsT.gameObject.SetActive(false);
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

    public void SetFoodOrderDetailsSlot(FoodOrderDetailsSlot foodOrderDetailsSlotF)
    {
        foodOrderDetailsSlot = foodOrderDetailsSlotF;
    }
}
