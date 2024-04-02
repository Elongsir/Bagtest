using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePannel
{
    public Transform LotteryPanelBut;
    public Transform BagPanelBut;
    public Transform ExitBut;
    protected override  void Awake()
    {
        base.Awake();
        InitName();
    }
    private void InitName()
    {
        LotteryPanelBut = transform.Find("Top/LotteryButton");
        BagPanelBut = transform.Find("Top/PackageButton");
        ExitBut = transform.Find("LeftBottom/ExitGame");

        LotteryPanelBut.GetComponent<Button>().onClick.AddListener(OnLotteryButton);
        BagPanelBut.GetComponent<Button>().onClick.AddListener(OnBagButton);
        ExitBut.GetComponent<Button>().onClick.AddListener(OnExitButton);
    }
    private void OnLotteryButton()
    {
        print(">>>>OnLotteryButton");
        UIManager.Instance.OpenPannel(UiConst.LottreyPanel);
        ClosePanel();
    }
    private void OnBagButton()
    {
        print(">>>>OnBagButton");
        UIManager.Instance.OpenPannel(UiConst.packagePanel);
        ClosePanel();
    }
    private void OnExitButton()
    {
        print(">>>>OnExitButton");
        EditorApplication.isPaused = false;
        Application.Quit();
    }
}
