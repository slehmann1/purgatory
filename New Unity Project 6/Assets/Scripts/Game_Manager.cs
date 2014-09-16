using UnityEngine;
using System.Collections;
public class Game_Manager : MonoBehaviour
{
	public Texture2D cursor;
		void Start ()
		{
			Cursor.SetCursor(cursor,new Vector2(0,0),CursorMode.Auto);
		}
}
