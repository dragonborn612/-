using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float  mixSpeed=5;
    public float maxSpeed=10;
    public Sprite hurtPig;
    private SpriteRenderer pigSpite;
    public GameObject boomPrefaber;
    public GameObject pigDieScore;
    public bool isPig = false;
    public AudioClip pigDie;
    public AudioClip pighurt;
    public AudioClip woodHurt;
    public AudioClip woodDie;
    
    private void Awake()
    {
        pigSpite = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.relativeVelocity.magnitude > maxSpeed)
        {
            die();
        }
        else if(collision.relativeVelocity.magnitude >= mixSpeed&& collision.relativeVelocity.magnitude <= maxSpeed)
        {
            pigSpite.sprite = hurtPig;
            
           
            if (isPig)
                AudioSource.PlayClipAtPoint(pighurt, transform.position);
            else
                AudioSource.PlayClipAtPoint(woodHurt, transform.position);
        }
        
    }
    private void die()
    {
        if (isPig)
        {
            GameManager.Instance.pigs.Remove(this);
            Instantiate(boomPrefaber, transform.position, Quaternion.identity);//生成爆炸动画
            AudioSource.PlayClipAtPoint(pigDie, transform.position);
            GameObject score = Instantiate(pigDieScore, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);//生成加分动画
            Destroy(score, 1.5f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(woodDie, transform.position);
        }
        Destroy(gameObject);
    }
}
