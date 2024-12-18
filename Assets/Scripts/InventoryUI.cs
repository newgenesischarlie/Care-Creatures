using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject gameObjectToToggle;

    public void WhenButtonClicked()
    {
        if (gameObjectToToggle.activeInHierarchy)
        {
            gameObjectToToggle.SetActive(false);
        }
        else
        {
            gameObjectToToggle.SetActive(true);
        }
    }
}