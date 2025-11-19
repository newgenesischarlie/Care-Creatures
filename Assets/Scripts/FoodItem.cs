using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Pet Simulator/Food Item")]
public class FoodItem : ScriptableObject
{
    public string foodName;
    public Sprite foodIcon;
    public int energyValue;
    public int happinessValue;
}
