using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ue : MonoBehaviour
{

    public GameObject peca;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized); // testar a posição do player com o outro

        if (Input.GetMouseButtonDown(0))
        {
            GameObject help;
            help = Instantiate(peca);
           
            Vector2 vec = new Vector2(Random.Range(-1.0f,0.0f), Random.Range(0.5f, 1.0f));
            help.GetComponent<Rigidbody2D>().AddForce(vec * 500);
           
        } 
        if(Input.GetMouseButtonDown(1))
        {
            GameObject help;
            help = Instantiate(peca);
            Vector2 vec = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f));
            help.GetComponent<Rigidbody2D>().AddForce(vec * 500);
            Debug.Log(Input.mousePosition - transform.position);
            // Debug.Log(transform.up);
        }
        if( Input.GetKey(KeyCode.Space))
        {
          
        }
    }
}
