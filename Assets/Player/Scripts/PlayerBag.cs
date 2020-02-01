using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public int itens; 

    // Start is called before the first frame update
    void Start()
    {
        itens = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Peca")
        {
            Destroy(collision.gameObject);
            itens++;
            if(itens >=5 )
            {
                //win condition
            }
        }
    }
}
