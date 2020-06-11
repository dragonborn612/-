using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<pig> pigs;
    public GameObject lose;
    public GameObject win;
    public GameObject[] stars;
    public GameObject pausePanel;
    public Animator pauseAnimator;
    public GameObject pauseButton;
    public AudioSource bgMusic;
    //单例；
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
        set { }
     }
    private Vector3 orignPos ;
    // Start is called before the first frame update
    void Awake()
    {
        _instance=this;
        orignPos = birds[0].transform.position;
    }
    private void Start()
    {
        Initialized();
    }
    
    /// <summary>
    /// 小鸟的初始化；
    /// </summary>
    private void Initialized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = orignPos;
                birds[i].enabled = true;
                birds[i].sj .enabled= true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sj.enabled = false;
            }
        }
    }
    public  void  NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只
                Initialized();
            }
            else
            {
                //输了
                bgMusic.enabled = false;
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            bgMusic.enabled = false;
            win.SetActive(true);
        }
    }
    public void ShowStar()
    {
        StartCoroutine("show");
    }
    IEnumerator show()
    {
        for (int i = 0; i < birds.Count + 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true); 
        }
    }
    /// <summary>
    /// 点击游戏界面的暂停；
    /// </summary>
    public void pause()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        pauseAnimator.SetBool("isPause", true);

        //Time.timeScale = 0;
    }
    /// <summary>
    /// 点击暂停界面的继续；
    /// </summary>
    public void pauseToStar()
    {
        Time.timeScale = 1;
        pauseAnimator.SetBool("isPause", false);
        
    }
    public void retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);

    }
    public void home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
