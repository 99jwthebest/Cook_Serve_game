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
    public float foodWaitingTime = 7f;
    public int takeOutWindowID;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (foodItem != null)
        {
            foodWaitingTime -= 1 * Time.deltaTime;
            Debug.Log("Customer is WAITING!!!");
        }
        else
        {
            foodWaitingTime = 7f;
        }

        if (foodWaitingTime <= 0)
        {
            foodWaitingTime = 7f;
            customerLeaves();
        }
        //if(foodItem.foodItemType == FoodItem.FoodItemType.Single)
        //{

        //}   
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


        gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.amountOfStrikes++;
        FoodMenuInventoryManager.Instance.amountOfStrikesText.text = FoodMenuInventoryManager.Instance.amountOfStrikes.ToString();
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
                        foodWaitingTime = 7f;

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
            changePlayerState();
        }
        else
        {
            Debug.Log("What the fuck did you do??!!!");
        }
    }

    public void turnOnFoodPrepButtons()
    {
        foodWaitingTime = 30f;
        FoodMenuInventoryManager.Instance.takeOutWindowIDReceive = takeOutWindowID;
        FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(true);
    }

    public void addFoodInfoToPrepButtons()
    {
        int ingredIndex = 0;

        foreach (Transform child in FoodMenuInventoryManager.Instance.foodPrepMenuContent)
        {
            FoodSelectMenu foodSelectMenu = child.GetComponentInChildren<FoodSelectMenu>();

            foodSelectMenu.foodItem = foodItem;

            //for(int i = 0; i)
            FoodStep[] foodSteps = foodSelectMenu.foodItem.GetFoodSteps();

            if (foodSteps != null && foodSteps.Length > 0 && ingredIndex < foodSelectMenu.foodItem.GetAmountOfFoodIngredients())
            {

                FoodIngredients[] ingredients = foodSteps[0].Ingredients;
                foodSelectMenu.foodItemButtonID = ingredients[ingredIndex].ingredientID;
                foodSelectMenu.foodItemNameSM.text = ingredients[ingredIndex].ingredientName;
                foodSelectMenu.foodItemIconSM.sprite = ingredients[ingredIndex].foodIngredientIcon;

                if (foodSelectMenu.foodItemButtonID == 0)
                {
                    child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                }

                ingredIndex++;
            }
            else
            {
                Debug.Log("Foodsteps is null or foodSteps does not have a length");
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
        foodItemTakeoutWindow.foodWaitingTime = 7f;
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

