using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
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
    public GameObject fadeBeginDayButtonImage;
    public int foodLoadoutSlotsBatchOrdersCount;

    public GameObject[] foodButtonsBatchOrders;
    public int foodButtonsBatchCount;
    public GameObject[] foodButtonsSingleOrders;
    public int foodButtonsSingleCount;
    public GameObject foodBatchInventory;
    public GameObject foodSingleInventory;
    public GameObject fadeLoadoutImage;

    private void Awake()
    {
        Instance = this;

        foodInventoryCount = FoodInventoryContent.childCount;
        fadeBeginDayButtonImage = GameObject.FindGameObjectWithTag("FadeBeginDayButtonImage");

        foodLoadoutSlotsBatchOrdersCount = foodLoadoutContent.childCount;
        foodButtonsBatchOrders = GameObject.FindGameObjectsWithTag("FoodButtonsBatchOrders");
        foodButtonsBatchCount = foodButtonsBatchOrders.Length;
        foodButtonsSingleOrders = GameObject.FindGameObjectsWithTag("FoodButtonsSingleOrders");
        foodButtonsSingleCount = foodButtonsSingleOrders.Length;

        foodBatchInventory = GameObject.FindGameObjectWithTag("FoodBatchInventory");
        foodSingleInventory = GameObject.FindGameObjectWithTag("FoodSingleInventory");

        InitializeInfoOfFoodItemsInInventory();

        fadeLoadoutImage = GameObject.FindGameObjectWithTag("FadeLoadoutImage");
        fadeLoadoutImage.SetActive(false);

        //if(foodButtonIdPrevious >= 0 && foodButtonIdPrevious < foodButtonsBatchCount)
        //{

        //}
        //if(foodButtonIdPrevious >= foodButtonsBatchCount && foodButtonIdPrevious < foodButtonsSingleCount + foodButtonsBatchCount)
        //{

        //}
    }

    private void Update()
    {
        if(foodSlotsFilled == 8)
        {
            fadeBeginDayButtonImage.SetActive(false);
            fadeLoadoutImage.SetActive(false);
        }
        else
        {
            fadeBeginDayButtonImage.SetActive(true);
        }
    }

    public void InitializeInfoOfFoodItemsInInventory()
    {
        foreach (Transform foodItem in foodBatchInventory.transform)
        {
            ItemPickup itemPickup = foodItem.gameObject.GetComponent<ItemPickup>();

            foodItemName = foodItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            foodItemIcon = foodItem.transform.GetChild(1).GetComponent<Image>();

            foodItemName.text = itemPickup.foodItem.foodItemName;
            foodItemIcon.sprite = itemPickup.foodItem.foodIcon;
        }

        foreach (Transform foodItem in foodSingleInventory.transform)
        {
            ItemPickup itemPickup = foodItem.gameObject.GetComponent<ItemPickup>();

            foodItemName = foodItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            foodItemIcon = foodItem.transform.GetChild(1).GetComponent<Image>();

            foodItemName.text = itemPickup.foodItem.foodItemName;
            foodItemIcon.sprite = itemPickup.foodItem.foodIcon;
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
            turnOffAllToggles();
            return;
        }
        if (foodButtonIdPrevious == 8)
        {
            foodButtonIdPrevious = 0;
            Debug.Log(foodButtonIdPrevious);
            Debug.Log("is it reseting Previous ID number????!!!!");
        }

        inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();
        inventoryItemController.foodToggleIsOn = true;
        if (inventoryItemController.foodItem == null)
        {
            autoSelect = true;
            DisplayCorrectInventory();

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

        foreach (Transform foodItemSlot in foodLoadoutContent)
        {
            //foodItemButton = inventoryItemController.GetFoodSlotToggle();
            inventoryItemController = foodItemSlot.gameObject.GetComponentInChildren<InventoryItemController>();


            if (inventoryItemController.foodToggleIsOn &&
                inventoryItemController.GetFoodButtodSlotID() != foodButtonIdPrevious)
            {
                inventoryItemController.foodToggleIsOn = false;
                inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
            }
        }
        inventoryItemController = foodLoadoutContent.GetChild(foodButtonIdPrevious).GetComponentInChildren<InventoryItemController>();


    }

    public void turnOffAllToggles()
    {
        foreach (Transform foodItemSlot in foodLoadoutContent)
        {
            inventoryItemController = foodItemSlot.gameObject.GetComponentInChildren<InventoryItemController>();
            //foodItemButton = inventoryItemController.GetFoodSlotToggle();
            inventoryItemController.foodToggleIsOn = false;
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

        if (foodButtonIdReceive >= 0 && foodButtonIdReceive < foodButtonsBatchCount)
        {
            Debug.Log("Returning to batch Inventory!!!");

            if (!FoodInventoryContent.GetChild(foodButtonIdPrevious).gameObject.activeInHierarchy)
            {
                FoodInventoryContent.GetChild(foodButtonIdPrevious).gameObject.SetActive(true);
            }
        }
        if (foodButtonIdReceive >= foodButtonsBatchCount && foodButtonIdReceive < foodButtonsSingleCount + foodButtonsBatchCount)
        {
            Debug.Log("Returning to batch Inventory!!!");
            if (!foodSingleInventory.transform.GetChild(foodButtonIdPrevious).gameObject.activeInHierarchy)
            {
                foodSingleInventory.transform.GetChild(foodButtonIdPrevious).gameObject.SetActive(true);

            }
        }
    }


    public void DisplayCorrectInventory()
    {
        if (foodButtonIdPrevious >= 0 && foodButtonIdPrevious < foodButtonsBatchCount)
        {
            Debug.Log("BatchOrders!!!");
            foodBatchInventory.SetActive(true);
            foodSingleInventory.SetActive(false);
        }
        if (foodButtonIdPrevious >= foodButtonsBatchCount && foodButtonIdPrevious < foodButtonsSingleCount + foodButtonsBatchCount)
        {
            Debug.Log("Single Orders!!!!");
            foodBatchInventory.SetActive(false);
            foodSingleInventory.SetActive(true);
        }
    }
   
    public void EnableFoodItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach(Transform foodItem in FoodInventoryContent)
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
}
