using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    /// <summary>
    /// 渐变动画后显示星星
    /// </summary>
    public void Show()
    {
        GameManager.Instance.ShowStar();
    }
}
