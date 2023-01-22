
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Overseer : MonoBehaviour
{
    public static Overseer Instance;
    public GameObject gibletPrefab;
    public GameObject foodPrefab;
    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            SpawnRandomFood();
        }
    }

    private void SpawnRandomFood()
    {
        if (FindObjectsOfType<FoodDecay>().Length > 250)
            return;
        
        Block[] blocks = FindObjectsOfType<Block>();
        Vector3 spawnPos = blocks[Random.Range(0, blocks.Length)].transform.position;
        
        GameObject food = Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        GetComponent<Energy>().Give(10, food.GetComponent<Energy>());
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var VARIABLE in FindObjectsOfType<GibletBlueprint>())
            {
                Destroy(VARIABLE.gameObject);
            }
        }
    }
    /*
    public void SpawnRandomGiblet()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        Vector3 spawnPosition = blocks[Random.Range(0, blocks.Length)].transform.position;
        
        GameObject giblet = Instantiate(gibletPrefab, spawnPosition, Quaternion.identity);
        giblet.GetComponent<GibletBlueprint>().genetics.Randomize();
        GetComponent<Energy>().Give(100, giblet.GetComponent<Energy>());
    }*/
}
