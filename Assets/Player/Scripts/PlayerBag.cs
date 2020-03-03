using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerBag : MonoBehaviour
{
    public GameObject peca;

    private int itens;
    public int Itens { get => itens; }

    private Player player;
    private PlayerMovement movement;
    public Animator anim;
    public AudioClip PickupClip;
    private AudioSource source;

    void Start()
    {
        itens = 0;
        movement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
        source = GetComponent<AudioSource>();
    }

    public void CollectPeca(Transform peca)
    {
        source.PlayOneShot(PickupClip);
        Destroy(peca.gameObject);
        itens++;
    }

    public void DropPeca(Transform damager, float force, bool shouldStun = false)
    {
        CameraShaker.Instance.ShakeOnce(5f, 10f, .1f, .5f);
        PlayerMovement damagerMovement = damager.GetComponent<PlayerMovement>();
        Vector2 dir = (transform.position - damager.position).normalized;


        if(anim.GetBool("isCharging"))
        {
            anim.SetBool("isCharging", false);
            GetComponent<PlayerAttack>().ChangeChargeState();
        }
        if(GetComponent<PlayerAttack>().IsReady)
            GetComponent<PlayerAttack>().IsReady = false;

        anim.SetTrigger("tookDmg");


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

        GameObject pecaCreated = Instantiate(peca, transform.position + (transform.up * 2.0f), Quaternion.identity);

        Vector2 hitDirection;
        if (dir.x < 0)
        {
            hitDirection = new Vector2(Random.Range(-1.0f, 0.0f), Random.Range(0.5f, 1.0f));
        }
        else
        {
            hitDirection = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f));
        }

        pecaCreated.GetComponent<Rigidbody2D>().AddForce(hitDirection * 50f, ForceMode2D.Impulse);
        itens--;

        if (shouldStun)
        {
            StartCoroutine(StunCycle());
        }
        
    }

    IEnumerator StunCycle()
    {
        movement.IsStopped = true;
        player.IsStopped = true;
        
        yield return new WaitForSecondsPausable(.5f);
        
        movement.IsStopped = false;
        player.IsStopped = false;
    }
}
