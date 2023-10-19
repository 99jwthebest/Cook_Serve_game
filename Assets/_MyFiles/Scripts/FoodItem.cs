using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct FoodStep
{ 
    [SerializeField]
    FoodIngriedients[] ingriedients;
}

[CreateAssetMenu(fileName = "New Item", menuName = "Food Item/Create New Item")]
public class FoodItem : ScriptableObject
{
    public int id;
    public string foodItemName;
    public int foodToBeMade;
    public Sprite foodIcon;
    public FoodItemType foodItemType;

    [SerializeField]
    FoodStep[] steps;
    public enum FoodItemType
    {
        Batch,
        Single
    }
}
