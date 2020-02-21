using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{

    public class Door_Control : MonoBehaviour
    {
        public string RedTeamTag, BlueTeamTag;  // object tags response
        private bool Open, Close;
        private Quaternion StartRotation;
        public float rotationResetSpeed = 1.0f;

        void Awake()
        {
            StartRotation = transform.rotation;
        }

        void Update()
        {
            if (StartRotation != transform.rotation && !Open && !Close)
            {
                StartCoroutine("waitToClose");
            }

            if (Close)
            {                
                transform.rotation = Quaternion.Slerp(transform.rotation, StartRotation, Time.time * rotationResetSpeed);

                if (StartRotation == transform.rotation)
                {
                    Open = false;
                    Close = false;
                }

            }           
        }

        // === Open collision === //
        void OnCollisionStay2D(Collision2D coll)
        {
            if (coll.gameObject.tag == RedTeamTag || coll.gameObject.tag == BlueTeamTag)
            {
             //   Debug.Log("Work");            
                Close = false;
                StopCoroutine("waitToClose");
                StartCoroutine("waitToClose");
            }            
        }

            // === WAIT TO CLOSE === //
            IEnumerator waitToClose()
        {
            Open = true;
            yield return new WaitForSeconds(4f);
            Close = true;           
        }

    }
}