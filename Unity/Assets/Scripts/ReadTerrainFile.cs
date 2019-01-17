using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class ReadTerrainFile : MonoBehaviour {

	public string fileName = "terrain.txt";
	public int[][] terrain;
	public int x = 256;
	public int y = 256; 

	public bool Load(string fileName)
	{
		terrain = new int[x][];
		for (int p = 0; p < x; ++p) {
			terrain [p] = new int[y];
		}

		string line;
		StreamReader theReader = new StreamReader(fileName, Encoding.Default);
		int i = 0;
		using (theReader) {
			do {
				line = theReader.ReadLine();
				if (line != null) {
					string[] entries = line.Split(' ');
					if (entries.Length > 0 && i<250){
						for(int j = 0; j<250; j++){
							terrain[i][j] =  (int) (float.Parse(entries[j])*150);
						}
						//DoStuff(entries);
					}
				}
				i++;
			} while (line != null);

			theReader.Close();
			return true;
		}
	}

}
