using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public int StartAmount;
    public int WinAmount;
    public GameObject PecaPrefab;

    void Start()
    {
        Vector2 minPosition = new Vector2(-17, -9);
        Vector2 maxPosition = new Vector2(17, 9);

        for (int i = 0; i < StartAmount; i++)
        {
            Instantiate(PecaPrefab, new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                0f
            ), Quaternion.identity);
        }
    }

    

}
