﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
#pragma warning disable 0649
    // Config params
    [SerializeField] AudioClip destructionSound;
    [SerializeField] GameObject destroyVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject droppedPowerUp;
#pragma warning restore 0649
    // State
    private int maxHealth;
    private int currentHealth;

    // Cached reference
    private Level level;

    private void Start()
    {
        maxHealth = hitSprites.Length + 1;
        currentHealth = maxHealth;
        CountBreakableBlocks();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(destructionSound, Camera.main.transform.position);
        DestroyBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.AddBlock();
        }
    }
    private void DestroyBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                TriggerDestroyEffect();
                Destroy(gameObject, 0.1f);
                level.RemoveBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
            
        }
    }
    private void ShowNextHitSprite()
    {
        int spriteIndex = (maxHealth - currentHealth - 1) % hitSprites.Length;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }
    private void TriggerDestroyEffect()
    {
        // VFX
        GameObject fx = Instantiate(destroyVFX,transform.position, transform.rotation);
        Destroy(fx, 1f);
        // Powerup, if present
        if (droppedPowerUp != null)
        {
            Instantiate(droppedPowerUp, transform.position, transform.rotation);
        }
    }
}
