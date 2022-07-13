using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMoney : MonoBehaviour
{
    private TextMeshProUGUI money;

    void Start()
    {
        money = GetComponent<TextMeshProUGUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        money.text = "$ " + LevelDataHandler.GetCurrentPlayerPoints();
    }
}
