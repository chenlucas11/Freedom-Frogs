using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : PhysicsObject
{
    private bool flipped = false;


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

            if (flipped)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.CompareTag("Tongue"))
        {
            Debug.Log("Tongue hit");
            if (flipped)
            {
                Destroy(this.gameObject);
            }
            else
            {
                FlipOver();
            }
        }
    }

    private void FlipOver()
    {
        if (!flipped)
        {
            this.transform.localScale = new Vector2(1, -1);
            flipped = true;
        }
        else
        {
            this.transform.localScale = new Vector2(1, 1);
            flipped = false;
        }
    }

    IEnumerator BeetleRoutine()
    {
        while (!flipped)
        {
            targetVelocity = Vector2.right;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(1, 1);
            targetVelocity = Vector2.left;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
