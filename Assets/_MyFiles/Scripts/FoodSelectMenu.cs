using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FoodSelectMenu : MonoBehaviour
{
    public FoodItem foodItem;
    public FoodIngredients foodIngredients;
    public TextMeshProUGUI foodItemNameSM;
    public Image foodItemIconSM;
    public TextMeshProUGUI amountOfFoodThatCanBeMade;
    public TextMeshProUGUI foodNeedToCookText;
    public int foodNeedToCookAmount;
    public TextMeshProUGUI foodCookingText;
    public int foodCookingCount;
    public int currentSpawnedFoodAmount;
    public int foodItemButtonID;
    public GameObject fadePrepButtonImage;



    //public bool equippedFoodItem;
    //public GameObject foodInventory;
    //public GameObject foodSelectMenu;
    //public Toggle foodSlotToggle;

    //public GameObject fadeInventoryImage;



    //void Awake()
    //{
    //    foodInventory = GameObject.FindGameObjectWithTag("FoodInventory");
    //    //fadeInventoryImage = GameObject.FindGameObjectWithTag("FadeInventoryImage");
    //    foodSlotToggle = gameObject.GetComponent<Toggle>();
    //    foodSelectMenu = GameObject.FindGameObjectWithTag("FoodSelectMenu");
    //}

    //void Start()
    //{
    //    foodInventory.SetActive(false);
    //    foodSelectMenu.SetActive(false);
    //}

    //public void RemoveFoodItem()
    //{
    //    //InventoryManager.Instance.Remove(foodItem);
    //    //InventoryManager.Instance.FoodItems.Remove(foodItem);

    //    Destroy(gameObject);
    //}

    //public void AddItem(FoodItem newFoodItem)
    //{
    //    foodItem = newFoodItem;

    //    Debug.Log("Hello, is this working" + foodItem);
    //    //equippedFoodItem = true;
    //    FoodMenuInventoryManager.Instance.foodSlotsFilled += 1;
    //    gameObject.GetComponent<Toggle>().isOn = false;

    //}

    //public void SetFoodButtonID()
    //{
    //    FoodMenuInventoryManager.Instance.foodButtonIdReceive = foodItemButtonID;
    //    //fadeInventoryImage.SetActive(false);
    //    foodSlotToggleF();
    //}

    //public void foodSlotToggleF()
    //{
    //    if (foodSlotToggle.isOn || FoodMenuInventoryManager.Instance.autoSelect)
    //    {
    //        transform.parent.GetComponent<Image>().enabled = true;

    //        if (foodItem == null)
    //        {
    //            FoodMenuInventoryManager.Instance.turnOffOtherToggles();
    //        }
    //        if (foodItem != null)
    //        {
    //            FoodMenuInventoryManager.Instance.turnOffAllToggles();
    //            //fadeInventoryImage.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        transform.parent.GetComponent<Image>().enabled = false;
    //    }
    //}


    //public void Equip_Unequip_FoodSlot()
    //{
    //    if (foodItem == null)
    //    {
    //        FoodMenuInventoryManager.Instance.inventoryItemController = this.GetComponent<InventoryItemController>();
    //        Debug.Log("Is it going into unequipped??!?!");

    //    }
    //    else if (foodItem != null)  // this is why it's not working, something with the stupid toggles, damn it!!!!!
    //    {
    //        UseItem();
    //        FoodMenuInventoryManager.Instance.RemoveFoodItemSlot();

    //        FoodMenuInventoryManager.Instance.foodButtonIdPrevious = foodItem.id;

    //        FoodMenuInventoryManager.Instance.AddFoodBackToInventory();

    //        foodItem = null;
    //        //fadeInventoryImage.SetActive(true);
    //        FoodMenuInventoryManager.Instance.foodSlotsFilled -= 1;
    //        Debug.Log("why the fuck is it going in here??!?!");
    //    }
    //}
    //public Toggle GetFoodSlotToggle()
    //{
    //    return foodSlotToggle;
    //}

    //public int GetFoodButtodSlotID()
    //{
    //    return foodItemButtonID;
    //}


    //public void UseItem()
    //{
    //    switch (foodItem.foodItemType)
    //    {
    //        case FoodItem.FoodItemType.Meat:
    //            Player.instance.addToFoodCooking(foodItem.foodToBeMade);
    //            break;
    //        case FoodItem.FoodItemType.Pastry:
    //            Player.instance.addToFoodCooking(foodItem.foodToBeMade);
    //            break;
    //        default:
    //            break;
    //    }
    //    Debug.Log(foodItem);

    //    //RemoveItem();
    //}


    //public void fillFoodSelectMenu()
    //{
    //    foreach (Transform foodItemSlot in foodLoadoutContent)
    //    {
    //       // foodItemButton = foodItemSlot.gameObject.GetComponentInChildren<Toggle>();
    //        //foodItemButton = inventoryItemController.GetFoodSlotToggle();
    //        inventoryItemController = foodItemSlot.gameObject.GetComponentInChildren<InventoryItemController>();
    //        //foodItemButton.isOn = false;
    //        //inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
    //        foreach (Transform foodMenuItem in menuFoodInventory)
    //        {
    //            // foodItemButton = foodItemSlot.gameObject.GetComponentInChildren<Toggle>();
    //            //foodItemButton = inventoryItemController.GetFoodSlotToggle();
    //            cookingStationSlot = foodMenuItem.gameObject.GetComponentInChildren<CookingStationSlot>();
    //            //foodItemButton.isOn = false;

    //            cookingStationSlot.foodItem = inventoryItemController.foodItem;
    //            //inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
    //        }
    //    }
    //}


    public void addFoodInfoToPrepButtons()
    {
        SoundEffects.instance.PlayCookingStationClickSound();


        for (int stepIndex = 0; stepIndex < foodItem.GetAmountOfFoodSteps(); stepIndex++)
        {
            int ingredIndex = 0;
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
        Player.instance.SetFoodSelectMenu(this);
    }

    public void turnOnFoodPrepImages()
    {
        FoodMenuInventoryManager.Instance.foodPrepImages.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).gameObject.SetActive(true); // change index of 0 to foodItem.Id
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        changePlayerState();
    }

    public void turnOnFoodPrepButtons()
    {
        FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent1.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodPrepMenuContent2.gameObject.SetActive(true);
        
        FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(false); 
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

    public void RandomFirstIngredient()
    {
        int firstIngredientCount = foodItem.firstIngredientCount;

        if(firstIngredientCount > 1)
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


    public void prepButtonFunctionality()
    {
        SoundEffects.instance.PlayCookingStationClickSound();

        //if(Player.instance.GetCurrentStepInOrder() == 0)
        Player.instance.incrementCurrentStepInOrder();

        //if(Player.instance.GetCurrentStepInOrder() == 1)
        //{
        //    foodItem.randomAmountOfIngredientNeededPerTab
        //}


        Player.instance.foodIngredientsA[Player.instance.foodIngredIndex] = foodIngredients;
        Player.instance.foodIngredIndex++;

        fadePrepButtonImage.gameObject.SetActive(true);

        if (Player.instance.GetCurrentStepInOrder() == 1)
        {
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
                    if (foodSelectMenu.foodItemButtonID == 0)
                    {
                        child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (Player.instance.GetCurrentStepInOrder() >= foodItem.GetPrepStepCount() && 
                foodItem.GetPrepStepCount() <= 2 &&
                foodItem.randomAmountOfIngredientNeededPerTab <= 1)
        {
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
                    child.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    public void changePlayerState()
    {
        Player.instance.pState = Player.PlayerState.MakingBatchOrder;
    }

    public void addFoodToCookingClick()
    {
        addToFoodCooking(foodItem.foodToBeMade);
    }

    public void addToFoodCooking(int amountOfFood)
    {
        Transform foodMenuItem = FoodMenuInventoryManager.Instance.FoodInventoryContent.GetChild(foodItemButtonID);

        FoodSelectMenu foodSelectMenu = foodMenuItem.GetComponentInChildren<FoodSelectMenu>();


        foodSelectMenu.foodCookingCount += amountOfFood;
        foodSelectMenu.foodCookingText.text = foodSelectMenu.foodCookingCount.ToString();
    }

    public void addToCookingStationSlotClick()
    {
        addToCookingStationSlot(foodItem.foodToBeMade);
    }

    public void addToCookingStationSlot(int amountOfFood)
    {
        Transform foodMenuItem = FoodMenuInventoryManager.Instance.foodLoadoutContent.GetChild(FoodMenuInventoryManager.Instance.foodButtonIDCSReceive);

        CookingStationSlot cookingStationSlotI = foodMenuItem.GetComponentInChildren<CookingStationSlot>();

        cookingStationSlotI.startAddTocookingCorotine(amountOfFood, cookingStationSlotI, this);

    }

    public void closeInventory()
    {
        FoodMenuInventoryManager.Instance.menuFoodInventory.gameObject.SetActive(false);
        FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(false);
    }


    //public void addToFoodNeeded(int amountOfFood)
    //{
    //    foodNeeded += amountOfFood;
    //    foodNeededText.text = $"Food Needed: {foodNeeded}";
    //}

}