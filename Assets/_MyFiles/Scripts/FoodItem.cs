using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FoodStep
{
    [SerializeField]
    FoodIngredients[] ingredients;

    public FoodIngredients[] Ingredients
    {
        get { return ingredients; }
    }
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
    int prepStepCount;
    public int foodIngredientsAmountPerTab;
    public int firstIngredientCount;

    [SerializeField]
    FoodStep[] steps;

    public enum FoodItemType
    {
        Batch,
        Single
    }

    public FoodStep[] GetFoodSteps()
    {
        return steps;
    }

    public int GetAmountOfFoodSteps()
    {
        int amountOfSteps = steps.Length;
        return amountOfSteps;
    }

    public int GetAmountOfFoodIngredients(int iterateThroughSteps) // int iterateThroughSteps
    {
        return steps[iterateThroughSteps].Ingredients.Length;
    }

    public int GetPrepStepCount()
    {
        return prepStepCount;
    }

}