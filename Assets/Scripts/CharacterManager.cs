using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskin;

    

    public void NextOption()
    {

        Debug.Log("next");
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void BackOption() {

        Debug.Log("back");
    
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }
        sr.sprite = skins[selectedSkin];
    }
    public void PlayGame()
    {

        Debug.Log("play game");
        //PrefabUtility.SaveAsPrefabAsset(playerskin, "Asset/selectedSkin.prefab ");
        SceneManager.LoadScene("MainGame");
    }
}
