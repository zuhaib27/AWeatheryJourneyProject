using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Player.Scripts;
using AWeatheryJourney;

public class Menu : MonoBehaviour
{
    public Menu parentMenu = null;
    public GameObject parentButton = null;
    public GameObject defaultButton = null;

    public Transform atlasPosition = null;

    private EventSystem _eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        _eventSystem = UIManager.Instance.GetEventSystem();

        if (_eventSystem == null)
        {
            Debug.LogWarning("EventSystem is necessary when using menu UI.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonMappings.GetButtonDown(AWeatheryJourney.Button.Back))
        {
            GoBack();
        }
    }

    public virtual void GoTo(Menu menu)
    {
        menu.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    public virtual void GoBack()
    {
        if (parentMenu != null)
        {
            GoTo(parentMenu);
        }
    }

    private void OnEnable()
    {
        _eventSystem = UIManager.Instance.GetEventSystem();
        _eventSystem.SetSelectedGameObject(defaultButton);

        if (atlasPosition != null)
        {
            MyCharacterController cc = FindObjectOfType<MyCharacterController>();
            cc.Motor.SetPositionAndRotation(atlasPosition.position, atlasPosition.rotation);
        }
    }
}
