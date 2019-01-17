using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

	public World world;
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		LoadChunks (player.transform.position, 300, 450);
	}

	public void LoadChunks(Vector3 playerPos, float distToLoad, float distToUnload){
		for(int x=0;x<world.chunks.GetLength(0);x++){
			for(int z=0;z<world.chunks.GetLength(2);z++){
				float dist=Vector2.Distance(new Vector2(x*world.chunkSize, z*world.chunkSize),new Vector2(playerPos.x,playerPos.z));
				if(dist<distToLoad){
					if(world.chunks[x,0,z]==null){
						world.GenColumn(x,z);
					}
				} else if(dist>distToUnload){
					if(world.chunks[x,0,z]!=null){
						world.UnloadColumn(x,z);
					}
				}

			}
		}

	}
}
