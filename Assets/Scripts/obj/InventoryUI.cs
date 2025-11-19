using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject gameObjectToToggle;
    public GameObject[] additionalObjectsToToggle;

    public void WhenButtonClicked()
    {
        bool isActive = gameObjectToToggle.activeInHierarchy;
        
        gameObjectToToggle.SetActive(!isActive);
        
        if (additionalObjectsToToggle != null)
        {
            foreach (GameObject obj in additionalObjectsToToggle)
            {
                if (obj != null)
                {
                    obj.SetActive(!isActive);
                }
            }
        }
    }
}