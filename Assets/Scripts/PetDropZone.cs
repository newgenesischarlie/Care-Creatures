using UnityEngine;
using UnityEngine.EventSystems;

public class PetDropZone : MonoBehaviour, IDropHandler
{
    private NeedsController needsController;

    private void Awake()
    {
        needsController = GetComponentInParent<NeedsController>();
        
        if (needsController == null)
        {
            needsController = FindObjectOfType<NeedsController>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableFood draggableFood = eventData.pointerDrag?.GetComponent<DraggableFood>();
        
        if (draggableFood != null && draggableFood.foodData != null)
        {
            FeedPet(draggableFood.foodData);
        }
    }

    public void FeedPet(FoodItem food)
    {
        if (needsController != null && food != null)
        {
            needsController.ChangeEnergy(food.energyValue);
            needsController.ChangeHappiness(food.happinessValue);
            PetUIController.instance.UpdateImages(needsController.happiness, needsController.energy);
            
            Debug.Log($"Fed pet with {food.foodName}! Energy +{food.energyValue}, Happiness +{food.happinessValue}");
        }
    }
}
