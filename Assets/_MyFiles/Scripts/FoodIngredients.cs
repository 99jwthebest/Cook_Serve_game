using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Ingredient")]
public class FoodIngredients : ScriptableObject
{
    public int ingredientID;
    public string ingredientName;
    public Sprite foodIngredientIcon;
}