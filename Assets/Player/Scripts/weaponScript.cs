using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform weaponPos, spawnBulletPoint;
    public GameObject bullet;
    bool isEquiped;
    void Start()
    {
        isEquiped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEquiped)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
               Shoot();
              
            }
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            collision.transform.SetParent(weaponPos.transform);
            collision.transform.localPosition = Vector2.zero;
            isEquiped = true;
        }
    }
    void Shoot()
    {
        Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
        isEquiped = false;
        GameObject obj = weaponPos.GetChild(0).gameObject;
        Destroy(obj);
        Debug.Log("Destroy");
        
    }
}
