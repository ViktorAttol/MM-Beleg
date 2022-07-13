using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMoney : MonoBehaviour
{
    public TextMeshProUGUI money;

    // Update is called once per frame
    void Update()
    {
        money.text = "$ " + LevelDataHandler.currentPlayerPoints;
    }
}
