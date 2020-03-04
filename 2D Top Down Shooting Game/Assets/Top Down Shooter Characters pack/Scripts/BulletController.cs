using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string RedTeamTag, BlueTeamTag;
    public Transform parentTransform;
    public string parentTag;
    public float bulletSpeed = 100f;
    public int bulletDamage = 1;
    public Sprite hitSprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * bulletSpeed);
        Destroy(gameObject,2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Door") 
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }
        if (other.gameObject.tag == "Entagged") 
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }
        if (other.gameObject.tag == RedTeamTag) 
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            //other.gameObject.GetComponentInChildren<NPC_Controller>().Damage = bulletDamage;
            if ((other.gameObject != null) && other.gameObject.GetComponent<Path_Follow>().targetInSight == false && parentTag == BlueTeamTag) 
            {
                other.gameObject.GetComponent<Path_Follow>().targetInSight = true;
                other.gameObject.GetComponent<Path_Follow>().aimTarget = parentTransform;
            }
            Destroy(gameObject, 0.02f);
        }
        if (other.gameObject.tag == BlueTeamTag) 
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            //other.gameObject.GetComponentInChildren<Player>.Damage(bulletDamage);
            Destroy(gameObject, 0.02f);
        }
    }
}
