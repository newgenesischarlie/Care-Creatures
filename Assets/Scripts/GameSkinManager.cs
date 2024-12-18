using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject selectedSkin;
   public GameObject Player;

   private Sprite PlayerSprite;
    void Start()
    {
    PlayerSprite = selectedSkin.GetComponent<SpriteRenderer>().sprite;

       Player.GetComponent<SpriteRenderer>().sprite = PlayerSprite;

    }
}
