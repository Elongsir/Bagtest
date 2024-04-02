using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static  GameManager instance;
    private PackageTable packageTable;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public static GameManager Instance
    {
        get { 
            if(instance == null)
            {
                instance=new GameManager();
            }
            return instance;
        }
    }
    void Start()
    {
        List<PackageLocalItem> ReadItems = PackageLocalData.Instance.LoadPakcage();
        UIManager.Instance.OpenPannel(UiConst.MainPanel);
    }

    //删除物品
    public void DeletePackageItems(List<string > uids)
    {
        foreach(string uid in uids) 
        { 
            DeletePackageItem(uid,false);
        }
        PackageLocalData.Instance.SavePackage();
    }
    public void DeletePackageItem(string uid,bool NeedSave= true) 
    { 
        PackageLocalItem packagelocalItem=GetPackageItemByUid(uid);
        if(packagelocalItem==null)
        {
            return;
        }
        PackageLocalData.Instance.Items.Remove(packagelocalItem);
        if(NeedSave)
        {
            PackageLocalData.Instance.SavePackage();
        }
    }
   //获取表格数据（静态）
    public PackageTable GetPackageTable()
    {
        if(packageTable == null) 
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        }
        return packageTable;
    }
    //获取本地数据（动态）
    public List<PackageLocalItem> GetPackageLocalData()
    {
        //获取背包数据
        return PackageLocalData.Instance.LoadPakcage();
    }

    //通过类型筛选道具（type）——List
    public List<PackageTableItem> GetPackageDataByType(int  type)
    {
        List<PackageTableItem> packageTableItems = new List<PackageTableItem>();
        foreach(PackageTableItem item in GetPackageTable().DataList)
        {
            if(item.Type==type)
            {
                packageTableItems.Add(item);
            }
        }
        return packageTableItems;
    }

 //通过id筛选道具（id）——table
    public PackageTableItem GetPackageItemByid  (int id)
    {
        List<PackageTableItem> PackageDataList = GetPackageTable().DataList;
        foreach(PackageTableItem PackageData in PackageDataList)
        {
            if(PackageData.Id== id)
            {
                return PackageData;
            }
        }
        return null;    
    }
    //通过uid筛选道具（Uid）——localItem

    public PackageLocalItem GetPackageItemByUid(string uid)
    {
        List<PackageLocalItem> PacakgeDataList = GetPackageLocalData();
        foreach(PackageLocalItem PackageData in PacakgeDataList)
        {
            if(PackageData.Uid==uid)
            {
                return PackageData;
            }
        }
       return null; 
    }

    public PackageLocalItem GetLotteryRandom1()
    {
        List<PackageTableItem> packageTableItems = GetPackageDataByType(GameConst.PackageTypeWeapon);
        int index = Random.Range(0, packageTableItems.Count);
        PackageTableItem packageTableItem = packageTableItems[index];
        PackageLocalItem packageLocalItem = new PackageLocalItem()
        {
            Uid = System.Guid.NewGuid().ToString(),
            id = packageTableItem.Id,
            num = 1,
            IsNew = true,
            Level = 1,
        };
        PackageLocalData.Instance.Items.Add(packageLocalItem);
        PackageLocalData.Instance.SavePackage();
        return packageLocalItem;
    }
    public List<PackageLocalItem> GetLotteryRandom10()
    {
        List<PackageLocalItem> packageLocalItems=new List<PackageLocalItem>();
        for(int i = 0;i<10;i++)
        {
            PackageLocalItem item =GetLotteryRandom1();
            packageLocalItems.Add(item);
        }
        return packageLocalItems;
        
    }

}

public class PackageItemComparer:IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem a, PackageLocalItem b)
    {
       PackageTableItem x = GameManager.Instance.GetPackageItemByid(a.id);
        PackageTableItem y =GameManager.Instance.GetPackageItemByid(b.id);
        //比较a和b的星星多少（多返回1，少返回-1，相同返回0）
        int StarComparison=y.Stars.CompareTo(x.Stars);
        if(StarComparison==0)
        {
            int idComparision=y.Id.CompareTo(x.Id);
            if(idComparision==0) 
            {
                return b.Level.CompareTo(a.Level);
            }
            return idComparision;
        }
        return StarComparison;
        
    }
}
public class GameConst
{
    public const int PackageTypeWeapon = 1;
    public const int PackageTypeFood = 2;
}

