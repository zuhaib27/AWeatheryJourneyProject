using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISession : MonoBehaviour
{
    public Menu defaultMenu;

    private Menu[] _menus;

    private void Awake()
    {
        _menus = GetComponentsInChildren<Menu>();
    }

    private void OnEnable()
    {
        foreach (Menu menu in _menus)
        {
            menu.gameObject.SetActive(false);
        }

        defaultMenu.gameObject.SetActive(true);
    }
}
