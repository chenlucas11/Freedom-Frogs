using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    private SpriteRenderer spriteRenderer;
    private PlayerController player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();

            if (player != null)
                player.Damage();

            Destroy(this.gameObject);
        }
        if (other.CompareTag("Tongue"))
        {
            Debug.Log("Tongue hit");
            Destroy(this.gameObject);
        }
    }
}
