using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPeca : MonoBehaviour
{
    public Transform[] Positions;
    public GameObject Peca;
    public float MinTime, MaxTime;

    private float SpawnTime;
    private float Delay;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = 0;
        Delay = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTime += Time.deltaTime;

        if(SpawnTime >= Delay)
        {
            Instantiate(Peca, Positions[Random.Range(0, Positions.Length)]); 
            SpawnTime = 0;
        }

    }
}
