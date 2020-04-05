using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{
	public class Menu : MonoBehaviour

{


	
	// Update is called once per frame
	void Play () 
	{ 
			Application.LoadLevel("Demo_Scen");
	}
	
//	void Options () 
//	{
//		Application.LoadLevel("Demo_Options");
//	}

	void Exit () 
	{	
		Application.Quit ();
	}
}
}