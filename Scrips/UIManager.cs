using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance;
    private Transform uiroot;
    //路径配置字典
    private Dictionary<string, string> pathDict;
    //预制件缓存字典
    private Dictionary<string, GameObject> prefebDict;
    //已打开界面的缓存字典
    public Dictionary<string, BasePannel> panelDict;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    public Transform Uiroot
    {
        get { 
            if(uiroot == null) 
            { 
                if(GameObject.Find("Canvas"))
                {
                    uiroot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    uiroot =new GameObject("Canvas").transform;
                }
            } 
            return uiroot;
        }
    }
    private UIManager()
    {
        InitDicts();
    }
    private void InitDicts()
    {
        prefebDict=new Dictionary<string, GameObject> ();
        panelDict=new Dictionary<string, BasePannel> ();
        pathDict = new Dictionary<string, string>()
        {
            {UiConst.packagePanel,"Package/PackagePanel" },
            {UiConst.LottreyPanel,"Lottery/LotteryPanel" },
            {UiConst.MainPanel,"MainPanel" },
        };

    }

    public BasePannel GetPannel(string name)
    {
        BasePannel pannel =null;
        //检查是否打开
        if(panelDict.TryGetValue(name,out pannel))
        {
            return pannel;
        }
        return null;
    }

    public BasePannel OpenPannel(string name)
    {
        BasePannel pannel =null;
        if(panelDict.TryGetValue (name,out pannel))
        {
            Debug.Log("界面已打开" + name);
                 return null;
        }
        string path = "";
        if(!pathDict.TryGetValue(name,out path))
        {
            Debug.LogError("界面名称错误或未配置路径：" + name);
            return null;
        }
        GameObject pannelPrefeb = null;
        if(!prefebDict.TryGetValue (name,out pannelPrefeb))
        {
            string realPath = "prefeb/Panel/" + path;
            pannelPrefeb=Resources.Load<GameObject>(realPath) as GameObject;
            if(pannelPrefeb==null)
            {
                Debug.Log("配置表写错了,重写");
            }
            prefebDict.Add (name, pannelPrefeb);
        }
        GameObject pannelObject = GameObject.Instantiate(pannelPrefeb, Uiroot, false);
        pannel=pannelObject.GetComponent<BasePannel>();
        panelDict.Add (name, pannel);
        pannel.OpenPennel(name);
        return pannel;
    }

    public bool ClosePanel(string name)
    {
        BasePannel pannel =null ;
        if(!panelDict.TryGetValue(name,out pannel))
        {
            Debug.Log("界面已关闭：" + name);
            return false;
        }
        pannel.ClosePanel ();
        return true;
    }

   
}
public class UiConst
{
    //menu Pannel
    public const string packagePanel = "packagePannel";
    public const string LottreyPanel = "LottreyPanel";
    public const string MainPanel = "MainPanel";
}
