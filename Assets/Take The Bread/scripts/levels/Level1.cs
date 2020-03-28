using UnityEngine;
using System.Collections;

public class Level1 :MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


		public GameObject bread;//the instance of the object used in game.


		void tapBread( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)//If the game locked,you can not control the game.
						return;
				if(gesture!=null && gesture.Selection == bread )//if taped the bread
				{
						Destroy (bread);//remove the bread
						GameData.getInstance ().main.gameWin ();	//tell the engine the game wins.
				}
		}
}
