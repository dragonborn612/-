using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick;
    private bool isMove = false;
    public float mixDistance=1.5f;
    [HideInInspector]
    public SpringJoint2D sj;
    private TestMyTrail myTrail;
    private Rigidbody2D rb;
    public float waitTime;
    public Transform rightPos;
    public Transform leftPos;
    public LineRenderer rightLR;
    public LineRenderer leftLR;
    public GameObject boomPrefaber;
    public float smooth = 3;
    public AudioClip select;
    public AudioClip fly;
    public AudioClip birdCollion;
    [SerializeField]
    private  int birgIndex = 1;
    // Start is called before the first frame update
    //void Start()
    //{

    //}
    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
    }
    private void OnMouseDown()
    {
        isClick = true;
        rb.isKinematic = true;
        AudioSource.PlayClipAtPoint(select, transform.position);
    }
    private void OnMouseUp()
    {
        isClick = false;
        rb.isKinematic = false;       //禁用物理影响运动；
        Invoke("Fly", waitTime);    //禁用弹簧关节；
        leftLR.enabled = false;
        rightLR.enabled = false;
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick&&!isMove)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);

            if (Vector3.Distance(transform.position, rightPos.position) > mixDistance)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= mixDistance;
                transform.position = pos + rightPos.position;
            }
            Line();
        }
        //相机跟随
        float posx = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(posx,0,15), Camera.main.transform.position.y, Camera.main.transform.position.z),
            smooth*Time.deltaTime);
    }
    private void Fly()
    {
        myTrail.startTrailk();
        Invoke("Next", 5f);
        sj.enabled = false;
        AudioSource.PlayClipAtPoint(fly, transform.position);
    }
    private void Line()
    {
        

        leftLR.enabled = true;
        rightLR.enabled = true;
        rightLR.SetPosition(0, rightPos.position);
        rightLR.SetPosition(1, transform.position);

        leftLR.SetPosition(0, leftPos.position);
        leftLR.SetPosition(1, transform.position);
        
    }
    void Next()
    {
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boomPrefaber, transform.position, Quaternion.identity);
        GameManager.Instance.NextBird();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (birgIndex == 2)
        {
            AudioSource.PlayClipAtPoint(birdCollion, Camera.main.transform.position);
        }
        myTrail.clenTraile();
        birgIndex++;
    }
}
