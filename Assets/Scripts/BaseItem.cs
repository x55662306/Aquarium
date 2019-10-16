using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem
{
    public int id;         //物品ID
    public string name;    //物品名稱
    
    public BaseItem(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public BaseItem(ObjectIten iten)
    {
        this.id = iten.id;
        this.name = iten.name;
    }

    public BaseItem()
    {
    }
}
