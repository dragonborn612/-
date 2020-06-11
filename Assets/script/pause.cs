using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseButton;
    // Start is called before the first frame update
    /// <summary>
    /// 暂停动画播放完后，时间停止
    /// </summary>
    public void Pause()
    {
        pauseButton.SetActive(true);
    }
}
