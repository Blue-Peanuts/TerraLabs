
using UnityEngine;

public class Overseer : MonoBehaviour
{
    public static Overseer Instance;
    [SerializeField] private GameObject gibletPrefab;
    private void Start()
    {
        Instance = this;
        
        SpawnRandomGiblet();
    }

    public void SpawnRandomGiblet()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        Vector3 spawnPosition = blocks[Random.Range(0, blocks.Length)].transform.position;
        
        GameObject giblet = Instantiate(gibletPrefab, spawnPosition, Quaternion.identity);
        giblet.GetComponent<GibletBlueprint>().genetics.Randomize();
        GetComponent<Energy>().Give(100, giblet.GetComponent<Energy>());
    }
}
