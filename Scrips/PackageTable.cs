using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Xiaoqi/PakageTable", fileName = "PakageTable")]
public class PackageTable : ScriptableObject
{
    public List<PackageTableItem> DataList= new List<PackageTableItem>();

}
[Serializable]
public class PackageTableItem
{
    public int Id;
    public int Type;
    public int Stars;
    public string Name;
    public string Description;
    public string SkillDescription;
    public string ImagePath;
}

