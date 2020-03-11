using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject parentMenu = null;
    public GameObject parentButton = null;

    private EventSystem _eventSystem;

    private const string _backInput = "Back";

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
        if (Input.GetButtonDown(_backInput))
        {
            GoBack();
        }
    }

    public virtual void GoBack()
    {
        if (parentMenu != null)
        {
            parentMenu.SetActive(true);

            if (parentButton != null)
            {
                _eventSystem = UIManager.Instance.GetEventSystem();
                _eventSystem.SetSelectedGameObject(parentButton);
            }

            gameObject.SetActive(false);
        }
    }
}
