using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableFood : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public FoodItem foodData;
    
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetFoodData(FoodItem food)
    {
        foodData = food;
        if (image != null && food != null)
        {
            image.sprite = food.foodIcon;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        bool droppedOnPet = false;
        
        if (eventData.pointerEnter != null)
        {
            PetDropZone dropZone = eventData.pointerEnter.GetComponent<PetDropZone>();
            if (dropZone != null)
            {
                dropZone.FeedPet(foodData);
                droppedOnPet = true;
                Destroy(gameObject);
            }
        }
        
        if (!droppedOnPet)
        {
            transform.SetParent(originalParent);
            transform.position = originalPosition;
        }
    }
}
