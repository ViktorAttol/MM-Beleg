using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    MAIN, SHOP, EXIT 
}

public class MainMenuStateMachine : MonoBehaviour
{
    private MenuState _menuState = MenuState.MAIN;
    private GameObject _activeView = null;
    
    public GameObject MainView;
    public GameObject ShopView;
    public GameObject ProgressView;
    public GameObject OptionView;

    private void Start()
    {
        _activeView = MainView;
    }

    private void SetMenuView()
    {
        switch (_menuState)
        {
            case MenuState.MAIN:
                SetView(MainView);
                break;
            case MenuState.SHOP:
                SetView(ShopView);
                break;
            case MenuState.EXIT:
                break;
        }
    }

    private void SetView(GameObject view)
    {
        _activeView.SetActive(false);
        _activeView = view;
        _activeView.SetActive(true);
    }

    public void PlayBtnClicked()
    {
        Debug.Log("play");
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void ShopBtnClicked()
    {
        Debug.Log("shop");
        _menuState = MenuState.SHOP;
        SetMenuView();
    }

    public void ExitBtnClicked()
    {
        Debug.Log("exit");
        _menuState = MenuState.EXIT;
        SetMenuView();
    }

    public void BackBtnClicked()
    {
        _menuState = MenuState.MAIN;
        SetMenuView();
    }
}
