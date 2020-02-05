using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdate : MonoBehaviour
{
    public PlayerBag Player;
    public Sprite[] Pecas;

    private int lastPecas;
    private Image startImage;

    void Start()
    {
        lastPecas = Player.Itens;
        startImage = GetComponent<Image>();
    }

    void Update()
    {
        if (Player == null) return;
        if (lastPecas != Player.Itens)
        {
            lastPecas = Player.Itens;
            startImage.sprite = Pecas[Player.Itens];
        }

    }
}
