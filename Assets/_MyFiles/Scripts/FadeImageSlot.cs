using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FadeImageSlot : MonoBehaviour, IPointerClickHandler
{
    GameObject fadeInventoryImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        InventoryManager.Instance.turnOffAllToggles();
        fadeInventoryImage.SetActive(true);

        Debug.Log("Am I clicking??");
    }

    void Awake()
    {
        fadeInventoryImage = GameObject.FindGameObjectWithTag("FadeInventoryImage");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
