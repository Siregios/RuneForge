using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShopItemButton : ItemButton
{
    public override void Initialize(Item item, List<Action<Item>> buttonFunctions)
    {
        base.Initialize(item, buttonFunctions);
    }
}