using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject chunk;
	public GameObject[,,] chunks;
	public int chunkSize=16;

	public ReadTerrainFile m_terrain;

	public byte[,,] data;
	public int worldX=16;
	public int worldY=16;
	public int worldZ=16;

	// Use this for initialization
	void Start () {

		m_terrain.Load("Assets/Terrain/" + m_terrain.fileName);
		data = new byte[worldX,worldY,worldZ];

		for (int x=0; x<worldX; x++){
			for (int z=0; z<worldZ; z++){
				int stone=PerlinNoise(x,0,z,20,3,1.2f);
				stone+= PerlinNoise(x,300,z,30,4,0)+10;
				int dirt=PerlinNoise(x,100,z,100,5,2f)+1;
				//float stone = (m_terrain.terrain[x][z]);

				for (int y=0; y<worldY; y++){
					//Debug.Log(stone);
					if(y<=stone){
						data[x,y,z]=2;
					} else if(y<=dirt+stone){
						data[x,y,z]=2;
					}

				}
			}
		}

		//test
		/*for (int x = 2; x < 20; ++x) {
			data[2,x,2] = 1;
		}*/


		chunks=new GameObject[Mathf.FloorToInt(worldX/chunkSize),Mathf.FloorToInt(worldY/chunkSize),Mathf.FloorToInt(worldZ/chunkSize)];

		/*for (int x=0; x<chunks.GetLength(0); x++){
			for (int y=0; y<chunks.GetLength(1); y++){
				for (int z=0; z<chunks.GetLength(2); z++){

					chunks[x,y,z]= Instantiate(chunk,new Vector3(x*chunkSize,y*chunkSize,z*chunkSize),new Quaternion(0,0,0,0)) as GameObject;
					Chunk newChunkScript= chunks[x,y,z].GetComponent("Chunk") as Chunk;
						newChunkScript.worldGO=gameObject;
						newChunkScript.chunkSize=chunkSize;
						newChunkScript.chunkX=x*chunkSize;
						newChunkScript.chunkY=y*chunkSize;
						newChunkScript.chunkZ=z*chunkSize;
				}
			}
		}*/
	}

	int PerlinNoise(int x,int y, int z, float scale, float height, float power){
		float rValue;
		rValue= Noise.Noise.GetNoise (((double)x) / scale, ((double)y)/ scale, ((double)z) / scale);
		rValue*=height;

		if(power!=0){
			rValue=Mathf.Pow( rValue, power);
		}
			
		return (int) rValue;
	}


	// Update is called once per frame
	void Update () {

	}

	public byte Block(int x, int y, int z){
		if( x>=worldX || x<0 || y>=worldY || y<0 || z>=worldZ || z<0){
			return (byte) 1;
		}

		return data[x,y,z];
	}


	public void GenColumn(int x, int z){
		for (int y=0; y<chunks.GetLength(1); y++) {
			//Create a temporary Gameobject for the new chunk instead of using chunks[x,y,z]
			chunks [x, y, z] = Instantiate (chunk, new Vector3 (x * chunkSize - 0.5f, y * chunkSize + 0.5f, z * chunkSize - 0.5f), new Quaternion (0, 0, 0, 0)) as GameObject;
			Chunk newChunkScript = chunks[x,y,z].GetComponent("Chunk") as Chunk;
			newChunkScript.worldGO = gameObject;
			newChunkScript.chunkSize = chunkSize;
			newChunkScript.chunkX = x * chunkSize;
			newChunkScript.chunkY = y * chunkSize;
			newChunkScript.chunkZ = z * chunkSize;
		}
	}

	public void UnloadColumn(int x, int z){
		for (int y=0; y<chunks.GetLength(1); y++) {
			Object.Destroy(chunks [x, y, z].gameObject);
		}
	}
}