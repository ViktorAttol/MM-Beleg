using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays Player Points as currency available for use in shop.
/// </summary>
public class ShopMoney : MonoBehaviour
{
    private TextMeshProUGUI money;

    void Start()
    {
        money = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        money.text = "$ " + LevelDataHandler.GetCurrentPlayerPoints();
    }
}
