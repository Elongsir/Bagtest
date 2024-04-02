using System;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData
{
   private static PackageLocalData instance;
    public static PackageLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
               
            return instance;
        }
    }

    public List<PackageLocalItem> Items;
    public void SavePackage()
    {
        string inventoryjson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PackageLocalData", inventoryjson);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPakcage()
    {
        if(Items!=null)
        {
            return Items;
        }
        if(PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData pakacageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            Items= pakacageLocalData.Items;
            return Items;

        }
        else
        {
            Items = new List<PackageLocalItem>();
            return Items;
        }
    }
}
[Serializable]
public class PackageLocalItem
{
    public string Uid;
    public int id;
    public int num;
    public int Level;
    public bool IsNew;
    public override string ToString()
    {
        return string.Format("[id]:{0},[num]:{1}", id, num);
    }
}
