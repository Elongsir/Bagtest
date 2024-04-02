using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance;
    private Transform uiroot;
    //·�������ֵ�
    private Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefebDict;
    //�Ѵ򿪽���Ļ����ֵ�
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
        //����Ƿ��
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
            Debug.Log("�����Ѵ�" + name);
                 return null;
        }
        string path = "";
        if(!pathDict.TryGetValue(name,out path))
        {
            Debug.LogError("�������ƴ����δ����·����" + name);
            return null;
        }
        GameObject pannelPrefeb = null;
        if(!prefebDict.TryGetValue (name,out pannelPrefeb))
        {
            string realPath = "prefeb/Panel/" + path;
            pannelPrefeb=Resources.Load<GameObject>(realPath) as GameObject;
            if(pannelPrefeb==null)
            {
                Debug.Log("���ñ�д����,��д");
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
            Debug.Log("�����ѹرգ�" + name);
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
