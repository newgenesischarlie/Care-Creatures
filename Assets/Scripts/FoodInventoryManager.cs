using UnityEngine;

public class FoodInventoryManager : MonoBehaviour
{
    public FoodInventorySlot[] inventorySlots;
    public FoodItem[] startingFoodItems;

    private void Start()
    {
        PopulateInventory();
    }

    public void PopulateInventory()
    {
        for (int i = 0; i < inventorySlots.Length && i < startingFoodItems.Length; i++)
        {
            if (inventorySlots[i] != null && startingFoodItems[i] != null)
            {
                inventorySlots[i].AddFood(startingFoodItems[i]);
            }
        }
    }

    public void AddFoodToSlot(int slotIndex, FoodItem food)
    {
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
        {
            inventorySlots[slotIndex].AddFood(food);
        }
    }

    public FoodInventorySlot FindEmptySlot()
    {
        foreach (var slot in inventorySlots)
        {
            if (!slot.HasFood())
            {
                return slot;
            }
        }
        return null;
    }
}
