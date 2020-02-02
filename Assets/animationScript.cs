using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    Animator anim;
    public Sprite[] spritesBody;
    int arrayCount;
    // Start is called before the first frame update
    void Start()
    {
        arrayCount = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jumped");
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (arrayCount < spritesBody.Length)
            {
                arrayCount++;
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spritesBody[arrayCount];
            }
            else
            {
                arrayCount = 0;
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spritesBody[arrayCount];
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("tookDmg");
        }
    }
}
