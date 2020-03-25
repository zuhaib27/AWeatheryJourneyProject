using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Variables")]
    public float SecondsDelayBeforeDestroy = 0.6f;

    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(respawnPoint);
            StartCoroutine(DelayBeforeDestroyEnumerator());
        }
    }


    private IEnumerator DelayBeforeDestroyEnumerator()
    {
        yield return new WaitForSeconds(SecondsDelayBeforeDestroy);
        Destroy(this.gameObject);
    }
}
