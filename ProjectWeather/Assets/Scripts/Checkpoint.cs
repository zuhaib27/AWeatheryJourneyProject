using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Variables")]
    public float SecondsDelayBeforeDestroy = 0.6f;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(other.transform.position);
            StartCoroutine(DelayBeforeDestroyEnumerator());
        }
    }


    private IEnumerator DelayBeforeDestroyEnumerator()
    {
        yield return new WaitForSeconds(SecondsDelayBeforeDestroy);
        Destroy(this.gameObject);
    }
}
