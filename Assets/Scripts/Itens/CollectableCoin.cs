using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableBase
{
    public bool special = false;

    protected override void OnCollect()
    {
        base.OnCollect();
        if (special == false)
        {
            ItemManager.Instance.AddCoins();
        }
        else
        {
            ItemManager.Instance.AddCoinsSpecial();
        }

    }

}
