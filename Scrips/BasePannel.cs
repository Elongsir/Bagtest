using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePannel : MonoBehaviour
{
    protected bool IsRemove = false;
    protected new string name;

    protected virtual void Awake()
    {

    }
    public virtual void OpenPennel(string name)
    {
        this.name = name;
        SetActive(true);
    }
    public virtual void ClosePanel()
    {
        IsRemove = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
        if(UIManager.Instance.panelDict.ContainsKey(name)) 
        {
            UIManager.Instance.panelDict.Remove(name);
        }
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
