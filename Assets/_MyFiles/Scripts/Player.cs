using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

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
        if(Input.GetMouseButtonUp(1))
        {
            GetOutOfMakingBatchOrder();
            FoodMenuInventoryManager.Instance.menuFoodInventory.gameObject.SetActive(false);
            if(pState != PlayerState.MakingSingleOrder)
            {
                FoodMenuInventoryManager.Instance.foodSelectMenu.gameObject.SetActive(false);

            }
        }
    }


    public void GetOutOfMakingBatchOrder()
    {
        if(pState == PlayerState.MakingBatchOrder)
        {
            ResetFoodImagesAndFoodPrepButtons();
            FoodMenuInventoryManager.Instance.foodPrepMenuContent.gameObject.SetActive(false);
            FoodMenuInventoryManager.Instance.foodSelectMenuContent.gameObject.SetActive(true);
            Debug.Log("State Changed Worked!");
            pState = PlayerState.None;
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

}
