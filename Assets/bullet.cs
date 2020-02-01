using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float spd;
    bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + spd * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
