using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeIngredientTabs : MonoBehaviour, IScrollHandler
{
    public static ChangeIngredientTabs instance;
    public int selectedTab = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnScroll(PointerEventData eventData)
    {
        if (Player.instance.pState == Player.PlayerState.MakingBatchOrder ||
           Player.instance.pState == Player.PlayerState.MakingSingleOrder)
        {

            int previousSelectedTab = selectedTab;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedTab >= FoodMenuInventoryManager.Instance.changingImageForTabIngredients.childCount - 1)
                {
                    selectedTab = 0;
                }
                else
                {
                    selectedTab++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedTab <= 0)
                {
                    selectedTab = FoodMenuInventoryManager.Instance.changingImageForTabIngredients.childCount - 1;
                }
                else
                {
                    selectedTab--;
                }
            }


            if (previousSelectedTab != selectedTab)
            {
                SelectImageTab();
                SelectTab();
            }
        }
    }


    public void SelectImageTab()
    {
        int i = 0;
        foreach (Transform tabImage in FoodMenuInventoryManager.Instance.changingImageForTabIngredients)
        {
            if (i == selectedTab)
            {
                tabImage.gameObject.SetActive(true);
            }
            else
            {
                tabImage.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void SelectTab()
    {
        int i = 0;
        foreach (Transform tab in FoodMenuInventoryManager.Instance.changingTabsIngredients)
        {
            if (i == selectedTab)
            {
                tab.gameObject.SetActive(true);
            }
            else
            {
                tab.gameObject.SetActive(false);
            }
            i++;
        }
    }
}