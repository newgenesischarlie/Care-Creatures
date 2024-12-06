﻿using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            MinigameUIController.instance.LoseMinigame();
        }
    }
}
