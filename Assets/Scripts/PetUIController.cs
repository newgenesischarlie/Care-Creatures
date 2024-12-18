using UnityEngine;
using UnityEngine.UI;

public class PetUIController : MonoBehaviour
{
    public Image happinessImage, energyImage;

    public static PetUIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one PetUIController in the Scene");
    }

    public void UpdateImages(int happiness, int energy)
    {
        happinessImage.fillAmount = Mathf.Clamp01((float)happiness / 100f);  // Update happiness bar
        energyImage.fillAmount = Mathf.Clamp01((float)energy / 100f);
    }
}
