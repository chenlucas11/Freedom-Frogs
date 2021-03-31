using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : PhysicsObject
{
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gravityModifier = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BirdRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // IN PROGRESS
    IEnumerator BirdRoutine()
    {
        while (true)
        {
            targetVelocity = Vector2.right;
            yield return new WaitForSeconds(5.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            this.transform.localScale = new Vector2(-1, 1);
            yield return new WaitForSeconds(1.0f);
            targetVelocity = Vector2.left;
            yield return new WaitForSeconds(5.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            this.transform.localScale = new Vector2(1, 1);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
