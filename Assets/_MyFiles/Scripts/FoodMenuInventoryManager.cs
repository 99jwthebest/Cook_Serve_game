using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FoodMenuInventoryManager : MonoBehaviour
{
    public static FoodMenuInventoryManager Instance;
    public List<FoodItem> FoodItems = new List<FoodItem>();

    public Transform FoodInventoryContent;
    public Transform foodLoadoutContent;
    public int foodInventoryCount;
    public GameObject InventoryFoodItem;

    public Toggle EnableRemove;

    //public InventoryItemController[] InventoryFoodItems;
    public InventoryItemController inventoryItemController;
    public int foodButtonIdReceive;
    public int foodButtonIdPrevious;
    public FoodItem foodItemIM;

    public TextMeshProUGUI foodItemName;
    public Image foodItemIcon;
    public int foodNumIdIM;
    public Toggle foodItemButton;
    public bool autoSelect;
    public int foodSlotsFilled;
    public int amountOfFoodNeedToCook;
    public TextMeshProUGUI foodNeededToCookText;
    public int foodButtonIDCSReceive;

    public Transform cookingStationSlots;
    public Transform menuFoodInventory;
    public Transform foodSelectMenu;
    public Transform foodSelectMenuContent;
    public Transform foodPrepMenuContent;
    public Transform foodPrepMenuContent1;
    public Transform foodPrepMenuContent2;
    public Transform foodPrepImages;

    public Transform takeoutOrderWindowContent;
    public Transform foodOrderWindows;
    public int takeOutWindowIDReceive;

    public int totalAmountOfCustomers;
    public int totalAmountOfCustomersPerRound;
    public TextMeshProUGUI totalAmountOfCustomersText;
    public int totalAmountOfCustomersPerRoundRemaining;

    public int foodBatchCount;
    public int foodSingleCount;
    public int totalAmountOfBatchOrders;
    public int totalAmountOfSingleOrders;
    public int amountOfSingleOrdersSpawned;
    public int totalAmountOfRandomOrders;
    public int takeoutOrderWindowCount;
    public int takeoutWindowsFilled;
    public int totalAmountOfOrdersSpawned;

    public int windowTakeoutOrderNum;
    Transform takeoutOrderWindowSlot;
    FoodItemTakeoutWindow foodItemTakeoutWindow;
    public int amountOfStrikes;
    public TextMeshProUGUI amountOfStrikesText;
    public int perfectFoodOrderCombo;
    public TextMeshProUGUI perfectFoodOrderComboText;
    public int highestFoodOrderCombo;
    public TextMeshProUGUI highestFoodOrderComboText;

    public Transform changingTabsIngredients;
    public Transform changingImageForTabIngredients;
    public Transform foodOrderDetailsT;
    public Transform foodOrderDetailsSlotTrans;


    private void Awake()
    {
        Instance = this;

        foodInventoryCount = FoodInventoryContent.childCount;

        //takeoutOrderWindowContent.GetChild(0)
        foodOrderWindows.gameObject.SetActive(true);
        //populateTakeoutWindows();

        totalAmountOfCustomersText.text = totalAmountOfCustomersPerRound.ToString();
        totalAmountOfSingleOrders = totalAmountOfCustomersPerRound;
        float tempNumber = (float)totalAmountOfSingleOrders / 100;
        totalAmountOfSingleOrders = Mathf.FloorToInt(tempNumber * 20);

        totalAmountOfBatchOrders = totalAmountOfCustomersPerRound - totalAmountOfSingleOrders;

        takeoutOrderWindowCount = takeoutOrderWindowContent.childCount;

        totalAmountOfCustomersPerRoundRemaining = totalAmountOfCustomersPerRound;

        foodBatchCount = InventoryManager.Instance.foodButtonsBatchCount;
    }

    private void Update()
    {
        if(perfectFoodOrderCombo > highestFoodOrderCombo)
        {
            highestFoodOrderCombo = perfectFoodOrderCombo;
        }
    }

    public void populateTakeoutWindowsLoop()
    {
        int takeoutOrderWindowCount = takeoutOrderWindowContent.childCount;


        for (int i = 0; i < takeoutOrderWindowCount; i++)
        {
            Transform takeoutOrderWindowSlot = takeoutOrderWindowContent.GetChild(i);

            takeoutOrderWindowSlot.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            FoodItemTakeoutWindow foodItemTakeoutWindow = takeoutOrderWindowSlot.GetComponentInChildren<FoodItemTakeoutWindow>();

            if (amountOfSingleOrdersSpawned >= totalAmountOfSingleOrders)
            {
                int randomFoodNumber = Random.Range(0, foodBatchCount);
                FoodSelectMenu foodSelectMenu = FoodInventoryContent.GetChild(randomFoodNumber).GetComponentInChildren<FoodSelectMenu>();

                if (foodSelectMenu.foodNeedToCookAmount > 0 && foodSelectMenu.currentSpawnedFoodAmount < foodSelectMenu.foodNeedToCookAmount)
                {
                    foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                    foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                    foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                    foodItemTakeoutWindow.foodItemTypeText.text = "BO";
                    foodSelectMenu.currentSpawnedFoodAmount++;
                    Debug.Log("is this check working??");
                    foodItemTakeoutWindow.foodWaiting = true;
                }
                else
                {
                    i--;
                }
            }
            else
            {
                int randomFoodNumber = Random.Range(0, foodInventoryCount);
                FoodSelectMenu foodSelectMenu = FoodInventoryContent.GetChild(randomFoodNumber).GetComponentInChildren<FoodSelectMenu>();

                if (foodItemTakeoutWindow != null)
                {
                    switch (foodSelectMenu.foodItem.foodItemType)
                    {
                        case FoodItem.FoodItemType.Batch:
                            if (foodSelectMenu.foodNeedToCookAmount > 0 && foodSelectMenu.currentSpawnedFoodAmount < foodSelectMenu.foodNeedToCookAmount)
                            {
                                foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                                foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                                foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                                foodItemTakeoutWindow.foodItemTypeText.text = "BO";
                                foodSelectMenu.currentSpawnedFoodAmount++;
                                foodItemTakeoutWindow.foodWaiting = true;

                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case FoodItem.FoodItemType.Single:
                            foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                            foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                            foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                            foodItemTakeoutWindow.foodItemTypeText.text = "SO";
                            amountOfSingleOrdersSpawned++;
                            foodItemTakeoutWindow.foodWaiting = true;

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }


    public void populateTakeoutWindows()
    {

        if (takeoutWindowsFilled == 8)
        {
            Debug.Log("All windows are filled!!");
            return;
        }

        if (totalAmountOfOrdersSpawned >= totalAmountOfCustomersPerRound)
        {
            Debug.Log("No more customers are coming through!!!!");
            return;
        }



        if (amountOfSingleOrdersSpawned >= totalAmountOfSingleOrders)
        {
            int randomFoodNumber = Random.Range(0, foodBatchCount);
            FoodSelectMenu foodSelectMenu = FoodInventoryContent.GetChild(randomFoodNumber).GetComponentInChildren<FoodSelectMenu>();

            if (foodSelectMenu.foodNeedToCookAmount > 0 && foodSelectMenu.currentSpawnedFoodAmount < foodSelectMenu.foodNeedToCookAmount)
            {
                SetTakeOutWindow();
                foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                foodItemTakeoutWindow.foodItemTypeText.text = "BO";
                foodSelectMenu.currentSpawnedFoodAmount++;
                Debug.Log("is this check working??");
                foodItemTakeoutWindow.foodWaiting = true;
                takeoutWindowsFilled++;
                totalAmountOfOrdersSpawned++;

            }
            else
            {
                Debug.Log("FAILED to SPAWN BATCH With RANDOM!!!");

                for (int i = 0; i < foodBatchCount; i++)
                {
                    FoodSelectMenu foodSelectMenuFL = FoodInventoryContent.GetChild(i).GetComponentInChildren<FoodSelectMenu>();
                    if (foodSelectMenuFL.foodNeedToCookAmount > 0 && foodSelectMenuFL.currentSpawnedFoodAmount < foodSelectMenuFL.foodNeedToCookAmount)
                    {
                        SetTakeOutWindow();
                        foodItemTakeoutWindow.foodItem = foodSelectMenuFL.foodItem;
                        foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenuFL.foodItem.foodItemName;
                        foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenuFL.foodItem.foodIcon;
                        foodItemTakeoutWindow.foodItemTypeText.text = "BO";
                        foodSelectMenuFL.currentSpawnedFoodAmount++;
                        Debug.Log("is this check working??");
                        foodItemTakeoutWindow.foodWaiting = true;
                        takeoutWindowsFilled++;
                        totalAmountOfOrdersSpawned++;
                        return;
                    }
                }
            }
        }
        else
        {
            int randomFoodNumber = Random.Range(0, foodInventoryCount);
            FoodSelectMenu foodSelectMenu = FoodInventoryContent.GetChild(randomFoodNumber).GetComponentInChildren<FoodSelectMenu>();

            switch (foodSelectMenu.foodItem.foodItemType)
            {
                case FoodItem.FoodItemType.Batch:
                    if (foodSelectMenu.foodNeedToCookAmount > 0 && foodSelectMenu.currentSpawnedFoodAmount < foodSelectMenu.foodNeedToCookAmount)
                    {
                        SetTakeOutWindow();

                        foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                        foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                        foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                        foodItemTakeoutWindow.foodItemTypeText.text = "BO";
                        foodSelectMenu.currentSpawnedFoodAmount++;
                        foodItemTakeoutWindow.foodWaiting = true;
                        takeoutWindowsFilled++;
                        totalAmountOfOrdersSpawned++;
                    }
                    else
                    {
                        populateTakeoutWindows();
                    }
                    break;
                case FoodItem.FoodItemType.Single:
                    SetTakeOutWindow();

                    foodItemTakeoutWindow.foodItem = foodSelectMenu.foodItem;
                    foodItemTakeoutWindow.foodItemNameTW.text = foodSelectMenu.foodItem.foodItemName;
                    foodItemTakeoutWindow.foodItemIconTW.sprite = foodSelectMenu.foodItem.foodIcon;
                    foodItemTakeoutWindow.foodItemTypeText.text = "SO";
                    amountOfSingleOrdersSpawned++;
                    foodItemTakeoutWindow.foodWaiting = true;
                    takeoutWindowsFilled++;
                    totalAmountOfOrdersSpawned++;

                    break;
                default:
                    break;
            }
        }
    }

    public void SetTakeOutWindow()
    {
        if (windowTakeoutOrderNum == 8)
        {
            windowTakeoutOrderNum = 0;
            Debug.Log("resetting Window Takeout Order number?!!!!");
        }

        takeoutOrderWindowSlot = takeoutOrderWindowContent.GetChild(windowTakeoutOrderNum);

        windowTakeoutOrderNum++;

        takeoutOrderWindowSlot.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

        foodItemTakeoutWindow = takeoutOrderWindowSlot.GetComponentInChildren<FoodItemTakeoutWindow>();

        if (foodItemTakeoutWindow.foodItem != null)
        {
            SetTakeOutWindow();
        }
    }


    public void Add(FoodItem foodItem)
    {
        FoodItems.Add(foodItem);
    }

    public void Remove(FoodItem foodItem)
    {
        FoodItems.Remove(foodItem);
    }

    public void ListFoodItems()
    {
        /* // Clean Content before opening Inventory
        foreach(Transform foodItem in FoodItemContent)
        {
            Destroy(foodItem.gameObject);
        }

        foreach(var foodItem in FoodItems)
        {
            GameObject obj = Instantiate(InventoryFoodItem, FoodItemContent);
            var foodItemName = obj.transform.Find("FoodItemName").GetComponent<TextMeshProUGUI>();
            var foodItemIcon = obj.transform.Find("FoodItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            //var inventoryfoodItem = obj.transform.GetComponent<InventoryItemController>();    

            foodItemName.text = foodItem.foodItemName;
            foodItemIcon.sprite = foodItem.foodIcon;
            //inventoryfoodItem.AddItem(foodItem);

            if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
        }
        */

        inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();
        inventoryItemController.AddItem(foodItemIM);
        Debug.Log("are in list food Items");
        foodItemName = foodLoadoutContent.GetChild(foodButtonIdPrevious).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        foodItemIcon = foodLoadoutContent.GetChild(foodButtonIdPrevious).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Image>();
        // var removeButton = foodLoadoutContent.GetChild(foodButtonIdReceive).transform.GetChild(2).GetComponent<Button>();

        foodItemName.text = foodItemIM.foodItemName;
        foodItemIcon.sprite = foodItemIM.foodIcon;
        //inventoryfoodItem.AddItem(foodItem);

        //if (EnableRemove.isOn)
        //    removeButton.gameObject.SetActive(true);

        //SetInventoryItems();
    }

    public void AutoSelectNextItemSlot()
    {
        inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;


        if (foodSlotsFilled == 8)
        {
            Debug.Log("Don't go into return, dumbass bitch!!!");
            inventoryItemController.fadeInventoryImage.SetActive(true);

            return;
        }
        if (foodButtonIdPrevious == 8)
        {
            foodButtonIdPrevious = 0;
            Debug.Log(foodButtonIdPrevious);
            Debug.Log("is it reseting Previous ID number????!!!!");
        }

        inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();
        inventoryItemController.gameObject.GetComponent<Toggle>().isOn = true;
        if (inventoryItemController.foodItem == null)
        {
            autoSelect = true;

            inventoryItemController.SetFoodButtonID();
            inventoryItemController.foodItem = null;
            inventoryItemController.Equip_Unequip_FoodSlot();

            inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();

        }
        else
        {

            Debug.Log("they are all full");
            foodButtonIdPrevious++;
            AutoSelectNextItemSlot();
            //if (foodButtonIdPrevious == 7)
            //    AutoSelectNextItemSlot();

            //    return;
        }
    }

    public void turnOffOtherToggles()
    {

        foodButtonIdPrevious = foodButtonIdReceive;
        //foodItemButton = foodLoadoutContent.GetChild(foodButtonIdReceive).GetComponentInChildren<Toggle>();
        ////foodItemButton = inventoryItemController.GetFoodSlotToggle();
        //inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdReceive).GetComponentInChildren<InventoryItemController>();

        foreach (Transform foodItemSlot in foodLoadoutContent)
        {
            foodItemButton = foodItemSlot.gameObject.GetComponentInChildren<Toggle>();
            //foodItemButton = inventoryItemController.GetFoodSlotToggle();
            inventoryItemController = foodItemSlot.gameObject.GetComponentInChildren<InventoryItemController>();


            if (foodItemButton.isOn &&
                inventoryItemController.GetFoodButtodSlotID() != foodButtonIdPrevious)
            {
                foodItemButton.isOn = false;
                inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
            }
        }
        inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();


    }

    public void turnOffAllToggles()
    {
        //foodItemButton = foodLoadoutContent.GetChild(foodButtonIdReceive).GetComponentInChildren<Toggle>();
        ////foodItemButton = inventoryItemController.GetFoodSlotToggle();
        //inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdReceive).GetComponentInChildren<InventoryItemController>();

        foreach (Transform foodItemSlot in foodLoadoutContent)
        {
            foodItemButton = foodItemSlot.gameObject.GetComponentInChildren<Toggle>();
            //foodItemButton = inventoryItemController.GetFoodSlotToggle();
            inventoryItemController = foodItemSlot.gameObject.GetComponentInChildren<InventoryItemController>();
            foodItemButton.isOn = false;
            inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
        }
    }


    public void RemoveFoodItemSlot()
    {

        //inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdReceive).GetComponent<InventoryItemController>();
        //inventoryItemController.AddItem(foodItemIM);
        foodItemName = foodLoadoutContent.GetChild(foodButtonIdReceive).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        foodItemIcon = foodLoadoutContent.GetChild(foodButtonIdReceive).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Image>();
        //var removeButton = foodLoadoutContent.GetChild(foodButtonIdReceive).transform.GetChild(2).GetComponent<Button>();


        foodItemName.text = null;
        foodItemIcon.sprite = null;

    }

    public void AddFoodBackToInventory()
    {
        if (!FoodInventoryContent.GetChild(foodButtonIdPrevious).gameObject.activeInHierarchy)
        {
            FoodInventoryContent.GetChild(foodButtonIdPrevious).gameObject.SetActive(true);
        }
    }

    public void EnableFoodItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform foodItem in FoodInventoryContent)
            {
                foodItem.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform foodItem in FoodInventoryContent)
            {
                foodItem.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    /* public void SetInventoryItems()
    {
        InventoryFoodItems = FoodItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < FoodItems.Count; i++)
        {
            InventoryFoodItems[i].AddItem(FoodItems[i]);
        }
    }
    */


    /// <summary>
    /// So i might need to add a switch case because what is going to happen is that the first food
    /// will usually always get the highest amount of food needed to cook 
    /// so we might have to add a switch case where in different cases, the food will most likely not 
    /// give the first food the most all the time, so it's even more random than it is now
    /// 
    /// There is also another glitch that happens that if the the transform gets to the last batch order
    /// and it hasn't filled in enough orders in the needcooking amount and we get a zero as a random number
    /// It will spawn less random orders than the total batch orders that are needed to complete the level.
    /// </summary>
    /// <returns></returns>
    public int RandomAmountOfFoodToCook()
    {
        int randomNumMaximum = totalAmountOfBatchOrders - totalAmountOfRandomOrders;

        int randomNum = Random.Range(0, randomNumMaximum + 1);

        totalAmountOfRandomOrders += randomNum;

        return randomNum;
    }

    public int fillLastBatchWithRemainingFood()
    {
        int remainingOrders = totalAmountOfBatchOrders - totalAmountOfRandomOrders;

        if (remainingOrders > 0)
        {
            Debug.Log("Returned remaining Orders GREATER than Zero!!");
            totalAmountOfRandomOrders += remainingOrders;
            return remainingOrders;
        }

        Debug.Log("Returned remaining orders NONE/Zero!!");
        return remainingOrders;
    }
}
