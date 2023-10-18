using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingStationSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public FoodItem foodItem;

    //public Button RemoveButton;
    public int foodItemButtonIDCS;
    public bool equippedFoodItem;
    public GameObject foodInventory;
    public GameObject foodSelectMenu;
    public Toggle foodSlotToggle;
    public Image foodItemImage;
    public TextMeshProUGUI foodItemNameText;
    //public GameObject fadeInventoryImage;

    public TextMeshProUGUI howMuchFoodIsLeftText;
    public int howMuchFoodIsLeft;
    public bool hasFood;

    void Awake()
    {
        foodInventory = GameObject.FindGameObjectWithTag("FoodInventory");
        //fadeInventoryImage = GameObject.FindGameObjectWithTag("FadeInventoryImage");
        foodSlotToggle = gameObject.GetComponent<Toggle>();
        foodSelectMenu = GameObject.FindGameObjectWithTag("FoodSelectMenu");
    }

    void Start()
    {
        foodInventory.SetActive(false);
        foodSelectMenu.SetActive(false);
    }

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


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("It enter the area, Boy!!!!");

        
        transform.parent.GetComponent<Image>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("It exited the area, Dawg!!!!");
        if (!foodSlotToggle.isOn)
            transform.parent.GetComponent<Image>().enabled = false;

    }
    //Onigiri, Sausage, Sauerkraut, Grilled Cheese Sandwich, marshmallow squares (rice crispies), Iced Americano (espresso), Shepherd's Pie
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("You clicked mothafucka!!!!");
        //if (FoodMenuInventoryManager.Instance.autoSelect)
        //{
        //    FoodMenuInventoryManager.Instance.inventoryItemController.transform.parent.GetComponent<Image>().enabled = false;
        //    Debug.Log("You stupid fucker!!!!");
        //    FoodMenuInventoryManager.Instance.autoSelect = false;
        //}

        if (foodItem == null)
        {
            foodInventory.SetActive(true);
            foodSelectMenu.SetActive(true);

            FoodMenuInventoryManager.Instance.foodButtonIDCSReceive = foodItemButtonIDCS;
        }
        else if (foodItem != null)
        {
            foreach (Transform foodItemSlot in FoodMenuInventoryManager.Instance.FoodInventoryContent)
            {
                FoodSelectMenu foodSelectMenu = foodItemSlot.GetComponentInChildren<FoodSelectMenu>();

                if (foodItem.id == foodSelectMenu.foodItem.id)
                {
                    foodSelectMenu.foodCookingCount -= foodItem.foodToBeMade;
                    foodSelectMenu.foodCookingText.text = foodSelectMenu.foodCookingCount.ToString();
                    howMuchFoodIsLeft = 0;
                    howMuchFoodIsLeftText.text = howMuchFoodIsLeft.ToString();
                    foodItem = null;
                    foodItemNameText.text = null;
                    foodItemImage.sprite = null;
                    transform.parent.GetComponent<Image>().enabled = false;

                    //hasFood = false;

                    break;
                }
            }
        }

        //SetFoodButtonID();
        //Equip_Unequip_FoodSlot();
    }

    public void startAddTocookingCorotine(int amountOfFood, CookingStationSlot cookingStationSlotI, FoodSelectMenu foodSelectMenu)
    {
        StartCoroutine(cookingStationSlotI.addToCookingStationSlotI(amountOfFood, cookingStationSlotI, foodSelectMenu));

    }

    public IEnumerator addToCookingStationSlotI(int amountOfFood, CookingStationSlot cookingStationSlotI, FoodSelectMenu foodSelectMenu)
    {
        changePlayerStateToNone();

        cookingStationSlotI.foodItemImage.sprite = foodSelectMenu.foodItem.foodIcon;
        cookingStationSlotI.foodItemNameText.text = foodSelectMenu.foodItem.foodItemName.ToString();

        cookingStationSlotI.howMuchFoodIsLeftText.text = "CO";

        Debug.Log("Is is going into ienum??");
        yield return new WaitForSeconds(3f);
        Debug.Log("Is it getting past Wait For Seconds??");

        cookingStationSlotI.foodItem = foodSelectMenu.foodItem;
        cookingStationSlotI.howMuchFoodIsLeft += amountOfFood;
        cookingStationSlotI.howMuchFoodIsLeftText.text = cookingStationSlotI.howMuchFoodIsLeft.ToString();
       // cookingStationSlotI.hasFood = true;

    }

    public void changePlayerState()
    {
        Player.instance.pState = Player.PlayerState.MakingBatchOrder;
    }

    public void changePlayerStateToNone()
    {
        Player.instance.pState = Player.PlayerState.None;
    }

    public void checkMenuItems()
    {

    }
}
