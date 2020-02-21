using UnityEngine;
using UnityEditor;
using System.Collections;

namespace GearsAndBrains
{

[CustomEditor(typeof(Outfits))]
public class Outfits_inspector : Editor {
		// === HEAD images === //
		Texture2D imgHelm = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Helm.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgHat2 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Сap.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgHat1 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Knitted_cap.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPoliceHat = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Police_hat.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBalaclava = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Balaclava.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgHead = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Head.png", typeof(Texture2D)) as Texture2D;

		// === TORSO images === //
		Texture2D imgTacticalVest = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Tactical_Vest.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPoliceText = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Police_text.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBulletproofVest = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Bulletproof_Vest.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBlueShirt = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Blue_Shirt.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgCamoShirt = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Camo_Shirt.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgShirt = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Shirt.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgSuit = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Suit.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPoliceShirt = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Police_Shirt.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgGloves2 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Gloves2.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgGloves1 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Gloves1.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgTorso = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Torso.png", typeof(Texture2D)) as Texture2D;

		// === LEGS images === //
		Texture2D imgHolster = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Holster.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBluePants = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Blue_pants.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgCamoPants = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Camo_pants.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgSandPants = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Sand_pants.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgTrousers = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Trousers.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgShorts = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Shorts.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPolicePants = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Police_Pants.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgLegs = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Legs.png", typeof(Texture2D)) as Texture2D;

		// === BOOTS images === //
		Texture2D imgSandBoots = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Sand_boots.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBlueBoots = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Blue_boots.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgBlackBoots = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Black_boots.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgSneakers2 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Sneakers1.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgSneakers1 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Sneakers2.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPoliceBoots = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Police_Boots.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgFoots = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Foots.png", typeof(Texture2D)) as Texture2D;

		// === BOOTS images === //
		Texture2D imgRifle2 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Rifle2.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgRifle1 = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Rifle1.png", typeof(Texture2D)) as Texture2D;
		Texture2D imgPistol = AssetDatabase.LoadAssetAtPath ("Assets/Top Down Shooter Characters pack/Sprites/Preview/Pistol.png", typeof(Texture2D)) as Texture2D;
        Texture2D imgShield = AssetDatabase.LoadAssetAtPath("Assets/Top Down Shooter Characters pack/Sprites/Preview/Shield.png", typeof(Texture2D)) as Texture2D;


        // ================= //

        public override void OnInspectorGUI (){
			Outfits script = (Outfits)target;
			bool allowSceneObjects = !EditorUtility.IsPersistent (script);

			script.outfitsType =(OutfitsType)EditorGUILayout.EnumPopup ("Type", script.outfitsType);  // Type Control

			// === HEAD === //

			switch (script.outfitsType) 
			{
			case OutfitsType.Head:
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgHelm, ScaleMode.ScaleToFit);  // Image
				script.HelmON = EditorGUILayout.Toggle ("Taktical healm", script.HelmON);			

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgHat2, ScaleMode.ScaleToFit);  // Image
				script.Hat2ON = EditorGUILayout.Toggle ("Сap", script.Hat2ON);
				
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgHat1, ScaleMode.ScaleToFit);  // Image
				script.Hat1ON = EditorGUILayout.Toggle ("Knitted cap", script.Hat1ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgPoliceHat, ScaleMode.ScaleToFit);  // Image
				script.Police_HatON = EditorGUILayout.Toggle ("Police hat", script.Police_HatON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBalaclava, ScaleMode.ScaleToFit);  // Image
				script.BalaclavaON = EditorGUILayout.Toggle ("Balaclava", script.BalaclavaON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgHead, ScaleMode.ScaleToFit);  // Image
				script.HeadON = EditorGUILayout.Toggle ("Head", script.HeadON);

				// === GAME OBJECT GROUP === //

				script.GameObjectflodOut = EditorGUILayout.Foldout (script.GameObjectflodOut, "Head game object group");  //Коллапс текста
				if (script.GameObjectflodOut)
				{
					script.Helm = EditorGUILayout.ObjectField ("Healm object", script.Helm, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Hat2 = EditorGUILayout.ObjectField ("Сap", script.Hat2, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Hat1 = EditorGUILayout.ObjectField ("Knitted cap", script.Hat1, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Police_Hat = EditorGUILayout.ObjectField ("Police hat", script.Police_Hat, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Balaclava = EditorGUILayout.ObjectField ("Balaclava", script.Balaclava, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Head = EditorGUILayout.ObjectField ("Head", script.Head, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
				}
				break;

			// === TORSO === //

			case OutfitsType.Torso:
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgTacticalVest, ScaleMode.ScaleToFit);   // Image
				script.Tactical_VestON = EditorGUILayout.Toggle ("Tactical vest", script.Tactical_VestON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgPoliceText, ScaleMode.ScaleToFit);   // Image
				script.Police_textON = EditorGUILayout.Toggle ("Police text", script.Police_textON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBulletproofVest, ScaleMode.ScaleToFit);   // Image
				script.Bulletproof_VestON = EditorGUILayout.Toggle ("Bulletproof vest", script.Bulletproof_VestON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBlueShirt, ScaleMode.ScaleToFit);   // Image
				script.Blue_ShirtON = EditorGUILayout.Toggle ("Blue shirt", script.Blue_ShirtON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgCamoShirt, ScaleMode.ScaleToFit);   // Image
				script.Camo_ShirtON = EditorGUILayout.Toggle ("Camo shirt", script.Camo_ShirtON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgShirt, ScaleMode.ScaleToFit);   // Image
				script.ShirtON = EditorGUILayout.Toggle ("Shirt", script.ShirtON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgSuit, ScaleMode.ScaleToFit);   // Image
				script.SuitON = EditorGUILayout.Toggle ("Suit", script.SuitON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgPoliceShirt, ScaleMode.ScaleToFit);   // Image
				script.Police_ShirtON = EditorGUILayout.Toggle ("Police shirt", script.Police_ShirtON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgGloves2, ScaleMode.ScaleToFit);   // Image
				script.Gloves2ON = EditorGUILayout.Toggle ("Gloves2", script.Gloves2ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgGloves1, ScaleMode.ScaleToFit);   // Image
				script.Gloves1ON = EditorGUILayout.Toggle ("Gloves1", script.Gloves1ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgTorso, ScaleMode.ScaleToFit);   // Image
				script.TorsoON = EditorGUILayout.Toggle ("Torso", script.TorsoON);


				// === GAME OBJECT GROUP === //
				script.GameObjectflodOut = EditorGUILayout.Foldout (script.GameObjectflodOut, "Torso game object group");  //Коллапс текста
				if (script.GameObjectflodOut)
				{

					script.Tactical_Vest = EditorGUILayout.ObjectField ("Tactical vest", script.Tactical_Vest, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Police_text = EditorGUILayout.ObjectField ("Police text", script.Police_text, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Bulletproof_Vest = EditorGUILayout.ObjectField ("Bulletproof vest", script.Bulletproof_Vest, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Blue_Shirt = EditorGUILayout.ObjectField ("Blue shirt", script.Blue_Shirt, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Camo_Shirt = EditorGUILayout.ObjectField ("Camo shirt", script.Camo_Shirt, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Shirt = EditorGUILayout.ObjectField ("Shirt", script.Shirt, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Suit = EditorGUILayout.ObjectField ("Suit", script.Suit, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Police_Shirt = EditorGUILayout.ObjectField ("Police shirt", script.Police_Shirt, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Gloves2 = EditorGUILayout.ObjectField ("Gloves2", script.Gloves2, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Gloves2_Dop = EditorGUILayout.ObjectField ("Gloves2 dop", script.Gloves2_Dop, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Gloves1 = EditorGUILayout.ObjectField ("Gloves1", script.Gloves1, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Gloves1_Dop = EditorGUILayout.ObjectField ("Gloves1 dop", script.Gloves1_Dop, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Torso = EditorGUILayout.ObjectField ("Torso", script.Torso, typeof(GameObject), allowSceneObjects) as GameObject;    //Object

				}
				break;

				// === LEGS === //

			case OutfitsType.Legs:
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgHolster, ScaleMode.ScaleToFit);   // Image
				script.HolsterON = EditorGUILayout.Toggle ("Holster", script.HolsterON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBluePants, ScaleMode.ScaleToFit);   // Image
				script.Blue_pantsON = EditorGUILayout.Toggle ("Blue pants", script.Blue_pantsON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgCamoPants, ScaleMode.ScaleToFit);   // Image
				script.Camo_pantsON = EditorGUILayout.Toggle ("Camo pants", script.Camo_pantsON );

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgSandPants, ScaleMode.ScaleToFit);   // Image
				script.Sand_pantsON = EditorGUILayout.Toggle ("Sand pants", script.Sand_pantsON );

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgTrousers, ScaleMode.ScaleToFit);   // Image
				script.TrousersON = EditorGUILayout.Toggle ("Trousers", script.TrousersON );

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgShorts, ScaleMode.ScaleToFit);   // Image
				script.ShortsON = EditorGUILayout.Toggle ("Shorts", script.ShortsON );

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgPolicePants, ScaleMode.ScaleToFit);   // Image
				script.Police_PantsON = EditorGUILayout.Toggle ("Police pants", script.Police_PantsON);
				
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgLegs, ScaleMode.ScaleToFit);   // Image
				script.LegsON = EditorGUILayout.Toggle ("Legs", script.LegsON);


				// === GAME OBJECT GROUP === //
				script.GameObjectflodOut = EditorGUILayout.Foldout (script.GameObjectflodOut, "Legs game object group");  //Коллапс текста
				if (script.GameObjectflodOut)
				{
					script.Holster = EditorGUILayout.ObjectField ("Holster", script.Holster, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Holster_empty = EditorGUILayout.ObjectField ("Holster empty", script.Holster_empty, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Blue_pants = EditorGUILayout.ObjectField ("Blue pants", script.Blue_pants, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Camo_pants = EditorGUILayout.ObjectField ("Camo pants", script.Camo_pants, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Sand_pants = EditorGUILayout.ObjectField ("Sand pants", script.Sand_pants, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Trousers = EditorGUILayout.ObjectField ("Trousers", script.Trousers, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Shorts = EditorGUILayout.ObjectField ("Shorts", script.Shorts, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Police_Pants = EditorGUILayout.ObjectField ("Police pants", script.Police_Pants, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script. Legs = EditorGUILayout.ObjectField (" Legs", script. Legs, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
				}
				break;

				// === BOOTS === //
				
			case OutfitsType.Boots:
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgSandBoots, ScaleMode.ScaleToFit);   // Image
				script.Sand_bootsON = EditorGUILayout.Toggle ("Sand boots", script.Sand_bootsON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBlueBoots, ScaleMode.ScaleToFit);   // Image
				script.Blue_bootsON = EditorGUILayout.Toggle ("Blue boots", script.Blue_bootsON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgBlackBoots, ScaleMode.ScaleToFit);   // Image
				script.Black_bootsON = EditorGUILayout.Toggle ("Black boots", script.Black_bootsON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgSneakers2, ScaleMode.ScaleToFit);   // Image
				script.Sneakers2ON = EditorGUILayout.Toggle ("Sneakers2", script.Sneakers2ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgSneakers1, ScaleMode.ScaleToFit);   // Image
				script.Sneakers1ON = EditorGUILayout.Toggle ("Sneakers1", script.Sneakers1ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgPoliceBoots, ScaleMode.ScaleToFit);   // Image
				script.Police_BootsON = EditorGUILayout.Toggle ("Police boots", script.Police_BootsON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgFoots, ScaleMode.ScaleToFit);   // Image
				script.FootsON = EditorGUILayout.Toggle ("Foots", script.FootsON);


				// === GAME OBJECT GROUP === //
				script.GameObjectflodOut = EditorGUILayout.Foldout (script.GameObjectflodOut, "Legs game object group");  //Коллапс текста
				if (script.GameObjectflodOut)
				{
					script.Sand_boots = EditorGUILayout.ObjectField ("Sand boots", script.Sand_boots, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Blue_boots = EditorGUILayout.ObjectField ("Blue boots", script.Blue_boots, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Black_boots = EditorGUILayout.ObjectField ("Black boots", script.Black_boots, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Sneakers2 = EditorGUILayout.ObjectField ("Sneakers2", script.Sneakers2, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Sneakers1 = EditorGUILayout.ObjectField ("Sneakers1", script.Sneakers1, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Police_Boots = EditorGUILayout.ObjectField ("Police boots", script.Police_Boots, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Foots = EditorGUILayout.ObjectField ("Foots", script.Foots, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
				}
				break;


				// === WEAPONS === //

			case OutfitsType.Weapons:
				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgRifle2, ScaleMode.ScaleToFit);   // Image
				script.Rifle2ON = EditorGUILayout.Toggle ("Rifle2", script.Rifle2ON);

				GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgRifle1, ScaleMode.ScaleToFit);   // Image
				script.Rifle1ON = EditorGUILayout.Toggle ("Rifle1", script.Rifle1ON); 

                GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50), imgPistol, ScaleMode.ScaleToFit);   // Image
                script.PistolON = EditorGUILayout.Toggle("Pistol", script.PistolON);

                GUI.DrawTexture (GUILayoutUtility.GetRect (50, 50), imgShield, ScaleMode.ScaleToFit);   // Image
				script.ShieldON = EditorGUILayout.Toggle ("Tactical shield", script.ShieldON);


                    // === GAME OBJECT GROUP === //
                script.GameObjectflodOut = EditorGUILayout.Foldout(script.GameObjectflodOut, "Weapons game object group");  //Коллапс текста
                if (script.GameObjectflodOut)
				{
					script.Rifle2 = EditorGUILayout.ObjectField ("Rifle2", script.Rifle2, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Rifle1 = EditorGUILayout.ObjectField ("Rifle1", script.Rifle1, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Pistol = EditorGUILayout.ObjectField ("Pistol", script.Pistol, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
					script.Shield = EditorGUILayout.ObjectField ("Tactical shield", script.Shield, typeof(GameObject), allowSceneObjects) as GameObject;    //Object
				}
			break;
			}

			if (GUI.changed)
				EditorUtility.SetDirty (target);
		}
}
}