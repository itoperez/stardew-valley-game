using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe List")]
public class RecipeList : ScriptableObject
{
    public List<CraftingRecipe> recipes;
}
