using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController main;

    [Header("Pecas")]   
    public int StartAmount;
    public int WinAmount;
    public GameObject PecaPrefab;
    [Header("Weapons")]
    public GameObject[] WeaponsPrefab;
    public float SpawnTime;
    [Header("UI")]
    public Canvas UICanvas;

    private PlayerBag[] players;
    private bool gameEnded;

    public bool ControlScene;

    void Start()
    {
        main = this;
        gameEnded = false;
        
        UICanvas.gameObject.SetActive(false);

        Vector3 spawnPosition;
        Vector3 lastSpawnPosition = Vector2.zero;
        for (int i = 0; i < StartAmount; i++)
        {
            do
            {
                spawnPosition = GetRandomPoint();
            } while (Vector3.Distance(lastSpawnPosition, spawnPosition) < 10);
            Instantiate(PecaPrefab, spawnPosition, Quaternion.identity);
        }

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
            yield return new WaitForSeconds(SpawnTime);

            GameObject weapon = WeaponsPrefab[Random.Range(0, WeaponsPrefab.Length)];
            Instantiate(weapon, GetRandomPoint(), Quaternion.identity);
        }
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
        GameObject winnerGameobject = winner.gameObject;
        gameEnded = true;
        foreach (var player in players)
        {
            Destroy(player.GetComponent<Rigidbody2D>());
            Destroy(player.GetComponent<PlayerMovement>());
            Destroy(player.GetComponent<AttackScript>());
            Destroy(player.GetComponent<PlayerBag>());
            Destroy(player.GetComponent<Player>());
        }

        UICanvas.gameObject.SetActive(true);

        StartCoroutine(WinCycle(winnerGameobject));
    }

    IEnumerator WinCycle(GameObject winner)
    {
        Vector2 origin = winner.transform.position;
        Vector2 target = Vector2.zero;

        float time = Vector3.Distance(origin, target) * .5f;

        while (origin != target)
        {
            origin = Vector2.MoveTowards(origin, target, time * Time.deltaTime);
            winner.transform.position = origin;

            Camera.main.orthographicSize -= 3f * Time.deltaTime;
            yield return null;
        }


    }

}
