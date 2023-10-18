using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FoodSelectMenu : MonoBehaviour
{
    public FoodItem foodItem;
    public TextMeshProUGUI foodItemNameSM;
    public Image foodItemIconSM;
    public TextMeshProUGUI amountOfFoodThatCanBeMade;
    public TextMeshProUGUI foodNeedToCookText;
    public int foodNeedToCookAmount;
    public TextMeshProUGUI foodCookingText;
    public int foodCookingCount;
    public int currentSpawnedFoodAmount;
    public int foodItemButtonID;

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
        foreach(Transform child in FoodMenuInventoryManager.Instance.foodPrepMenuContent)
        {
            FoodSelectMenu foodSelectMenu = child.GetComponentInChildren<FoodSelectMenu>();

            foodSelectMenu.foodItem = foodItem;
            foodSelectMenu.foodItemButtonID = foodItemButtonID;

        }
    }

    public void turnOnFoodPrepImages()
    {
        FoodMenuInventoryManager.Instance.foodPrepImages.gameObject.SetActive(true);
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).gameObject.SetActive(true); // change index of 0 to foodItem.Id
        FoodMenuInventoryManager.Instance.foodPrepImages.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        changePlayerState();
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
