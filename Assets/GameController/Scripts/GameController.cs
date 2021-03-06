﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController main;

    [Header("Pecas")]   
    public int StartAmount;
    public int WinAmount;
    public GameObject PecaPrefab;
    [Header("Weapons")]
    public int MaxWeapons;
    public GameObject[] WeaponsPrefab;
    public float SpawnTime;
    [Header("Musics")]
    public AudioSource MusicSource;
    public AudioClip FinalSfx;
    [Header("UI")]
    public SelectCharacter SelCharacter;
    public Canvas NormalUICanvas;
    public Canvas WinUICanvas;
    public Sprite P1WinText;
    public Sprite P2WinText;

    private PlayerBag[] players;
    private bool gameEnded;
    public bool GameEnded { get => gameEnded; }

    public bool ControlScene;

    [Header("SpawnPoints")]
    public List<Transform> Left;
    public List<Transform> Right;    
    public Transform Mid;
    private int aux;

    private int weaponsCreated;

    void Start()
    {
        main = this;
        gameEnded = false;
        
        if(!ControlScene)
        {
             WinUICanvas.gameObject.SetActive(false);

             for(int i =0; i < 3; i++)
             {
                 aux = Random.Range(0, Left.Count);
                 Instantiate(PecaPrefab, Left[aux].position, Quaternion.identity);
                 Left.RemoveAt(aux);

                 aux = Random.Range(0, Right.Count);
                 Instantiate(PecaPrefab, Right[aux].position, Quaternion.identity);
                 Right.RemoveAt(aux);
             }

        }

        //Instantiate(PecaPrefab, Mid.position, Quaternion.identity);

        players = GameObject.FindObjectsOfType<PlayerBag>();
        StartCoroutine(SpawnWeaponCycle());
    }

    Vector2 GetRandomPoint()
    {
        Vector2 minPosition = new Vector2(-17, -9);
        Vector2 maxPosition = new Vector2(17, 9);

        return new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            0f
        );
    }

    IEnumerator SpawnWeaponCycle()
    {
        while (!gameEnded)
        {
            yield return new WaitForSecondsPausable(SpawnTime);

            if (weaponsCreated < MaxWeapons)
            {
                GameObject weapon = WeaponsPrefab[Random.Range(0, WeaponsPrefab.Length)];
                Instantiate(weapon, GetRandomPoint(), Quaternion.identity);
                weaponsCreated += 1;
            }
        }
    }

    public void PickupWeapon()
    {
        weaponsCreated -= 1;
    }

    void LateUpdate()
    {
        if (gameEnded) return;
        foreach (var player in players)
        {
            if (player.Itens >= WinAmount)
            {
                if(!ControlScene)
                {
                    WinGame(player);
                }
            }
        }
    }

    void WinGame(PlayerBag winner)
    {
        MusicSource.Stop();
        GameObject loserGameObject = null;
        GameObject winnerGameObject = winner.gameObject;
        gameEnded = true;
        foreach (var player in players)
        {
            Destroy(player.GetComponent<Rigidbody2D>());
            Destroy(player.GetComponent<PlayerMovement>());
            Destroy(player.GetComponent<PlayerAttack>());
            Destroy(player.GetComponent<PlayerBag>());
            Destroy(player.GetComponent<Player>());
            if (winnerGameObject != player.gameObject)
            {
                loserGameObject = player.gameObject;
            }
        }

        MusicSource.volume = .3f;
        MusicSource.PlayOneShot(FinalSfx);

        StartCoroutine(WinCycle(winnerGameObject, loserGameObject));
    }

    IEnumerator WinCycle(GameObject winner, GameObject loser)
    {
        NormalUICanvas.gameObject.SetActive(false);
        WinUICanvas.gameObject.SetActive(true);
        WinUICanvas.transform.Find("WinText").GetComponent<Image>().sprite = winner.tag == Tags.PLAYER1 ? P1WinText : P2WinText;
        Image loserImage = WinUICanvas.transform.Find("Loser/Sprite").GetComponent<Image>();
        Image winnerImage = WinUICanvas.transform.Find("Winner/Sprite").GetComponent<Image>();

        SetSprite(winnerImage, winner);
        SetSprite(loserImage, loser);

        if (SelCharacter.Player[0] == SelCharacter.Player[1])
        {
            if (winner.tag == Tags.PLAYER1)
            {
                WinUICanvas.transform.Find("Winner/BowTie").gameObject.SetActive(false);
            }
            else
            {
                WinUICanvas.transform.Find("Loser/BowTie").gameObject.SetActive(false);
            }
        }
        else
        {
            WinUICanvas.transform.Find("Winner/BowTie").gameObject.SetActive(false);
            WinUICanvas.transform.Find("Loser/BowTie").gameObject.SetActive(false);
        }

        CanvasGroup group = WinUICanvas.GetComponent<CanvasGroup>();
        group.interactable = false;
        float alpha = 0f;

        while (alpha < 1f)
        {
            group.alpha = alpha;
            alpha += 2f * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        group.interactable = true;

    }

    void SetSprite(Image image, GameObject player)
    {
        int pos = player.tag == Tags.PLAYER1 ? 0 : 1;
        PlayerChangeSprite movement = player.GetComponent<PlayerChangeSprite>();
        image.sprite = movement.SelectCharacter.GetPlayerSprite(pos);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
