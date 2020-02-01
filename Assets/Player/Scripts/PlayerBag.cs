using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public GameObject peca;

    private int itens;
    public int Itens { get => itens; }

    void Start()
    {
        itens = 0;
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

    public void DropPeca(int side)//0 - esq || 1 - dir
    {
        if(itens >0)
        {
            if (side == 0)
            {
                GameObject help;
                help = Instantiate(peca, transform.position + transform.up, Quaternion.identity);

                Vector2 vec = new Vector2(Random.Range(-1.0f, 0.0f), Random.Range(0.5f, 1.0f));
                help.GetComponent<Rigidbody2D>().AddForce(vec * 500);

                itens--;
            }
            else
            {
                GameObject help;
                help = Instantiate(peca, transform.position + transform.up, Quaternion.identity);
                Vector2 vec = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f));
                help.GetComponent<Rigidbody2D>().AddForce(vec * 500);
                itens--;

            }
        }
        
    }
}
