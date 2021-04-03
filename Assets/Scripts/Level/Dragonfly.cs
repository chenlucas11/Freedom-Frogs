using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonfly : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Vector3 distance = new Vector3(17, 0, 0);
    private Vector3 currentTarget;
    private bool facingRight;

    [SerializeField] private float delayTimer = 2f;
    private float delayStart;

    private GameObject target = null;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        currentTarget = transform.position + distance;
        target = null;
    }

    void Update()
    {
        if (transform.position != currentTarget)
            Move();
        else
            UpdateTarget();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBottom"))
        {
            target = other.transform.parent.gameObject;
            offset = target.transform.position - transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        target = null;
    }


    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        delayStart = Time.time;
    }

    private void UpdateTarget()
    {
        if (Time.time - delayStart > delayTimer)
        {
            if (facingRight)
            {
                transform.localScale = new Vector2(-1, 1);
                facingRight = false;
                currentTarget = transform.position - distance;
            }
            else if (!facingRight)
            {
                transform.localScale = new Vector2(1, 1);
                facingRight = true;
                currentTarget = transform.position + distance;
            }
        }
    }
}
