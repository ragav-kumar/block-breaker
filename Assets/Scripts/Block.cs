using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config params
    [SerializeField] AudioClip destructionSound;
    [SerializeField] GameObject destroyVFX;
    private int maxHealth;
    [SerializeField] Sprite[] hitSprites;

    // State
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
            level.addBlock();
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
                level.removeBlock();
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
        GameObject fx = Instantiate(destroyVFX,transform.position, transform.rotation);
        Destroy(fx, 1f);
    }
}
