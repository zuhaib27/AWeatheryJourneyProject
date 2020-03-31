using UnityEngine;

public class EnableBehaviourOnTrigger : MonoBehaviour
{
    public string triggerTag;
    public MonoBehaviour behaviourToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            behaviourToEnable.enabled = true;
        }
    }
}
