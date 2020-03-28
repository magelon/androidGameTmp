using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void shake(GameObject obj){

//		Sequence mySequence = new Sequence();
		Vector3 oriPos = obj.transform.position;
//		mySequence.Append (HOTween.To (obj.transform, .03f,new TweenParms().Prop("position",new Vector3(oriPos.x,oriPos.y+.05f,oriPos.z),false)));
//		mySequence.Append (HOTween.To (obj.transform, .03f,new TweenParms().Prop("position",new Vector3(oriPos.x,oriPos.y-.05f,oriPos.z),false)));
//		mySequence.loopType = LoopType.Yoyo;
//		Tweener shaker =  HOTween.Shake (obj.transform, 3f,new TweenParms().Prop("position",new Vector3(0,.2f,0),true));
				Hashtable args = new Hashtable();  
	
//				args.Add("amount", new Vector3(5, 5, 5));  
				args.Add("x", 0);  
				args.Add("y",10);  
				args.Add("islocal", true); 
				args.Add("time", .4f);  
				ATween.ShakePosition (obj,args);
//		                              tTween.loopType = LoopType.YoyoInverse;
//		mySequence.Play ();
//		return shaker;

	}


		// Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
		public static Vector3 GetWorldPos( Vector2 screenPos,GameObject dragobject, bool locky = false,bool lockx = false)
		{
				Vector3 dragobjPos = Camera.main.WorldToScreenPoint (dragobject.transform.position);
				float tx = lockx ? dragobjPos.x: screenPos.x;
				float ty = locky ? dragobjPos.y : screenPos.y;
				Vector2 tpos = new Vector2 (tx, ty);
				Ray ray = Camera.main.ScreenPointToRay( tpos );

				// we solve for intersection with z = 0 plane
				float t = -ray.origin.z / ray.direction.z;

				return ray.GetPoint( t );
		}


}
