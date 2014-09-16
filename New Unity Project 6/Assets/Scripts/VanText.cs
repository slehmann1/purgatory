using UnityEngine;
using System.Collections.Generic;
public class VanText : MonoBehaviour
{
		[System.Serializable]
		public class TextSetup
		{
				public string text;
				public Color[] colors;
				public GameObject[] images;
				public Font[] fonts;
		}
		private const int headerMax = 15;
		private const int subHeaderMax = 25;
		public int probabilityBlank;
		// Use this for initialization
		public TextMesh heading, subHeading;
		public TextSetup[] possibles;
		void Start ()
		{
				if (Random.Range (0, 100) > probabilityBlank) {
//create text
						TextSetup use = possibles [Random.Range (0, possibles.Length)];
						string[] stringname = new string[2];
						stringname [0] = use.text;
						stringname = stringname [0].Split (new char[]{'|'});
						// a pipe seperates headings
						heading.text = wrap (stringname [0], headerMax);
						subHeading.text = wrap (stringname [1], subHeaderMax);
						heading.color = use.colors [Random.Range (0, use.colors.Length)];
						subHeading.color = heading.color;
						heading.font = use.fonts [Random.Range (0, use.fonts.Length)];
						subHeading.font = heading.font;
						heading.renderer.material = heading.font.material;
						subHeading.renderer.material = heading.font.material;
						try {
								if (use.images.Length > 0) {
										GameObject g = (GameObject)Instantiate (use.images [Random.Range (0, use.images.Length)]);
										g.transform.parent = transform;
										g.transform.localPosition = g.transform.position;
								}
						} catch {
						}
				}
		}
		private string wrap (string s, int characters)
		{
				if (s.Length > characters) {
						string [] chars = s.Split (new char[]{' '});
						List <string> list = new List<string> (chars);
						bool cont = true;
						int i = 0;
						while (cont) {
								if (i < list.Count - 1) {
										if (list [i].Length + list [i + 1].Length < characters) {
												list [i] += " " + (list [i + 1]);
												list.RemoveAt (i + 1);
										} else {
												i++;
										}
								} else {
										cont = false;
								}
						}
						for (int x=1; x<list.Count; x++) {
								list [0] += "\n" + list [x];
						}
						return list [0];
				}
				return s;
		}
}
 