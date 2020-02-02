using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIupdate : MonoBehaviour
{
    public PlayerBag Player;
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
        if (Player == null) return;
        if(Npeca < 5)
        {
            if (Player.Itens > Npeca)
            {
                Pecas[Npeca].SetActive(true);
                Npeca++;
            }

            if(Player.Itens < Npeca)
            {
                Npeca--;
                Pecas[Npeca].SetActive(false);
            }
        }

    }
}
