using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnroom : MonoBehaviour {
    public GameObject Player;
    public GameObject center;
    [Range(4,20)]
    public int sides = 4;
    public float radius = 3;
    public float cliffMaxStickOut = 5;
    public float cliffMinStickOut = 1;
    public int initalWallCount = 10;
    public float timeBetweenSpawns = 0.5f;
    public float spawnDistance = 300;
    [Range(0, 1)]
    public float cliffSpawnRate = .1f;
    [Range(0, 1)]
    public float flipRate = .3f;
    private List<GameObject> sideList = new List<GameObject>();
    private int wallVerticleCount = 1;
    private float angle;
    public float wallHeight = 0;
    public GameObject [] rooms;
    public GameObject [] pickup;
    public float range;
    public float time;
    private float timer;
    public int max;
    public int currentpickups;
    //public GunUI play;
    //public PlayerFaceUI pick;
    // Use this for initialization
    public void spawn()
    {
        //spawn a pickup
        GameObject spawnpickup = Instantiate(pickup[Random.Range(0, pickup.Length)]);
        Vector3 pickupPosition = new Vector3(center.transform.position.x, center.transform.position.y - (wallHeight * wallVerticleCount), center.transform.position.z);
        Vector3 rando = Random.insideUnitSphere * radius * .85f;
        pickupPosition += new Vector3(rando.x, 0, rando.z);
        spawnpickup.transform.position = pickupPosition;
        //spawn walls
        for (int i = 0; i < sides; i++)
        {

            //float x = Random.Range(-range, range);
            //float z = Random.Range(-range, range);
            //spawn a wall
            int random = 1;
            if (Random.Range(0f, 1f) > cliffSpawnRate)
            {
                random = 0;
            }
            
            GameObject spawnroom = Instantiate(rooms[random], sideList[i].transform);
            
            //face center
            //spawnroom.transform.LookAt(center.transform.position);
            //change position
            spawnroom.transform.position = new Vector3(spawnroom.transform.position.x, spawnroom.transform.position.y - (wallHeight * wallVerticleCount), spawnroom.transform.position.z);
            if (random == 1)
            {
                spawnroom.transform.localPosition = new Vector3(0, spawnroom.transform.localPosition.y, Random.Range(0, cliffMaxStickOut));
            }
            else
            {
                if (Random.Range(0f, 1f) > flipRate)
                {
                    spawnroom.transform.localRotation = Quaternion.Euler(spawnroom.transform.rotation.x, spawnroom.transform.rotation.y, spawnroom.transform.rotation.z + 180);
                }
            }
            //don't tilt
            //spawnroom.transform.rotation = Quaternion.Euler(0, spawnroom.transform.rotation.eulerAngles.y + (angle * i), 0);

            //spawnpickup.GetComponent<pickup>().play = play;
            //spawnpickup.GetComponent<pickup>().pick = pick;
            //spawnpickup.transform.position = spawnroom.transform.position;
            //spawnpickup.GetComponent<pickup>().sp = this;
        }
        wallVerticleCount++;
    }
    IEnumerator SpawnCoroutine()
    {
        float level = center.transform.position.y - (wallHeight * wallVerticleCount);
        float distance = Player.transform.position.y - level;
        while (true)
        {
            level = center.transform.position.y - (wallHeight * wallVerticleCount);
            distance = Player.transform.position.y - level;
            if (distance < spawnDistance)
            {
                spawn();
            }
            else
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
        //float level = center.transform.position.y - (wallHeight * wallVerticleCount);
        //float distance = Player.transform.position.y - level;
        //if ()
    }
    void CalculateSideLocations()
    {
        angle = 360 / sides;
        for(int i = 0; i < sides; i++)
        {
            GameObject side = new GameObject("WallLocation" + i);
            //move it to the center's position
            side.transform.SetPositionAndRotation(center.transform.position, center.transform.rotation);
            //rotate to face direction it needs to move
            side.transform.rotation = Quaternion.Euler(side.transform.rotation.x, side.transform.rotation.y + (angle * i), side.transform.rotation.z);
            //move position
            side.transform.position = side.transform.position + (side.transform.forward.normalized * radius);
            //have it face the center 
            side.transform.LookAt(center.transform);
            //add to list
            sideList.Add(side);
        }
    }

    void Start () {
        CalculateSideLocations();
        for (int i = 0; i < initalWallCount; i++)
        {
            spawn();
        }
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update() {
        if (timer <= 0 && currentpickups < max)
        {
            spawn();
            timer = time;
            currentpickups += 1;
        }
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }
    private void Reset()
    {
        center = GameObject.FindGameObjectWithTag("Room");
    }


}
