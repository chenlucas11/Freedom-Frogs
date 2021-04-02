using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();;
        Destroy(this.gameObject,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position[0] > transform.position[0])
        {
            transform.Translate(Vector2.left * Time.deltaTime * 8);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * 8);
        }
    }
}
