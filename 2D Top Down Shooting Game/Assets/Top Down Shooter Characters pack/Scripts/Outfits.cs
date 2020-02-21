using System.Collections;
using UnityEngine;

namespace GearsAndBrains
{
	// === CUSTOM EDITOR SCRIPT, IN FOLDER EDITOR "Outfits_inspector"  === //

	// === SET PARAMETERS  FOR GUI EDITOR === //
	public enum OutfitsType
	{
		Head = 0,
		Torso = 1,
		Legs = 2,
		Boots = 3,
		Weapons = 4
	}


[ExecuteInEditMode]
public class Outfits : MonoBehaviour 
	{
		// === SET VARIABLES AND OBJECTS OUTFITS === //
		public OutfitsType outfitsType = OutfitsType.Torso;

		public bool GameObjectflodOut;

		// === TORSO === //

		public bool HelmON;

		public GameObject Helm; 

		public bool Hat2ON;
		public GameObject Hat2;
	

		public bool Hat1ON;
		public GameObject Hat1;


		public bool Police_HatON;
		public GameObject Police_Hat;

		public bool BalaclavaON;
		public GameObject Balaclava;

		public bool HeadON;
		public GameObject Head; 

		public bool Rifle2ON;
		public GameObject Rifle2; 

		public bool Rifle1ON;
		public GameObject Rifle1;

		public bool PistolON;
		public GameObject Pistol;

        public bool ShieldON;
        public GameObject Shield;

        public bool Tactical_VestON;
		public GameObject Tactical_Vest;

		public bool Police_textON;
		public GameObject Police_text;

		public bool Bulletproof_VestON;
		public GameObject Bulletproof_Vest; 

		public bool Blue_ShirtON;
		public GameObject Blue_Shirt;

		public bool Camo_ShirtON;
		public GameObject Camo_Shirt;

		public bool ShirtON;
		public GameObject Shirt; 

		public bool SuitON;
		public GameObject Suit; 

		public bool Police_ShirtON;
		public GameObject Police_Shirt; 

		public bool Gloves2ON;
		public GameObject Gloves2; 
		public GameObject Gloves2_Dop; 

		public bool Gloves1ON;
		public GameObject Gloves1; 
		public GameObject Gloves1_Dop;

		public bool TorsoON;
		public GameObject Torso; 

		// === LEGS === //

		public bool HolsterON;
		public GameObject Holster;
		public GameObject Holster_empty;

		public bool Blue_pantsON;
		public GameObject Blue_pants; 

		public bool Camo_pantsON;
		public GameObject Camo_pants;

		public bool Sand_pantsON;
		public GameObject Sand_pants;

		public bool TrousersON;
		public GameObject Trousers;

		public bool ShortsON;
		public GameObject Shorts;

		public bool Police_PantsON;
		public GameObject Police_Pants;	

		public bool Sand_bootsON;
		public GameObject Sand_boots;

		public bool Blue_bootsON;
		public GameObject Blue_boots;

		public bool Black_bootsON;
		public GameObject Black_boots;

		public bool LegsON;
		public GameObject Legs;

		public bool Sneakers2ON;
		public GameObject Sneakers2;

		public bool Sneakers1ON;
		public GameObject Sneakers1;

		public bool Police_BootsON;
		public GameObject Police_Boots;

		public bool FootsON;
		public GameObject Foots;

        //private Soldier_Control solControl;
        //private Enemy_Control enemyControl;

        void Awake ()
        {
           // solControl = GetComponent<Soldier_Control>();
           // enemyControl = GetComponent<Enemy_Control>();
        }

		// === CHANGE VARIABLES OUTFITS, UPDATE IN EDITOR MODE === //
	void Update ()  
		{
		// === TORSO === //

			Helm.SetActive(HelmON);

			Hat2.SetActive(Hat2ON);

			Hat1.SetActive(Hat1ON);

			Police_Hat.SetActive(Police_HatON);

			Balaclava.SetActive(BalaclavaON);

			Head.SetActive(HeadON);

			Rifle2.SetActive(Rifle2ON);
		
			Rifle1.SetActive(Rifle1ON);

			Pistol.SetActive(PistolON);

            Shield.SetActive(ShieldON);

            Tactical_Vest.SetActive(Tactical_VestON);

			Police_text.SetActive(Police_textON);

			Bulletproof_Vest.SetActive(Bulletproof_VestON);
		
			Blue_Shirt.SetActive(Blue_ShirtON);
		
			Camo_Shirt.SetActive(Camo_ShirtON);
		
			Shirt.SetActive(ShirtON);
	
			Suit.SetActive(SuitON);
		
			Police_Shirt.SetActive(Police_ShirtON);

			Gloves2.SetActive (Gloves2ON);
			Gloves2_Dop.SetActive (Gloves2ON);		
		
			Gloves1.SetActive (Gloves1ON);
			Gloves1_Dop.SetActive (Gloves1ON);

			Torso.SetActive (TorsoON);

		// === LEGS === //
		
			Holster.SetActive (HolsterON);
			Holster_empty.SetActive (HolsterON);

			Blue_pants.SetActive(Blue_pantsON);

			Camo_pants.SetActive(Camo_pantsON);
		
			Sand_pants.SetActive(Sand_pantsON);
		
			Trousers.SetActive(TrousersON);
		
			Shorts.SetActive(ShortsON);

			Police_Pants.SetActive(Police_PantsON);

			Sand_boots.SetActive(Sand_bootsON);

			Blue_boots.SetActive(Blue_bootsON);
		
			Black_boots.SetActive(Black_bootsON);

			Legs.SetActive(LegsON);

			Sneakers2.SetActive(Sneakers2ON);

			Sneakers1.SetActive(Sneakers1ON);

			Police_Boots.SetActive(Police_BootsON);

			Foots.SetActive(FootsON);

           
            //if (solControl != null)
            //{
            //     solControl.Shield = ShieldON;
            //}

            //if (enemyControl != null)
            //{
            //    enemyControl.Shield = ShieldON;
            //}

        }
	
//		// === CHANGE SORTING LAYER ON DEATH, CALL FROM FUNCTION *_CONTROL=== //
// public	void sortingOrderDeath ()
//		{	
//		// === TORSO === //
//
//			Helm.GetComponent<SpriteRenderer> ().sortingOrder = -8; 
//			Hat2.GetComponent<SpriteRenderer> ().sortingOrder = -8; 
//			Hat1.GetComponent<SpriteRenderer> ().sortingOrder = -8; 
//			Police_Hat.GetComponent<SpriteRenderer> ().sortingOrder = -8; 
//			Balaclava.GetComponent<SpriteRenderer> ().sortingOrder = -9; 
//			Head.GetComponent<SpriteRenderer> ().sortingOrder = -10; 
//			Rifle2.GetComponent<SpriteRenderer> ().sortingOrder = -11;
//			Rifle1.GetComponent<SpriteRenderer> ().sortingOrder = -11; 
//			Pistol.GetComponent<SpriteRenderer> ().sortingOrder = -11; 
//			Tactical_Vest.GetComponent<SpriteRenderer> ().sortingOrder = -12; 
//			Police_text.GetComponent<SpriteRenderer> ().sortingOrder = -11; 
//			Bulletproof_Vest.GetComponent<SpriteRenderer> ().sortingOrder = -13; 
//			Blue_Shirt.GetComponent<SpriteRenderer> ().sortingOrder = -14; 
//			Camo_Shirt.GetComponent<SpriteRenderer> ().sortingOrder = -14; 
//			Shirt.GetComponent<SpriteRenderer> ().sortingOrder = -14; 
//			Suit.GetComponent<SpriteRenderer> ().sortingOrder = -14; 
//			Police_Shirt.GetComponent<SpriteRenderer> ().sortingOrder = -14; 
//			Gloves2.GetComponent<SpriteRenderer> ().sortingOrder = -15; 
//			Gloves2_Dop.GetComponent<SpriteRenderer> ().sortingOrder = -15; 
//			Gloves1.GetComponent<SpriteRenderer> ().sortingOrder = -15; 
//			Gloves1_Dop.GetComponent<SpriteRenderer> ().sortingOrder = -15; 
//			Torso.GetComponent<SpriteRenderer> ().sortingOrder = -16; 
//
//		// === LEGS === //
//
//			Holster.GetComponent<SpriteRenderer> ().sortingOrder = -17; 
//			Holster_empty.GetComponent<SpriteRenderer> ().sortingOrder = -17; 
//			Blue_pants.GetComponent<SpriteRenderer> ().sortingOrder = -18; 
//			Camo_pants.GetComponent<SpriteRenderer> ().sortingOrder = -18; 
//			Sand_pants.GetComponent<SpriteRenderer> ().sortingOrder = -18;
//			Trousers.GetComponent<SpriteRenderer> ().sortingOrder = -18;
//			Shorts.GetComponent<SpriteRenderer> ().sortingOrder = -18;
//			Police_Pants.GetComponent<SpriteRenderer> ().sortingOrder = -18;			
//			Sand_boots.GetComponent<SpriteRenderer> ().sortingOrder = -19;
//			Blue_boots.GetComponent<SpriteRenderer> ().sortingOrder = -19;
//			Black_boots.GetComponent<SpriteRenderer> ().sortingOrder = -19;
//			Legs.GetComponent<SpriteRenderer> ().sortingOrder = -20;
//			Sneakers2.GetComponent<SpriteRenderer> ().sortingOrder = -21;
//			Sneakers1.GetComponent<SpriteRenderer> ().sortingOrder = -21;
//			Police_Boots.GetComponent<SpriteRenderer> ().sortingOrder = -21;
//			Foots.GetComponent<SpriteRenderer> ().sortingOrder = -22;
//	}
}
}