using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
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
    [SerializeField]
    FoodOrderDetailsSlot foodOrderDetailsSlot;
    public List<FoodIngredients> foodIngredients = new List<FoodIngredients>();
    public List<FoodIngredients> foodIngredientsToCheck = new List<FoodIngredients>();
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

        if(pState == PlayerState.None)
        {

        }
    }


    public void GetOutOfMakingBatchOrder()
    {
        if (foodItemPrepping != null)
        {
            if (pState == PlayerState.MakingBatchOrder && stepInCurrentOrder < foodItemPrepping.GetPrepStepCount())
            {
                foodIngredientsToCheckA[0] = null;
                foodIngredientsToCheckA[1] = null;
                foodIngredientsToCheckA[2] = null;

                foodIngredientsToCheckA[3] = null;
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
                //CheckIfIngredientsAreCorrect();
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
                //CheckIfIngredientsAreCorrect();
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

        foodIngredients.RemoveAll(x => x != null);
        foodIngredients.Clear();
        foodIngredientsToCheck.Clear();

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

    public void AddIngredientToList(FoodIngredients foodIngredient)
    {
        foodIngredients.Add(foodIngredient);
    }

    public void Remove(FoodIngredients foodIngredient)
    {
        foodIngredients.Remove(foodIngredient);
    }

    public void AddIngredientToListCheck(FoodIngredients foodIngredient)
    {
        foodIngredientsToCheck.Add(foodIngredient);
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
