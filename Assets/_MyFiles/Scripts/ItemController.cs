using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerEnterHandler
{
    public FoodItem foodItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundEffects.instance.PlayHoverOverButtonSound();
    }
}
