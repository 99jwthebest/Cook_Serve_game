using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Food Item/Create New Item")]
public class FoodItem : ScriptableObject
{
    public int id;
    public string foodItemName;
    public int foodToBeMade;
    public Sprite foodIcon;
    public FoodItemType foodItemType;

    public enum FoodItemType
    {
        Batch,
        Single
    }
}
