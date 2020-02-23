using UnityEngine;
using System.Collections;

public class Anim_Scale : MonoBehaviour {
	public Vector2 time=new Vector2(1.0f,1.0f);
	public Vector2 scale=new Vector2(1.5f,1.5f);
	public iTween.EaseType easetype=iTween.EaseType.easeInOutSine;
	void Start () {
		iTween.ScaleTo(gameObject,iTween.Hash("scale",new Vector3(scale.x,scale.y,1.0f),"time",time.x,"looptype", iTween.LoopType.pingPong,"easetype",easetype));
	}
}
