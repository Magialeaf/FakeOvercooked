using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CuttingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCount;
}

[CreateAssetMenu]
public class CuttingRecipeList : ScriptableObject
{
    public List<CuttingRecipe> list;
    public KitchenObjectSO GetOutput(KitchenObjectSO input)
    {
        foreach (var item in list)
        {
            if (item.input == input)
            {
                return item.output;
            }
        }
        return null;
    }

    public bool TryGetCuttingRecipe(KitchenObjectSO input, out CuttingRecipe cuttingRecipe)
    {
        foreach (CuttingRecipe item in list)
        {
            if (item.input == input)
            {
                cuttingRecipe = item;
                return true;
            }
        }
        cuttingRecipe = null;
        return false;
    }

}