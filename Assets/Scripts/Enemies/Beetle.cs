using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : PhysicsObject
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(BeetleRoutine());
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

    IEnumerator BeetleRoutine()
    {
        while (true)
        {
            targetVelocity = Vector2.right;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.flipY = !spriteRenderer.flipY;
            targetVelocity = Vector2.left;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }
}