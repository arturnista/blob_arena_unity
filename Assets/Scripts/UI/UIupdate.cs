using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIupdate : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Pecas;

    private int Npeca;

    // Start is called before the first frame update
    void Start()
    {
        Npeca = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Npeca < 5)
        {
            if (Player.GetComponent<PlayerBag>().Itens > Npeca)
            {
                Pecas[Npeca].SetActive(true);
                Npeca++;
            }

            if(Player.GetComponent<PlayerBag>().itens < Npeca)
            {
                Pecas[Npeca].SetActive(false);
                Npeca--;
            }
        }
        
    }
}
