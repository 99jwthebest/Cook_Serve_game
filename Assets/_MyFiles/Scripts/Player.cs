using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int amountOfIngredientsNeededToBeCorrect;
    public int ingredientCorrect;
    public int foodIngredIndex = 0;
    [SerializeField]
    int stepInCurrentOrder;
    [SerializeField]
    FoodItem foodItemPrepping;
    [SerializeField]
    FoodSelectMenu foodSelectMenu;
    public FoodItemTakeoutWindow foodTakeoutWindow;
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
        SoundEffects.instance.PlayAmbientClip();
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
                SoundEffects.instance.PlayOrderMessedUpSound();
                ClearIngredientArrays();
                foodIngredIndex = 0;
                amountOfIngredientsNeededToBeCorrect = 0;

                //foodIngredients.Clear();
                //foodIngredientsToCheck.Clear();
                CloseAndEmptyFoodOrderDetails();
                EmptyAndTurnOffExtraButtonsOnIngredientTabs();

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
                CloseAndEmptyFoodOrderDetails();
                EmptyAndTurnOffExtraButtonsOnIngredientTabs();


                ChangeIngredientTabs.instance.selectedTab = 0;
                ChangeIngredientTabs.instance.SelectImageTab();
                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                
                CheckIfIngredientsAreCorrect();
                pState = PlayerState.None;
                Debug.Log("State Changed Worked!");

                if (ingredientCorrect < amountOfIngredientsNeededToBeCorrect)
                {
                    ClearIngredientArrays();
                    amountOfIngredientsNeededToBeCorrect = 0;
                    ingredientCorrect = 0;
                    SoundEffects.instance.PlayOrderMessedUpSound();

                    Debug.Log("Need to make order again.");
                    return;

                }

                ingredientCorrect = 0;

                foodSelectMenu.addFoodToCookingClick();
                foodSelectMenu.addToCookingStationSlotClick();
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

                CloseAndEmptyFoodOrderDetails();
                EmptyAndTurnOffExtraButtonsOnIngredientTabs();


                ChangeIngredientTabs.instance.selectedTab = 0;
                ChangeIngredientTabs.instance.SelectImageTab();
                stepInCurrentOrder = 0;
                ResetFoodImagesAndFoodPrepButtons();
                FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
                FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

                FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
                
                CheckIfIngredientsAreCorrect();
                pState = PlayerState.None;
                Debug.Log("State Changed Worked!");

                if (ingredientCorrect < amountOfIngredientsNeededToBeCorrect)
                {
                    ClearIngredientArrays();
                    amountOfIngredientsNeededToBeCorrect = 0;
                    ingredientCorrect = 0;

                    if (FoodMenuInventoryManager.Instance.perfectFoodOrderCombo > FoodMenuInventoryManager.Instance.highestFoodOrderCombo)
                    {
                        FoodMenuInventoryManager.Instance.highestFoodOrderCombo = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo;
                    }

                    FoodMenuInventoryManager.Instance.perfectFoodOrderCombo = 0;
                    FoodMenuInventoryManager.Instance.perfectFoodOrderComboText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();
                    SoundEffects.instance.PlayOrderMessedUpSound();

                    Debug.Log("Need to make order again.");
                    return;

                }

                ingredientCorrect = 0;

                SoundEffects.instance.PlayGaveCustomerFoodSound();
                foodTakeoutWindow.giveCustomerSingleOrder();
            }
        }
    }

    public void FailedMakingSingleOrderFood()
    {
        SoundEffects.instance.PlayOrderMessedUpSound();

        foodIngredIndex = 0;

        CloseAndEmptyFoodOrderDetails();
        EmptyAndTurnOffExtraButtonsOnIngredientTabs();

        ChangeIngredientTabs.instance.selectedTab = 0;
        ChangeIngredientTabs.instance.SelectImageTab();
        stepInCurrentOrder = 0;
        ResetFoodImagesAndFoodPrepButtons();
        FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);

        FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);

        pState = PlayerState.None;
        Debug.Log("State Changed Worked!");

        ClearIngredientArrays();
        amountOfIngredientsNeededToBeCorrect = 0;
        ingredientCorrect = 0;

        if (FoodMenuInventoryManager.Instance.perfectFoodOrderCombo > FoodMenuInventoryManager.Instance.highestFoodOrderCombo)
        {
            FoodMenuInventoryManager.Instance.highestFoodOrderCombo = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo;
        }

        FoodMenuInventoryManager.Instance.perfectFoodOrderCombo = 0;
        FoodMenuInventoryManager.Instance.perfectFoodOrderComboText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();
        FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(false);

        foodTakeoutWindow = null;

        Debug.Log("Customer left while you were making making a single Order, BITCH!!!");

    }

    public void CheckIfIngredientsAreCorrect()
    {

        for(int checkIndex = 0; checkIndex < amountOfIngredientsNeededToBeCorrect; checkIndex++)
        {
            for (int i = 0; i < amountOfIngredientsNeededToBeCorrect; i++)
            {
                if (foodIngredientsToCheckA[checkIndex] == foodIngredientsA[i])
                {
                    ingredientCorrect++;
                    if(ingredientCorrect == amountOfIngredientsNeededToBeCorrect)
                    {
                        ClearIngredientArrays();
                        amountOfIngredientsNeededToBeCorrect = 0;
                        break;
                    }
                }
                else
                {
                    Debug.Log("Ingredient Wrong!!!!!");
                }
            }
        }
    }

    public void ClearIngredientArrays()
    {
        foodIngredientsA[0] = null;
        foodIngredientsA[1] = null;
        foodIngredientsA[2] = null;
        foodIngredientsA[3] = null;
        foodIngredientsA[4] = null;
        foodIngredientsA[5] = null;
        foodIngredientsA[6] = null;
        foodIngredientsA[7] = null;
        foodIngredientsA[8] = null;
        foodIngredientsA[9] = null;
        foodIngredientsA[10] = null;
        foodIngredientsA[11] = null;
        foodIngredientsA[12] = null;
        foodIngredientsA[13] = null;




        foodIngredientsToCheckA[0] = null;
        foodIngredientsToCheckA[1] = null;
        foodIngredientsToCheckA[2] = null;
        foodIngredientsToCheckA[3] = null;
        foodIngredientsToCheckA[4] = null;
        foodIngredientsToCheckA[5] = null;
        foodIngredientsToCheckA[6] = null;
        foodIngredientsToCheckA[7] = null;
        foodIngredientsToCheckA[8] = null;
        foodIngredientsToCheckA[9] = null;
        foodIngredientsToCheckA[10] = null;
        foodIngredientsToCheckA[11] = null;
        foodIngredientsToCheckA[12] = null;
        foodIngredientsToCheckA[13] = null;

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

    public void EmptyAndTurnOffExtraButtonsOnIngredientTabs()
    {
        for (int stepIndex = 0; stepIndex < 3; stepIndex++)
        {
            Transform foodPrepMenuContent = null;

            if (stepIndex == 0)
            {
                foodPrepMenuContent = FoodMenuInventoryManager.Instance.foodPrepMenuContent;
            }
            else if (stepIndex == 1)
            {
                foodPrepMenuContent = FoodMenuInventoryManager.Instance.foodPrepMenuContent1;
            }
            else if (stepIndex == 2)
            {
                foodPrepMenuContent = FoodMenuInventoryManager.Instance.foodPrepMenuContent2;
            }

            if (foodPrepMenuContent == null)
            {
                Debug.Log("foodPrepMenuContent for step " + stepIndex + " is not available.");
                return;
            }

            foreach (Transform child in foodPrepMenuContent)
            {
                FoodSelectMenu foodSelectMenu = child.GetComponentInChildren<FoodSelectMenu>();

                child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                foodSelectMenu.foodItem = null;
                foodSelectMenu.foodIngredients = null;
                child.transform.gameObject.SetActive(false);
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
    public FoodItemTakeoutWindow GetFoodItemTakeoutWindow()
    {
        return foodTakeoutWindow;
    }

    public void SetFoodOrderDetailsSlot(FoodOrderDetailsSlot foodOrderDetailsSlotF)
    {
        foodOrderDetailsSlot = foodOrderDetailsSlotF;
    }
}
