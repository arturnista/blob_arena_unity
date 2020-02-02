using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{

    public void DisableForTime()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(.1f);
        gameObject.tag = "Peca";
    }

}
