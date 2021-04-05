using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkBeetleProj : MonoBehaviour
{
    [SerializeField] private float speed = 7;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().Damage();
            Destroy(this.gameObject);
        }
    }
}
