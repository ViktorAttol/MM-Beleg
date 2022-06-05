using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuState
{
    MAIN, PLAY, SHOP, PROGRESS, OPTIONS, EXIT 
}

public class MainMenuStateMachine : MonoBehaviour
{
    private MenuState _menuState = MenuState.MAIN;
    private GameObject _activeView = null;
    
    public GameObject MainView;

    public GameObject PlayView;
    
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
            case MenuState.PLAY:
                SetView(PlayView);
                break;
            case MenuState.SHOP:
                SetView(ShopView);
                break;
            case MenuState.PROGRESS:
                SetView(ProgressView);
                break;
            case MenuState.OPTIONS:
                SetView(OptionView);
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
        _menuState = MenuState.PLAY;
        SetMenuView();
    }

    public void ShopBtnClicked()
    {
        Debug.Log("shop");
        _menuState = MenuState.SHOP;
        SetMenuView();
    }

    public void ProgressBtnClicked()
    {
        Debug.Log("progress");
        _menuState = MenuState.PROGRESS;
        SetMenuView();
    }
    
    public void OptionBtnClicked()
    {
        Debug.Log("options");
        _menuState = MenuState.OPTIONS;
        SetMenuView();
    }

    public void ExitBtnClicked()
    {
        Debug.Log("exit");
        _menuState = MenuState.EXIT;
        SetMenuView();
    }

    public void BackbtnClicked()
    {
        Debug.Log("exit");
        _menuState = MenuState.MAIN;
        SetMenuView();
    }
}
