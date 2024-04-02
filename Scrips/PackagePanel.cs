using Assets.Scrips;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PackageMode
{ 
    normal,
    delete,
    sort,
}
public class PackagePanel : BasePannel
{
    private Transform UiMenus;
    private Transform UiMenuWeapon;
    private Transform UiMenuFood;
    private Transform UiTabName;

    private Text UiMenuNum;
    private Text UiDeleNum;
    private Transform UiCloseButton;
    private Transform UiCenter;
    private Transform UiScrollView;
    public Transform UiContent;
    private Transform UiDetailPanel;
    private Transform UiLeftButton;
    private Transform UiRightButton;
    private Transform UiDeletePanel;
    private Transform UiDeleteBackButton;
    private Transform UiDeleteConfirmButton;
    private Transform UiBottomMenus;
    private Transform UiDeleteButton;
    private Transform UiDetailButton;

    public PackageMode curMode= PackageMode.normal;

    public GameObject PackageUiItemPrefeb;

    public  List<string> DeleteChooseuid;

    private string chooseuid;
    public string ChooseUid
    { get
        {
            return chooseuid;
        }
        set
        {
            chooseuid = value;
        }
    }

    private string choiceitemUid;
    public string ChoiceItemUid
    {
        set {
            choiceitemUid = value;
            this.RefreshDetail();
           
        }
        get
        {

            return choiceitemUid;
        }
    }
    //添加待删除物品
    public void AddChooseDeletedUid(string uid)
    {
        this.DeleteChooseuid ??= new List<string>();
        if(!this.DeleteChooseuid.Contains(uid))
        {
            this.DeleteChooseuid.Add(uid);
        }
        else
        {
            this.DeleteChooseuid.Remove(uid);
        }
        RefreshDeletePanel();
    }

    private void RefreshDeletePanel()
    {
        RectTransform scrollContent = UiScrollView.GetComponent<ScrollRect>().content;
        foreach (Transform Cell in scrollContent)
        {
            PackageCell Packagecell =Cell.GetComponent<PackageCell>();
            Packagecell.RefreshDeleteState();
        }
      
    }

    private  void RefreshUIText()
    {
        int i= GameManager.Instance.GetPackageLocalData().Count;
        UiMenuNum.text = string.Format("武器" + i + "/100");
    }
    public void RefreshUIDelete()
    {
        int All=GameManager.Instance.GetPackageLocalData().Count;
        int i = DeleteChooseuid.Count;
        UiDeleNum.text= string.Format("武器" + i + "/"+All);

    }
    override protected void Awake()
    {
        base.Awake();
        InitUi();
    }

    private void Start()
    {
        RefreshUI();
       
     
    }

    public void RefreshUI()
    {
        RefreshScroll();
        RefreshUIText();

    }
    public void RefreshScroll()//刷新滚动容器
    {
        //清理滚动容器中的所有物品
        RectTransform scrollContent = UiScrollView.GetComponent<ScrollRect>().content;
        for(int i=0; i<scrollContent.childCount;i++) 
        { 
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        foreach( PackageLocalItem packageLocalItem in GameManager.Instance.GetPackageLocalData() ) 
        {
           Transform PackageUIItem =Instantiate(PackageUiItemPrefeb.transform,scrollContent);
           PackageCell packageCell= PackageUIItem.GetComponent<PackageCell>();
            packageCell.Refesh(packageLocalItem, this);
        }
    }
    public void RefreshDetail()
    {
        //根据Uid获得动态数据
        PackageLocalItem packageLocalItem=GameManager.Instance.GetPackageItemByUid(choiceitemUid);
        UiDetailPanel.GetComponent<PackageDetail>().Refresh(packageLocalItem,this);
    }
    public void InitUi()
    {
        InitUiName();
        InitClick();
    }
    public void InitUiName()
    {
        //right:UiCloseButton关闭按钮、HasNum拥有武器数目
        UiCloseButton = transform.Find("RightTop/Close");
        UiMenuNum = transform.Find("RightTop/num").GetComponent<Text>();
        
        //TopCenter：UiMenus主菜单位置、UiMenuWeapon武器菜单，UiMenuFood食物菜单

        UiMenus = transform.Find("TopCenter/Menus");
        UiMenuWeapon = transform.Find("TopCenter/Menus/Weapon");
        UiMenuFood = transform.Find("TopCenter/Menus/Food");
        //LeftTop：当前菜单类别（武器、食物）
        UiTabName = transform.Find("LeftTop/TabName");


        //Center：UiCenter中心位置、UiScrollView滚动容器父物体，UiDetailPanel描述界面
        UiCenter = transform.Find("Center");
        UiScrollView = transform.Find("Center/ScrollView");
        UiContent = transform.Find("Center/ScrollView/Viewport/Content");
        UiDetailPanel = transform.Find("Center/DetailPanel");

        UiLeftButton = transform.Find("Left/LeftPackage");
        UiRightButton = transform.Find("Right/RightPakage");

        UiDeletePanel = transform.Find("Bottom/DeletePanel");
        UiDeleteBackButton = transform.Find("Bottom/DeletePanel/Back");
        UiDeleteConfirmButton = transform.Find("Bottom/DeletePanel/ConfirmButton");

        UiBottomMenus = transform.Find("Bottom/BottomMenus");
        UiDeleteButton = transform.Find("Bottom/BottomMenus/DeleteButton");
        UiDetailButton = transform.Find("Bottom/BottomMenus/DetailButton");
        UiDeleNum = transform.Find("Bottom/DeletePanel/InfoText").GetComponent<Text>();

        UiDeletePanel.gameObject.SetActive(false);
        UiBottomMenus.gameObject.SetActive(true);
    }
    public void InitClick()
    {
        UiMenuWeapon.GetComponent<Button>().onClick.AddListener(OnClickMenuWeapon);
        UiMenuFood.GetComponent<Button>().onClick.AddListener(OnClickMenuFood);
        UiCloseButton.GetComponent<Button>().onClick.AddListener(OnClickCloseButton);
        UiLeftButton.GetComponent<Button>().onClick.AddListener(OnClickLeftButton);
        UiRightButton.GetComponent<Button>().onClick.AddListener(OnClickRightButton);
        UiDeleteBackButton.GetComponent<Button>().onClick.AddListener(OnClickDeleteBackButton);
        UiDeleteConfirmButton.GetComponent<Button>().onClick.AddListener(OnClickiDeleteConfirmButton);
        UiDeleteButton.GetComponent<Button>().onClick.AddListener(OnClickDeleteButton);
        UiDetailButton.GetComponent<Button>().onClick.AddListener(OnClickDetailButton);
    }

    private void OnClickDetailButton()
    {
        print("1");
    }

    private void OnClickDeleteButton()
    {
        print("2");
        this.curMode = PackageMode.delete;
        UiDeletePanel.gameObject.SetActive(true);
        DeleteChooseuid = new List<string>();
        RefreshDeletePanel();
        
       
    }

    private void OnClickiDeleteConfirmButton()
    {
        print("3");
        if(this.DeleteChooseuid==null)
        {
            return;
        }
        if(this.DeleteChooseuid.Count==0) 
        {
            return; 
        }
        GameManager.Instance.DeletePackageItems(this.DeleteChooseuid);
        RefreshUI();
        DeleteChooseuid.Clear();
        RefreshUIDelete();
    }

    private void OnClickDeleteBackButton()
    {
        print("4");
        this.curMode = PackageMode.normal;
        UiDeletePanel.gameObject.SetActive(false);
        RefreshUI();
        RefreshUIText() ;
    }

    private void OnClickRightButton()
    {
        print("5");
    }

    private void OnClickLeftButton()
    {
        print("6");
    }

    private void OnClickCloseButton()
    {
        print("7");
        ClosePanel();
        UIManager.Instance.OpenPannel(UiConst.MainPanel);
    }

    private void OnClickMenuFood()
    {
        print("8");
    }

    private void OnClickMenuWeapon()
    {
        print("9");
    }
}


