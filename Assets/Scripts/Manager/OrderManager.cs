using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    [SerializeField] private RecipeList recipleList;
    [SerializeField] private int orderMaxCount = 5;
    [SerializeField] private float orderRate = 2f;
    private int successDeliveryCount = 0;

    private float orderTimer = 0;
    private bool isStartOrder = false;
    private int orderCount = 0;

    public static OrderManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    private List<RecipeSO> orderRecipeList = new List<RecipeSO>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
    }

    private void Update()
    {
        if (isStartOrder)
        {
            OrderUpdate();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            StartSpawnOrder();
        }
    }

    private void OrderUpdate()
    {
        orderTimer += Time.deltaTime;
        if (orderTimer >= orderRate)
        {
            orderTimer = 0;
            OrderANewRecipe();
        }
    }

    private void OrderANewRecipe()
    {
        if (orderCount > orderMaxCount) return;

        orderCount++;

        int index = UnityEngine.Random.Range(0, recipleList.recipeList.Count);
        orderRecipeList.Add(recipleList.recipeList[index]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO correctRecipe = null;

        foreach (RecipeSO recipeSO in orderRecipeList)
        {
            if (IsCorrect(recipeSO, plateKitchenObject))
            {
                correctRecipe = recipeSO;
                break;
            }
        }

        if (correctRecipe == null)
        {
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
            print("上菜失败");
        }
        else
        {
            orderRecipeList.Remove(correctRecipe);
            successDeliveryCount++;
            OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
            print("上菜成功");
        }
    }

    private bool IsCorrect(RecipeSO recipeSO, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipeSO.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();

        if (list1.Count != list2.Count) return false;

        foreach (KitchenObjectSO item in list1)
        {
            if (list2.Contains(item) == false) return false;
        }

        return true;
    }

    public List<RecipeSO> GetOrderList() => orderRecipeList;

    public void StartSpawnOrder()
    {
        isStartOrder = true;
    }

    public int GetSuccessDeliveryCount() => successDeliveryCount;
}
