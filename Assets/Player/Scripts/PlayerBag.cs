using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerBag : MonoBehaviour
{
    public GameObject peca;

    private int itens;
    public int Itens { get => itens; }

    private PlayerMovement movement;

    void Start()
    {
        itens = 0;
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Peca")
        {
            Destroy(collision.gameObject);
            itens++;
        }
    }

    public void DropPeca(Transform damager, float force, bool bulletHit = false)
    {
        CameraShaker.Instance.ShakeOnce(5f, 10f, .1f, .5f);
        PlayerMovement damagerMovement = damager.GetComponent<PlayerMovement>();
        Vector2 dir = (transform.position - damager.position).normalized;

        if (damagerMovement != null)
        {
            if(movement.IsGrounded && damagerMovement.IsGrounded)
            {
                movement.AddExtraVelocity(new Vector2(dir.x * force, Random.Range(0.5f, 1.0f) * force));
            }
            else
            {
                movement.AddExtraVelocity(dir * force);
            }
        }
        else
        {
            movement.AddExtraVelocity(dir * force);
        }

        if(itens <= 0) return;

        if (dir.x < 0)
        {
            GameObject help;
            help = Instantiate(peca, transform.position + transform.up, Quaternion.identity);

            Vector2 vec = new Vector2(Random.Range(-1.0f, 0.0f), Random.Range(0.5f, 1.0f));
            help.GetComponent<Rigidbody2D>().AddForce(vec * 50f, ForceMode2D.Impulse);

            itens--;
        }
        else
        {
            GameObject help;
            help = Instantiate(peca, transform.position + transform.up, Quaternion.identity);
            Vector2 vec = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f));
            help.GetComponent<Rigidbody2D>().AddForce(vec * 50f, ForceMode2D.Impulse);
            itens--;
        }

        if (bulletHit)
        {
            StartCoroutine(StunCycle());
        }
        
    }

    IEnumerator StunCycle()
    {
        movement.IsStopped = true;
        yield return new WaitForSeconds(1f);
        movement.IsStopped = false;
    }
}
