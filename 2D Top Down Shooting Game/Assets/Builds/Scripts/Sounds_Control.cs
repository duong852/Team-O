using UnityEngine;
using System.Collections;
    public class Sounds_Control : MonoBehaviour
    {
        public AudioClip insideAudioClip, outsideAudioClip;
        private AudioSource myAudioSours;         
        [HideInInspector]
        public bool Street;
        private bool Play;

        // Use this for initialization
        void Start()
        {
            Street = true;
            Play = true;
            myAudioSours = GetComponent<AudioSource>();
            myAudioSours.clip = outsideAudioClip;
            myAudioSours.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {            
            if (Street && !Play && myAudioSours.enabled == false)
            {
                Play = true;
                myAudioSours.clip = outsideAudioClip;
                myAudioSours.enabled = true;                                             
            }

            if (!Street && !Play && myAudioSours.enabled == false)
            {
                Play = true;
                myAudioSours.clip = insideAudioClip;
                myAudioSours.enabled = true;                           
            }

            if (myAudioSours.clip == outsideAudioClip && !Street)
            {                
                myAudioSours.enabled = false;
                Play = false;                
            }

            if (myAudioSours.clip == insideAudioClip && Street)
            {                
                myAudioSours.enabled = false;
                Play = false;                
            }
        }     
}