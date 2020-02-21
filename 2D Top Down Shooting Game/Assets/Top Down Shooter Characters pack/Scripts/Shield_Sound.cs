using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{

    public class Shield_Sound : MonoBehaviour
    {
        public AudioClip shieldHitAudio;


        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Bullet")
            {
                AudioSource.PlayClipAtPoint(shieldHitAudio, transform.position);
            }

        }



    }
}
