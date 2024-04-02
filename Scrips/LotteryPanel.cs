using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryPanel : BasePannel
{
    private  Transform Close;
    private  Transform Lottery1;
    private  Transform Lottery10;
    private  Transform Center;
    private GameObject LotteryPrefeb;
    protected override void Awake()
    {
        base.Awake();
        InitName();
    }
    public void InitName()
    {
        Close = transform.Find("TopRight/Close");
        Lottery1 = transform.Find("Bottom/Lottery1");
        Lottery10 = transform.Find("Bottom/Lottery10");
        Center = transform.Find("Center");
        LotteryPrefeb = Resources.Load<GameObject>("Prefeb/Panel/Lottery/LotteryItem");
        InitUI();
    }
    public void InitUI()
    {
        Close.GetComponent<Button>().onClick.AddListener(OnclosePanel);
        Lottery1.GetComponent<Button>().onClick.AddListener(OnClickLottery1);
        Lottery10.GetComponent<Button>().onClick.AddListener(OnClickLottery10);
        for (int i = 0; i < Center.childCount; i++)
        {
            Destroy(Center.GetChild(i).gameObject);
        }
    }
    private void OnClickLottery1()
    {
        Debug.Log("=>>>OnclickLottry1");
        for(int i = 0;i<Center.childCount;i++) 
        {
            Destroy(Center.GetChild(i).gameObject);
        }
        PackageLocalItem item = GameManager.Instance.GetLotteryRandom1();
        Transform lotteryItemTran = Instantiate(LotteryPrefeb.transform, Center);
        
        LotteryCell lotteryItem=lotteryItemTran.GetComponent<LotteryCell>();
        lotteryItem.Refresh(item,this);
    }
    private void OnClickLottery10()
    {
        Debug.Log("=>>>OnclickLottry10");
        List<PackageLocalItem> items = GameManager.Instance.GetLotteryRandom10(); 
        for (int i = 0; i < Center.childCount; i++)
        {
            Destroy(Center.GetChild(i).gameObject);
        }
        foreach (PackageLocalItem item in items)
        {
            Transform lotteryItemTran = Instantiate(LotteryPrefeb.transform, Center);
            LotteryCell lotteryItem = lotteryItemTran.GetComponent<LotteryCell>();
            lotteryItem.Refresh(item, this);
        } 
    }
    private void OnclosePanel()
    {
        ClosePanel();
        UIManager.Instance.OpenPannel(UiConst.MainPanel);
    }
}
