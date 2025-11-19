using UnityEngine;
using UnityEngine.UI;

public class FoodInventorySlot : MonoBehaviour
{
    public FoodItem foodItem;
    public GameObject draggableFoodPrefab;

    private Image slotImage;
    private GameObject currentFoodInstance;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    private void Start()
    {
        if (foodItem != null)
        {
            AddFood(foodItem);
        }
    }

    public void AddFood(FoodItem food)
    {
        foodItem = food;
        
        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
        }

        if (draggableFoodPrefab != null && food != null)
        {
            currentFoodInstance = Instantiate(draggableFoodPrefab, transform);
            
            RectTransform rectTransform = currentFoodInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            
            DraggableFood draggable = currentFoodInstance.GetComponent<DraggableFood>();
            if (draggable != null)
            {
                draggable.SetFoodData(food);
            }
            
            Image foodImage = currentFoodInstance.GetComponent<Image>();
            if (foodImage != null)
            {
                foodImage.sprite = food.foodIcon;
            }
        }
    }

    public void RemoveFood()
    {
        foodItem = null;
        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
        }
    }

    public bool HasFood()
    {
        return foodItem != null;
    }
}
