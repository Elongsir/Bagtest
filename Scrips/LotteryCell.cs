using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryCell : MonoBehaviour
{
    private  Transform Image;
    private  Transform Stars;

    private PackageTableItem PackageTableItem;
    private PackageLocalItem PackageLocalItem;
    private Transform UIRoot;

    private void Awake()
    {
        InitName();
    }
    private void InitName()
    {
        Image = transform.Find("Center/Image");
        Stars = transform.Find("Bottom/Stars");
    }
    public  void Refresh(PackageLocalItem packageLocalItem,LotteryPanel UiParent)
    {
        this.PackageLocalItem = packageLocalItem;
        PackageTableItem=GameManager.Instance.GetPackageItemByid(packageLocalItem.id);
        UIRoot = UiParent.transform;
        //Ë¢ÐÂÍ¼Æ¬
        RefreshImage();
        //Ë¢ÐÂÐÇ¼¶
        RefreshStars();
    }
    private void RefreshImage()
    {
        Texture2D t = Resources.Load<Texture2D>(this.PackageTableItem.ImagePath);
        Sprite sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        Image.GetComponent<Image>().sprite = sprite;
    }

    private void RefreshStars()
    {
        for(int i = 0;i<Stars.childCount; i++)
        {
            Transform Star=Stars.GetChild(i);
            {
                if(i<PackageTableItem.Stars)
                {
                    Star.gameObject.SetActive(true);
                }
                else
                {
                    Star.gameObject.SetActive(false);
                }
            }
        }
    }
}
