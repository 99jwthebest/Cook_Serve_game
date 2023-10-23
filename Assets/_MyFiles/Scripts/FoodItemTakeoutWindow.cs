using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FoodItemTakeoutWindow : MonoBehaviour
{
    public FoodItem foodItem;
    public TextMeshProUGUI foodItemNameTW;
    public Image foodItemIconTW;
    public TextMeshProUGUI foodItemTypeText;
    public bool foodWaiting;
    public float foodWaitingTime = 15f;
    float foodStartWaitingTime;
    public int takeOutWindowID;

    [Header("Progress Bar Properties")]
    public float maximum;
    public float current;
    public float start;
    public Image mask;


    // Start is called before the first frame update
    void Start()
    {
        foodStartWaitingTime = foodWaitingTime;
        maximum = foodWaitingTime * 10;
        start = maximum;
        current = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (foodItem != null)
        {
            current -= 10 * Time.deltaTime;
            foodWaitingTime -= 1 * Time.deltaTime;
            Debug.Log("Customer is WAITING!!!");

        }
        else
        {
            foodWaitingTime = foodStartWaitingTime;
            current = start;
        }

        if (foodWaitingTime <= 0)
        {
            foodWaitingTime = foodStartWaitingTime;
            current = start;
            customerLeaves();
        }

        GetCurrentFill();

        //if(foodItem.foodItemType == FoodItem.FoodItemType.Single)
        //{

        //}   
    }
    public void GetCurrentFill()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }

    public void customerLeaves()
    {
        foodItem = null;
        foodItemNameTW.text = null;
        foodItemIconTW.sprite = null;
        foodItemTypeText.text = null;
        foodWaiting = false;
        FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining--;
        FoodMenuInventoryManager.Instance.totalAmountOfCustomersText.text = FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining.ToString();
        FoodMenuInventoryManager.Instance.takeoutWindowsFilled--;


        if (Player.instance.foodTakeoutWindow != null && 
            Player.instance.pState == Player.PlayerState.MakingSingleOrder && 
            Player.instance.GetFoodItemTakeoutWindow().foodItem == null)
        {
            Player.instance.FailedMakingSingleOrderFood();

            Debug.Log("CALLING FROM BEYOND FMOTEHR TRUCKER!!!!");
        }


        gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.amountOfStrikes++;
        FoodMenuInventoryManager.Instance.amountOfStrikesText.text = FoodMenuInventoryManager.Instance.amountOfStrikes.ToString();

        if(FoodMenuInventoryManager.Instance.perfectFoodOrderCombo > FoodMenuInventoryManager.Instance.highestFoodOrderCombo)
        {
            FoodMenuInventoryManager.Instance.highestFoodOrderCombo = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo;
        }

        FoodMenuInventoryManager.Instance.perfectFoodOrderCombo = 0;
        FoodMenuInventoryManager.Instance.perfectFoodOrderComboText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();

        Debug.Log("Customer left, Asshole!!!");

    }

    public void checkIfFoodIsReady()
    {
        if (foodItem.foodItemType == FoodItem.FoodItemType.Batch)
        {
            foreach (Transform cookingStationSTrans in FoodMenuInventoryManager.Instance.foodLoadoutContent)
            {
                CookingStationSlot cookingStationSlot = cookingStationSTrans.GetComponentInChildren<CookingStationSlot>();

                if (cookingStationSlot.foodItem != null && foodItem.id == cookingStationSlot.foodItem.id)
                {
                    if (cookingStationSlot.howMuchFoodIsLeft > 0)
                    {

                        cookingStationSlot.howMuchFoodIsLeft--;
                        cookingStationSlot.howMuchFoodIsLeftText.text = cookingStationSlot.howMuchFoodIsLeft.ToString();

                        if (cookingStationSlot.howMuchFoodIsLeft <= 0)
                        {
                            cookingStationSlot.foodItem = null;
                            cookingStationSlot.foodItemNameText.text = null;
                            cookingStationSlot.foodItemImage.sprite = null;
                            //cookingStationSlot.hasFood = false;

                            Debug.Log("Food from cooking station goners!!!");
                            //return;
                        }

                        foodItem = null;
                        foodItemNameTW.text = null;
                        foodItemIconTW.sprite = null;
                        foodItemTypeText.text = null;
                        foodWaiting = false;
                        FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining--;
                        FoodMenuInventoryManager.Instance.totalAmountOfCustomersText.text = FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining.ToString();
                        FoodMenuInventoryManager.Instance.takeoutWindowsFilled--;

                        FoodMenuInventoryManager.Instance.perfectFoodOrderCombo++;
                        FoodMenuInventoryManager.Instance.perfectFoodOrderComboText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();

                        foodWaitingTime = foodStartWaitingTime;
                        current = start;
                        SoundEffects.instance.PlayGaveCustomerFoodSound();

                        gameObject.SetActive(false);
                        Debug.Log("we succedded on giving food to CUSTOMER!!!");
                        return;
                    }
                    else
                    {
                        Debug.Log("You don't have food ready!! dumbo!!!");
                        break;
                    }
                }
                else
                {
                    Debug.Log("Not the correct station!!!");
                }
            }
            Debug.Log("Could not find a station with food!!!");
        }
        else if (foodItem.foodItemType == FoodItem.FoodItemType.Single)
        {
            turnOnFoodPrepButtons();
            addFoodInfoToPrepButtons();
            turnOnFoodPrepImages();
            turnOffFoodPrepButtons();
            changePlayerState();
            turnOnFoodOrderDetails();
            RandomFirstIngredient();
            RandomIngredientsForOrder();
            SetIngredientsForNotRandomOrder();
        }
        else
        {
            Debug.Log("What the fuck did you do??!!!");
        }
    }

    public void turnOnFoodPrepButtons()
    {
        foodWaitingTime = 30f;
        current = foodWaitingTime * 10f;

        FoodMenuInventoryManager.Instance.takeOutWindowIDReceive = takeOutWindowID;
        FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(true);
    }

    public void turnOffFoodPrepButtons()
    {
        //FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(false);
    }

    public void turnOnFoodOrderDetails()
    {
        FoodMenuInventoryManager.Instance.foodOrderDetailsT.gameObject.SetActive(true);
    }


    public void addFoodInfoToPrepButtons()
    {
        int ingredIndex = 0;

        for (int stepIndex = 0; stepIndex < foodItem.GetAmountOfFoodSteps(); stepIndex++)
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
                foodSelectMenu.foodItem = foodItem;

                FoodStep[] foodSteps = foodSelectMenu.foodItem.GetFoodSteps();

                if (foodSteps != null && foodSteps.Length > 0 && ingredIndex < foodSelectMenu.foodItem.GetAmountOfFoodIngredients(stepIndex))
                {
                    FoodIngredients[] ingredients = foodSteps[stepIndex].Ingredients;
                    foodSelectMenu.foodIngredients = ingredients[ingredIndex];
                    foodSelectMenu.foodItemButtonID = ingredients[ingredIndex].ingredientID;
                    foodSelectMenu.foodItemNameSM.text = ingredients[ingredIndex].ingredientName;
                    foodSelectMenu.foodItemIconSM.sprite = ingredients[ingredIndex].foodIngredientIcon;

                    if (foodSelectMenu.foodItemButtonID == 0)
                    {
                        child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                        child.transform.gameObject.SetActive(true);
                    }
                    else
                    {
                        child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                        child.transform.gameObject.SetActive(true);
                    }

                    ingredIndex++;
                }
                else
                {
                    Debug.Log("Foodsteps is null or foodSteps do not have a length.");
                }
            }
        }

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
                if (foodSelectMenu.foodIngredients == null)
                {
                    child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    foodSelectMenu.foodItem = null;
                    child.transform.gameObject.SetActive(false);
                }
            }
        }


        Player.instance.SetFoodItemPrepping(foodItem);
        Player.instance.SetFoodItemTakeoutWindow(this);
    }

    public void turnOnFoodPrepImages()
    {
        FoodMenuInventoryManager.Instance.foodPrepImages.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).gameObject.SetActive(true); // change index of 0 to foodItem.Id
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }



    public void RandomFirstIngredient()
    {
        int firstIngredientCount = foodItem.firstIngredientCount;

        if (firstIngredientCount > 1)
        {
            int randomFirstIngredient = Random.Range(0, firstIngredientCount);

            Transform foodOrderDetailsSlotChild = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.GetChild(0);
            foodOrderDetailsSlotChild.gameObject.SetActive(true);
            FoodOrderDetailsSlot foodOrderDetailsSlot = foodOrderDetailsSlotChild.GetComponent<FoodOrderDetailsSlot>();

            if (foodOrderDetailsSlot.foodItem == null)
            {
                foodOrderDetailsSlot.foodItem = foodItem;
                FoodStep[] foodSteps = foodOrderDetailsSlot.foodItem.GetFoodSteps();

                if (foodSteps != null && foodSteps.Length > 0)
                {
                    FoodIngredients[] ingredients = foodSteps[0].Ingredients;
                    Player.instance.foodIngredientsToCheckA[0] = ingredients[randomFirstIngredient];
                    foodOrderDetailsSlot.foodIngredientID = ingredients[randomFirstIngredient].ingredientID;
                    foodOrderDetailsSlot.foodIngredientName.text = ingredients[randomFirstIngredient].ingredientName;
                    foodOrderDetailsSlot.foodImageIngredientColor.sprite = ingredients[randomFirstIngredient].foodIngredientIcon;

                    Player.instance.amountOfIngredientsNeededToBeCorrect++;
                    Debug.Log("Food has MORE than one first Ingredient");
                }
                else
                {
                    Debug.Log("Foodsteps is null or foodSteps do not have a length.");
                }
            }
        }
        else
        {
            Transform foodOrderDetailsSlotChild = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.GetChild(0);
            foodOrderDetailsSlotChild.gameObject.SetActive(true);
            FoodOrderDetailsSlot foodOrderDetailsSlot = foodOrderDetailsSlotChild.GetComponent<FoodOrderDetailsSlot>();

            if (foodOrderDetailsSlot.foodItem == null)
            {
                foodOrderDetailsSlot.foodItem = foodItem;
                FoodStep[] foodSteps = foodOrderDetailsSlot.foodItem.GetFoodSteps();

                if (foodSteps != null && foodSteps.Length > 0)
                {
                    FoodIngredients[] ingredients = foodSteps[0].Ingredients;
                    Player.instance.foodIngredientsToCheckA[0] = ingredients[0];
                    foodOrderDetailsSlot.foodIngredientID = ingredients[0].ingredientID;
                    foodOrderDetailsSlot.foodIngredientName.text = ingredients[0].ingredientName;
                    foodOrderDetailsSlot.foodImageIngredientColor.sprite = ingredients[0].foodIngredientIcon;

                    Player.instance.amountOfIngredientsNeededToBeCorrect++;
                    Debug.Log("Food Has ONE First Ingredient");
                }
                else
                {
                    Debug.Log("Foodsteps is null or foodSteps do not have a length.");
                }
            }
        }

    }


    public void RandomIngredientsForOrder()
    {
        if (!foodItem.notRandomIngredients)
        {

            // int foodOrderDetailsSlotTransCount = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.childCount;
            turnOnFoodOrderDetails();

            int currentTransform = 1;

            for (int stepIndex = 0; stepIndex < foodItem.GetPrepStepCount() - 1; stepIndex++)
            {

                int randomAmountOfIngredientNeededPerTab = Random.Range(1, foodItem.randomAmountOfIngredientNeededPerTab + 1);

                for (int j = 0; j < randomAmountOfIngredientNeededPerTab; j++)
                {
                    Transform foodOrderDetailsSlotChild = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.GetChild(currentTransform);
                    foodOrderDetailsSlotChild.gameObject.SetActive(true);
                    FoodOrderDetailsSlot foodOrderDetailsSlot = foodOrderDetailsSlotChild.GetComponent<FoodOrderDetailsSlot>();

                    int randomIngredientNum = Random.Range(foodItem.firstIngredientCount, foodItem.GetAmountOfFoodIngredients(stepIndex));

                    //int ingredIndex = 0;


                    if (foodOrderDetailsSlot.foodItem == null)
                    {
                        foodOrderDetailsSlot.foodItem = foodItem;
                        FoodStep[] foodSteps = foodOrderDetailsSlot.foodItem.GetFoodSteps();

                        if (foodSteps != null && foodSteps.Length > 0)
                        {
                            FoodIngredients[] ingredients = foodSteps[stepIndex].Ingredients;
                            Player.instance.foodIngredientsToCheckA[currentTransform] = ingredients[randomIngredientNum];

                            foodOrderDetailsSlot.foodIngredientID = ingredients[randomIngredientNum].ingredientID;
                            foodOrderDetailsSlot.foodIngredientName.text = ingredients[randomIngredientNum].ingredientName;
                            foodOrderDetailsSlot.foodImageIngredientColor.sprite = ingredients[randomIngredientNum].foodIngredientIcon;

                            currentTransform++;
                            Player.instance.amountOfIngredientsNeededToBeCorrect++;
                            //ingredIndex++;
                        }
                        else
                        {
                            Debug.Log("Foodsteps is null or foodSteps do not have a length.");
                        }
                    }
                    else
                    {
                        Debug.Log("foodOrderDetailSlot is full!");
                        continue;
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

    public void SetIngredientsForNotRandomOrder()
    {
        if (foodItem.notRandomIngredients)
        {
            // int foodOrderDetailsSlotTransCount = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.childCount;
            turnOnFoodOrderDetails();

            int currentTransform = 1;
            int ingredIndex = 1;

            for (int stepIndex = 0; stepIndex < foodItem.GetPrepStepCount() - 1; stepIndex++)
            {

                int randomAmountOfIngredientNeededPerTab = foodItem.randomAmountOfIngredientNeededPerTab;

                for (int j = 0; j < randomAmountOfIngredientNeededPerTab; j++)
                {
                    Transform foodOrderDetailsSlotChild = FoodMenuInventoryManager.Instance.foodOrderDetailsSlotTrans.GetChild(currentTransform);
                    foodOrderDetailsSlotChild.gameObject.SetActive(true);
                    FoodOrderDetailsSlot foodOrderDetailsSlot = foodOrderDetailsSlotChild.GetComponent<FoodOrderDetailsSlot>();



                    if (foodOrderDetailsSlot.foodItem == null)
                    {
                        foodOrderDetailsSlot.foodItem = foodItem;
                        FoodStep[] foodSteps = foodOrderDetailsSlot.foodItem.GetFoodSteps();

                        if (foodSteps != null && foodSteps.Length > 0)
                        {
                            FoodIngredients[] ingredients = foodSteps[0].Ingredients;
                            Player.instance.foodIngredientsToCheckA[currentTransform] = ingredients[ingredIndex];

                            foodOrderDetailsSlot.foodIngredientID = ingredients[ingredIndex].ingredientID;
                            foodOrderDetailsSlot.foodIngredientName.text = ingredients[ingredIndex].ingredientName;
                            foodOrderDetailsSlot.foodImageIngredientColor.sprite = ingredients[ingredIndex].foodIngredientIcon;

                            currentTransform++;
                            Player.instance.amountOfIngredientsNeededToBeCorrect++;
                            ingredIndex++;
                        }
                        else
                        {
                            Debug.Log("Foodsteps is null or foodSteps do not have a length.");
                        }
                    }
                    else
                    {
                        Debug.Log("foodOrderDetailSlot is full!");
                        continue;
                    }
                }
            }
        }
    }


    public void giveCustomerSingleOrder()
    {
        FoodItemTakeoutWindow foodItemTakeoutWindow = FoodMenuInventoryManager.Instance.takeoutOrderWindowContent.GetChild(FoodMenuInventoryManager.Instance.takeOutWindowIDReceive).GetComponentInChildren<FoodItemTakeoutWindow>();
        foodItemTakeoutWindow.foodItem = null;
        foodItemTakeoutWindow.foodItemNameTW.text = null;
        foodItemTakeoutWindow.foodItemIconTW.sprite = null;
        foodItemTakeoutWindow.foodItemTypeText.text = null;
        foodItemTakeoutWindow.foodWaiting = false;
        FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining--;
        FoodMenuInventoryManager.Instance.totalAmountOfCustomersText.text = FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining.ToString();
        FoodMenuInventoryManager.Instance.takeoutWindowsFilled--;

        FoodMenuInventoryManager.Instance.perfectFoodOrderCombo++;
        FoodMenuInventoryManager.Instance.perfectFoodOrderComboText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();
        foodItemTakeoutWindow.foodWaitingTime = foodStartWaitingTime;
        foodItemTakeoutWindow.current = start;
        changePlayerStateToNone();
        foodItemTakeoutWindow.gameObject.SetActive(false);
        Debug.Log("We Gave out a SINGLE ORDER to CUSTOMER!!!");
    }

    public void changePlayerState()
    {
        Player.instance.pState = Player.PlayerState.MakingSingleOrder;
        Player.instance.SetFoodItemPrepping(foodItem);
    }

    public void changePlayerStateToNone()
    {
        Player.instance.pState = Player.PlayerState.None;
    }
}

