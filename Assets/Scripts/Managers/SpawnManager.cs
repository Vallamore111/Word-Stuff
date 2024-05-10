using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Transform spawns;
    private List<GameObject> spawnedObjects;
    private List<Vector3> spawnPoints;
    private TouchInput touchInput;
    private WordListMenu wordListMenu;

    [Header("Bubbles")]
    public GameObject bubblePrefab;
    private float bubbleDropTime;
    private float nextBubble;
    public List<string> dropList;


    private void Awake()
    {
        instance = this;
        touchInput = FindObjectOfType<TouchInput>();
        wordListMenu = FindObjectOfType<WordListMenu>();
        spawnedObjects = new List<GameObject>();
        spawnPoints = new List<Vector3>();
        dropList = new List<string>();

        enabled = false;
    }

    private void Start()
    {
        nextBubble = 0.625f;
    }


    private void Update()
    {
        CreateBubbles();
    }


    public void EnableSpawns()
    {
        enabled = true;
        bubbleDropTime = Time.time;
    }


    public void DisableSpawns()
    {
        DestroyAllSpawns();
        enabled = false;
    }


    public void SetDropList()
    {
        dropList.Clear();

        foreach (var letter in wordListMenu.activeLetters)
        { dropList.Add(letter.assignedLetter); }
    }


    private void CreateBubbles()
    {
        if (Time.time <= bubbleDropTime) return;

        GenerateSpawnPoints(touchInput.dropWidth, touchInput.dropHeight);

        int index = Random.Range(0, dropList.Count);
        var bubble = Instantiate(bubblePrefab, spawns);
        MoveSpawnPoint(bubble);
        bubble.GetComponent<Bubble>().bubbleText.text = dropList[index];
        dropList.RemoveAt(index);
        spawnedObjects.Add(bubble);
        bubbleDropTime += nextBubble;

        if (dropList.Count == 0)
        { SetDropList(); }
    }


    private void MoveSpawnPoint(GameObject spawn)
    {
        int i = Random.Range(0, spawnPoints.Count);
        Vector3 spawnSpot = spawnPoints[i];
        spawn.transform.position = spawnSpot;
        spawnPoints.RemoveAt(i);
    }
        

    private void GenerateSpawnPoints(Vector2 spawnWidth, Vector2 spawnHeight)
    {
        spawnPoints.Clear();

        float xMin = spawnWidth.x;
        float xMax = spawnWidth.y;
        float yMin = spawnHeight.x;
        float yMax = spawnHeight.y;
        List<float> xSpawnValues = new List<float>();

        do 
        {
            xSpawnValues.Add(xMin);
            xMin++;
        } 
        while (xMin < xMax);


        for (int i = 0; i < xSpawnValues.Count; i++)
        {
            Vector3 newPoint = new Vector3(xSpawnValues[i], Random.Range(yMin, yMax), 0);
            spawnPoints.Add(newPoint);
            xSpawnValues.RemoveAt(i);
        }
    }


    private void DestroyAllSpawns()
    {
        foreach (var spawn in spawnedObjects)
        { Destroy(spawn); }
    }
}
