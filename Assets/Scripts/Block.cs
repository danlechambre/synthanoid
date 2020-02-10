using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private LevelManager levelManager;

#pragma warning disable CS0649
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private GameObject blockExplosionPrefab;
    [SerializeField]
    private Color[] colors = new Color[5];
#pragma warning restore CS0649

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int baseValue = 10;
    [SerializeField]
    private int hitPoints = 1;

    private int blockValue;

    
    private void Start()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        blockValue = baseValue * hitPoints;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.tag != "Unbreakable Block")
        {
            SetBlockColor();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Ball" && gameObject.tag == "Breakable Block")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        hitPoints -= 1;

        TriggerBlockImpactFX();

        if (hitPoints < 1)
        {
            DestroyBlock();
        }
        else
        {
            SetBlockColor();
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.5f);
        levelManager.BlockDestroyed(blockValue);
        Destroy(gameObject);
    }

    private void TriggerBlockImpactFX()
    {
        GameObject explosion = Instantiate(blockExplosionPrefab, gameObject.transform.position, Quaternion.identity);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = this.spriteRenderer.color;
    }

    private void SetBlockColor()
    {
        spriteRenderer.color = colors[hitPoints - 1];
    }
}
