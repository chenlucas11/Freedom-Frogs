using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Vector2 playerLocation;
    private Vector2 playerVelocity;
    [SerializeField] private GameObject tongue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tongue(Vector3 location)
    {
        Vector3 tongue_length = new Vector3(2.0f, 0.0f, 0.0f);
        Instantiate(tongue, transform.position + tongue_length, Quaternion.identity);
    }
}
