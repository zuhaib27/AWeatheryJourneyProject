using System.Collections;
using Assets.Player.Scripts;
using UnityEngine;

public class PlatformLauncher : MonoBehaviour
{
    [Header("Variables")]
    public Vector3 DirectionVector = Vector3.up;
    public float Magnitude = 60f;
    [Range(0.0f, 2.5f)]
    public float SecondsDelayBeforeLaunch = 0.6f;

    //public ParticleSystem ParticleSystem;  // TODO: could sync animations with launch

    // Private variables
    private bool _onTrigger = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Stepped on platform");
            _onTrigger = true;
            StartCoroutine(DelayBeforeLaunchEnumerator(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _onTrigger = false;
    }

    private IEnumerator DelayBeforeLaunchEnumerator(Collider other)
    {
        yield return new WaitForSeconds(SecondsDelayBeforeLaunch);

        // Check if they are still on the trigger after delay
        if (_onTrigger)
        {
            MyCharacterController cc = other.GetComponent<MyCharacterController>();
            cc.LaunchPlayer(DirectionVector.normalized, Magnitude);
        }
    }
}
