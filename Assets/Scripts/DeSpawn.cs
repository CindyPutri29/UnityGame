using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class DeSpawn : MonoBehaviour
{
    public float delayToDespawn = 3;

    private void OnEnable()
    {
        StartCoroutine(IEDelayToSpawn());
        IEnumerator IEDelayToSpawn()
        {
            yield return new WaitForSeconds(delayToDespawn);
            LeanPool.Despawn(this);
        }
    }

}
