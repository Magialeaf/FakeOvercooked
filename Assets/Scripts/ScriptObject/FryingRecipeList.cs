using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FryingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTime;
}

[CreateAssetMenu]
public class FryingRecipeList : ScriptableObject
{
    public List<FryingRecipe> list;

    public bool TryGetFryingRecipe(KitchenObjectSO input, out FryingRecipe fryingRecipe)
    {
        foreach (FryingRecipe item in list)
        {
            if (item.input == input)
            {
                fryingRecipe = item;
                return true;
            }
        }
        fryingRecipe = null;
        return false;
    }
}
