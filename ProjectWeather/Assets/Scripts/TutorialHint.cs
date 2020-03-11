using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

public class TutorialHint : MonoBehaviour
{
    [Header("Variables")]
    public float SecondsDelayBeforeDestroy = 0.6f;

    public GameObject tip;
    public Button waitButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TutorialManager.Instance.DisplayTip(tip, waitButton);
            StartCoroutine(DelayBeforeDestroyEnumerator());
        }
    }


    private IEnumerator DelayBeforeDestroyEnumerator()
    {
        yield return new WaitForSeconds(SecondsDelayBeforeDestroy);
        Destroy(this.gameObject);
    }
}
