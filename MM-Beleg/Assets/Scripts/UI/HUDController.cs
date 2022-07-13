using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class HUDController : MonoBehaviour
{

    public Player player;

    private int numOfHearts;
    private float startTime = 0;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Camera CurrentCamera;

    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public TextMeshProUGUI dimension;
    public TextMeshProUGUI weapon;

    void Start()
    {
        numOfHearts = player.GetHealth();

        for (int i = 0; i < numOfHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawHealthBar();
        UpdateScore();
        UpdateTime();
        UpdateCurrentDimension();
        UpdateEquippedWeapon();
        UpdateBackgroundColor();
    }

    private void UpdateBackgroundColor()
    {
        CurrentCamera.backgroundColor = DimensionColors.backgroundColors[(int)DimensionController.Instance.GetCurrentDimension()];
    }

    private void UpdateEquippedWeapon()
    {
        string weapon = "" + player.GetCurrentWeapon();
        weapon = weapon.ToUpper();
        this.weapon.text = "WEAPON  // " + weapon;
    }

    private void UpdateCurrentDimension()
    {
        dimension.text = "DIMENSION // " + DimensionController.Instance.GetCurrentDimension();
    }

    private void UpdateTime()
    {
        this.startTime += Time.deltaTime;
        float minutes = Mathf.Floor(startTime / 60);
        float seconds = startTime % 60;

        this.time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScore()
    {
        score.text = "" + LevelController.Instance.GetPoints();
    }

    private void DrawHealthBar()
    {
        for (int i = 0; i < numOfHearts; i++)
        {
            if (i < player.GetHealth())
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
