using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] cubePreFabs;
   [SerializeField] private Transform cubeParent;
   public float cubeSpawnTime = 0.5f;
   [Range(0,1)] public float cubeSpawnTimeFactor = 0.5f;
   public float cubeSpeed = 20f;
   [Range(0,1)] public float cubeSpeedFactor = 20f;

   public float _cubeSpawnTime;
   public float _cubeSpeed;

   public float timeAlive;
   public float timeUntilCubeSpawn;


   private void Start(){
    GameManager.Instance.onGameOver.AddListener(ClearCubes);
    GameManager.Instance.onPlay.AddListener(ResetFactors);
   }
   private void Update(){
        if (GameManager.Instance.isPlaying){
            timeAlive += Time.deltaTime;
            CalculateFactors();
            SpawnLoop();
        }
    }

   private void SpawnLoop(){
        timeUntilCubeSpawn += Time.deltaTime;
        if(timeUntilCubeSpawn >= _cubeSpawnTime){
            Spawn();
            timeUntilCubeSpawn = 0f;
        }
    }

    private void ClearCubes(){
        foreach(Transform child in cubeParent){
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors(){
        _cubeSpawnTime = cubeSpawnTime / Mathf.Pow(timeAlive, cubeSpawnTimeFactor);
        _cubeSpeed = cubeSpeed * Mathf.Pow(timeAlive, cubeSpeedFactor);
    }

    private void ResetFactors(){
        timeAlive = 1f;
        _cubeSpawnTime = cubeSpawnTime;
        _cubeSpeed = cubeSpeed;
    }
   private void Spawn(){
    GameObject cubeToSpawn = cubePreFabs[Random.Range(0,cubePreFabs.Length)];
    GameObject spawnedcube = Instantiate(cubeToSpawn, transform.position, Quaternion.identity);
    spawnedcube.transform.parent = cubeParent;
    Rigidbody2D cubeRB = spawnedcube.GetComponent<Rigidbody2D>();
    cubeRB.velocity = Vector2.left*_cubeSpeed;
   }
}
