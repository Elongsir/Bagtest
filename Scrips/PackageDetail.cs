using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    private Transform Stars;
    private Transform Describe;
    private Transform Title;
    private Transform LevelText;
    private Transform SkillDescribe;
    private Transform UIIcon;

    private PackageLocalItem PackageLocaltem;
    private PackageTableItem PackageTableItem;
    private PackagePanel UiParent;


    private void Awake()
    {
        InitUiName();
   
    }

    public void Test()
    {
        Refresh(GameManager.Instance.GetPackageLocalData()[1], null);
    }

    public void InitUiName()
    {
        Stars = transform.Find("Center/Stars");
        Describe = transform.Find("Center/Describe");
        Title=transform.Find("Top/Title");
        LevelText = transform.Find("Bottom/LevelPnl/LevelText");
        SkillDescribe = transform.Find("Bottom/BottomBg/SkillText");
        UIIcon = transform.Find("Center/Icon");
    }
    public void Refresh(PackageLocalItem packageLocalItem,PackagePanel UiParent)
    {
        //��ʼ����̬��̬���ڵ�
        this.PackageLocaltem = packageLocalItem;
        this.PackageTableItem=GameManager.Instance.GetPackageItemByid(PackageLocaltem.id);
        this.UiParent = UiParent;
        //��ʼ���ȼ�
        this.LevelText.GetComponent<Text>().text = string.Format("Lv:{0}/40",this.PackageLocaltem.Level.ToString());
        //����
        this.Describe.GetComponent<Text>().text=string .Format("���£�{0}",this.PackageTableItem.Description.ToString());
        //����
        this.Title.GetComponent<Text>().text = string.Format(this.PackageTableItem.Name);
        //Ч������
        this.SkillDescribe.GetComponent<Text>().text=string.Format(this.PackageTableItem.SkillDescription.ToString());
        //ͼƬ����
        Texture2D t = Resources.Load<Texture2D>(this.PackageTableItem.ImagePath);
        Sprite Temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = Temp;
        //�Ǽ�����
        RefreshStars();


    }
    public void RefreshStars()
    {
        for(int i = 0; i< this.UIIcon.childCount; i++) 
        {
            Transform star=UIIcon.GetChild(i).transform;
            if(i<this.PackageTableItem.Stars)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }

}
