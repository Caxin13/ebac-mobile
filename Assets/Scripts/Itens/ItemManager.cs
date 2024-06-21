using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI uiTextCoins;

    public SOInt coinsSpecial;
    public TextMeshProUGUI uiTextCoinsSpecial;



    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        coinsSpecial.value = 0;
        //UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
       //UpdateUI();
    }

    public void AddCoinsSpecial(int amount = 5)
    {
        coins.value += amount;
        coinsSpecial.value += amount / 5;
        //UpdateUI();
    }

    private void UpdateUI()
    {
        //uiTextCoins.text = coins.ToString();
        //UIInGameManager.UpdateTextCoins(coins.ToString());
    }

}
