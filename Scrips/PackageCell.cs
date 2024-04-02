using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scrips
{
    public  class PackageCell:MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
    {
        private Transform UiIcon;
        private Transform UiHead;
        private Transform UiNew;
        private Transform UiSelect;
        private Transform UiLevel;
        private Transform UiStars;
        private Transform UiDeleteSelect;

        private PackageLocalItem packageLocalItem;
        private PackageTableItem PackageTableItem;
        private PackagePanel ParentUI;

        private Transform UiSelectAni;
        private Animator SelectAni;
        
        private Transform UiMouseAni;
        private Animator MouseAni;

        private void Awake()
        {
            InitName();
        }
        private void InitName()
        {
            UiIcon = transform.Find("Top/icon");
            UiHead = transform.Find("Top/Head");
            UiNew = transform.Find("Top/New");
            UiSelect = transform.Find("Select");
            UiLevel = transform.Find("Botton/LevelText");
            UiStars = transform.Find("Botton/Stars");
            UiDeleteSelect = transform.Find("DeleteSelect");
            UiDeleteSelect.gameObject.SetActive(false);

            UiSelectAni = transform.Find("SelectAni");
            SelectAni=UiSelectAni.GetComponent<Animator>();
           // UiSelectAni.gameObject.SetActive(false) ;
            
            UiMouseAni = transform.Find("MouseOverAni");
            MouseAni=UiMouseAni.GetComponent<Animator>();
            UiMouseAni.gameObject.SetActive(false) ;
        }

        public void Refesh( PackageLocalItem packageLocalItem ,PackagePanel UIParent)
        {
            //数据初始化
            this.packageLocalItem = packageLocalItem;
            this.PackageTableItem = GameManager.Instance.GetPackageItemByid(packageLocalItem.id);
            this.ParentUI = UIParent;
            //等级信息
            this.UiLevel.GetComponent<Text>().text = packageLocalItem.Level.ToString();
            //是否为新获得
            this.UiNew.gameObject.SetActive(this.packageLocalItem.IsNew);
            //物品的图片
            Texture2D texture = Resources.Load<Texture2D>(this.PackageTableItem.ImagePath);
            Sprite Temp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            UiIcon.GetComponent<Image>().sprite = Temp;
            //刷新星级
            RefreshStars();
            
        }

        //刷新删除选中状态
        public void RefreshDeleteState()
        {
            if(ParentUI.DeleteChooseuid.Contains(this.packageLocalItem.Uid))
            { 
                UiDeleteSelect.gameObject.SetActive(true);
            }
            else
            {
                UiDeleteSelect.gameObject.SetActive(false);
            }
            this.ParentUI.RefreshUIDelete();
        }
        public void RefreshStars()
        {
            for(int i=0;i<UiStars.childCount;i++)
            {
                Transform star= UiStars.GetChild(i).transform;
                if(i<PackageTableItem.Stars)
                {
                    star.gameObject.SetActive(true);
                }
                else
                {
                    star.gameObject.SetActive(false);
                }

            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.packageLocalItem.IsNew == true)
            {
                RefreshNew();
            }
            if (this.ParentUI.curMode==PackageMode.delete)
            {
                this.ParentUI.AddChooseDeletedUid(this.packageLocalItem.Uid);
            }
            if(this.ParentUI.ChoiceItemUid==this.packageLocalItem.Uid)
            {
                return;
            }
            ParentUI.ChoiceItemUid=this.packageLocalItem.Uid;
            SelectAni.SetTrigger("Select");
            Debug.Log("按下");
            //取消为新状态

            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UiMouseAni.gameObject.SetActive(true);
            MouseAni.SetTrigger("In");

            

            Debug.Log("进入");
        }
        private void RefreshNew()
        {
            PackageLocalItem OldItem = GameManager.Instance.GetPackageItemByUid(this.packageLocalItem.Uid);
            PackageLocalItem newitem = new()
            {
                Uid = Guid.NewGuid().ToString(),
                id = OldItem.id,
                num = OldItem.num,
                Level = OldItem.Level,
                IsNew = false,
            };
            PackageLocalData.Instance.Items.Remove(OldItem);
            PackageLocalData.Instance.Items.Add(newitem);
            PackageLocalData.Instance.SavePackage();
           this.packageLocalItem = newitem;
            Refesh(newitem, ParentUI);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseAni.SetTrigger("Exit");
           // UiMouseAni.gameObject.SetActive(false);
            Debug.Log("离开");
        }
    }
}
