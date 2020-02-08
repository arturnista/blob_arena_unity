using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPeace : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        gameObject.tag = Tags.UNTAGGED;
        yield return new WaitForSeconds(.1f);
        gameObject.tag = Tags.STAR_PEACE;
    }

}
