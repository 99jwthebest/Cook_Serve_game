using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeIngredientTabs : MonoBehaviour, IScrollHandler
{
    public int selectedTab = 0;

    // Start is called before the first frame update
    void Start()
    {

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
                if (selectedTab >= FoodMenuInventoryManager.Instance.changingTabsForIngredients.childCount - 1)
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
                    selectedTab = FoodMenuInventoryManager.Instance.changingTabsForIngredients.childCount - 1;
                }
                else
                {
                    selectedTab--;
                }
            }


            if (previousSelectedTab != selectedTab)
            {
                SelectTab();

            }
        }
    }


    public void SelectTab()
    {
        int i = 0;
        foreach (Transform tab in FoodMenuInventoryManager.Instance.changingTabsForIngredients)
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