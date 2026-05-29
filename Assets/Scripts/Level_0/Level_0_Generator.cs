using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using Random = UnityEngine.Random;

public class Level_0_Generator : MonoBehaviour
{
    // PREFABS
    [Header("Prefabs")]
    public GameObject lamp;
    public GameObject player;

    // MATERIALS
    [Header("Materials")]
    public Material wallMaterial;
    public Material groundMaterial;
    public Material roofMaterial;
    public Material curveMaterial;
    public Material disabledLightMaterial;

    // ROOM
    [Header("Room Properties")]
    [Range(1, 10)]
    public int mazeDimension = 3;
    [Range(5, 200)]
    public int roomSize = 50;
    [Range(0, 200)]
    public int roomDistance = 50;
    public float roomHeight = 10;
    public Hashtable rooms = new Hashtable();

    // DOORS
    public float minimalDoorWidth = 5;
    public float maximalDoorWidth = 10;
    public float minimalDoorDistance = 1;

    // INTERIOR
    public int maxNumberOfObjectsInRooms = 20;
    public int maxNumberOfHolesInGround = 4;

    // LIGHT
    public float lampHeight = 7f;
    public int numberOfLights = 5;

    [Header("Spawn chances and probabilities")]
    [Range(0.0f, 1.0f)]
    public float wallHasOneDoor = 0.7f;
    [Range(0.0f, 1.0f)]
    public float noWall = 0.5f;
    [Range(0.0f, 1.0f)]
    public float groundHasHole = 0.2f;
    [Range(0.0f, 1.0f)]
    public float lightIsOn = 0.5f;

    // CONTAINER
    GameObject allRooms;

    // PRIVATES
    public static MeshCreator meshCreator;
    private int currentRoomDimension = 1;
    private string lastRoom = "000";

    void Start()
    {
        Init();

        CreateStartRoom();

        CreateRoomMaze(mazeDimension);
    }

    void Init()
    {
        meshCreator = new MeshCreator();

        allRooms = new GameObject("AllRooms");
        allRooms.transform.SetParent(transform);
    }

    void CreateStartRoom()
    {
        CreateRandomRoom(roomSize, roomHeight, roomSize, Vector3.zero);
    }

    void CreateRoomMaze(int dimension)
    {
        for (int i = currentRoomDimension; i < dimension; i++)
        {
            float offset = -roomDistance * i;
            // FRONT SIDE
            for (int top = 0; top < i * 2 + 1; top++)
            {
                CreateRandomRoom(roomSize, roomHeight, roomSize, new Vector3(-roomSize * i + roomSize * top + offset, 0, roomSize * i + roomDistance * i));
                offset += roomDistance;
            }

            // RIGHT SIDE
            offset = roomDistance * (i-1);
            for (int right = 1; right <= i + (i-1); right++)
            {
                CreateRandomRoom(roomSize, roomHeight, roomSize, new Vector3(roomSize * i + roomDistance * i, 0, roomSize * i - roomSize * right + offset));
                offset -= roomDistance;
            }

            // BACK SIDE
            offset = roomDistance * i;
            for (int bottom = 0; bottom < i * 2 + 1; bottom++)
            {
                CreateRandomRoom(roomSize, roomHeight, roomSize, new Vector3(roomSize * i - roomSize * bottom + offset, 0, -roomSize * i - roomDistance * i));
                offset -= roomDistance;
            }

            // LEFT SIDE
            offset = -roomDistance * (i-1);
            for (int left = 1; left <= i + (i - 1); left++)
            {
                CreateRandomRoom(roomSize, roomHeight, roomSize, new Vector3(-roomSize * i - roomDistance * i, 0, -roomSize * i + roomSize * left + offset));
                offset += roomDistance;
            }
            currentRoomDimension++;
        }
    }

    void ExpandFront(IRoom currentRoom)
    {
        float offset = -roomDistance * (currentRoomDimension-1);

        for (int top = 0; top < currentRoomDimension * 2 - 1; top++)
        {
            IRoom room = CreateRandomRoom(roomSize, roomHeight, roomSize, currentRoom.getPosition() + new Vector3(-roomSize * (currentRoomDimension - 1) + roomSize * top + offset, 0, roomSize * currentRoomDimension + roomDistance * currentRoomDimension));
            offset += roomDistance;

            Destroy(GameObject.Find("" + room.getPositionKeyVector().x + "" + room.getPositionKeyVector().y + "" + (currentRoom.getPositionKeyVector().z - (mazeDimension - 1))));
        }
    }

    void ExpandBack(IRoom currentRoom)
    {
        float offset = roomDistance * (currentRoomDimension - 1);

        for (int bottom = 0; bottom < currentRoomDimension * 2 - 1; bottom++)
        {
            IRoom room = CreateRandomRoom(roomSize, roomHeight, roomSize, currentRoom.getPosition() + new Vector3(roomSize * (currentRoomDimension - 1) - roomSize * bottom + offset, 0, -roomSize * currentRoomDimension - roomDistance * currentRoomDimension));
            offset -= roomDistance;

            Destroy(GameObject.Find("" + room.getPositionKeyVector().x + "" + room.getPositionKeyVector().y + "" + (currentRoom.getPositionKeyVector().z + (mazeDimension - 1))));
        }
    }

    void ExpandRight(IRoom currentRoom)
    {
        float offset = roomDistance * (currentRoomDimension - 1);

        for (int right = 0; right < currentRoomDimension * 2 - 1; right++)
        {
            IRoom room = CreateRandomRoom(roomSize, roomHeight, roomSize, currentRoom.getPosition() + new Vector3(roomSize * currentRoomDimension + roomDistance * currentRoomDimension, 0, roomSize * (currentRoomDimension - 1) - roomSize * right + offset));
            offset -= roomDistance;

            Destroy(GameObject.Find("" + (currentRoom.getPositionKeyVector().x - (mazeDimension - 1)) + "" + room.getPositionKeyVector().y + "" + room.getPositionKeyVector().z));
        }
    }

    void ExpandLeft(IRoom currentRoom)
    {
        float offset = -roomDistance * (currentRoomDimension - 1);

        for (int left = 0; left < currentRoomDimension * 2 - 1; left++)
        {
            IRoom room = CreateRandomRoom(roomSize, roomHeight, roomSize, currentRoom.getPosition() + new Vector3(-roomSize * currentRoomDimension - roomDistance * currentRoomDimension, 0, -roomSize * (currentRoomDimension - 1) + roomSize * left + offset));
            offset += roomDistance;

            Destroy(GameObject.Find("" + (currentRoom.getPositionKeyVector().x + (mazeDimension - 1)) + "" + room.getPositionKeyVector().y + "" + room.getPositionKeyVector().z));
        }
    }

    IRoom CreateRandomRoom(float width, float height, float depth, Vector3 position)
    {
        GameObject room = new GameObject();
        RandomRoom roomInstance = room.AddComponent<RandomRoom>();
        roomInstance.Instantiate(width, height, depth, roomDistance, position);

        roomInstance.setState(Random.state);
        room.name = roomInstance.getPositionKeyString();
        room.transform.SetParent(allRooms.transform);
        room.AddComponent<MeshRenderer>().material = wallMaterial;
        room.AddComponent<MeshFilter>();
        room.AddComponent<MeshCollider>();

        State newRandomState = Random.state;
        bool resetRandomState = false;

        if (!rooms.ContainsKey(room.name))
        {
            rooms.Add(roomInstance.getPositionKeyString(), roomInstance);
        }
        else
        {
            resetRandomState = true;
            Random.state = ((IRoom)rooms[room.name]).getState();
            roomInstance.setState(Random.state);
        }

        // RANDOM DOORWAYS
        roomInstance.buildDoorways(this);

        // GROUND
        roomInstance.buildGround(this);

        // ROOF
        roomInstance.buildRoof(this);

        if (resetRandomState)
        {
            Random.state = newRandomState;
        }

        return roomInstance;
    }

    public void CombineMeshes(Transform parent)
    {
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        List<CombineInstance> combine = new List<CombineInstance>();

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].sharedMesh != null && meshFilters[i].gameObject.activeSelf)
            {
                CombineInstance current = new CombineInstance();
                current.mesh = meshFilters[i].sharedMesh;
                current.transform = meshFilters[i].transform.localToWorldMatrix;
                combine.Add(current);
                meshFilters[i].gameObject.SetActive(false);
            }
        }

        // safety
        if (parent.GetComponent<MeshFilter>() == null)
        {
            parent.gameObject.AddComponent<MeshFilter>();
        }
        if (parent.GetComponent<MeshCollider>() == null)
        {
            parent.gameObject.AddComponent<MeshCollider>();
        }
        if (parent.GetComponent<MeshRenderer>() == null && meshFilters.Length > 1)
        {
            parent.gameObject.AddComponent<MeshRenderer>().material = meshFilters[1].gameObject.GetComponent<MeshRenderer>().material;
        }

        var meshFilter = parent.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshFilter.mesh.CombineMeshes(combine.ToArray());
        parent.GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        parent.transform.gameObject.SetActive(true);

        parent.transform.localScale = new Vector3(1, 1, 1);
        parent.transform.rotation = Quaternion.identity;
        parent.transform.position = Vector3.zero;
    }

    private void Update()
    {
        string currentRoom = getCurrentRoom();
        //Debug.Log(currentRoom);
        if (!currentRoom.Equals(lastRoom))
        {
            IRoom last = (IRoom)rooms[lastRoom];
            IRoom current = (IRoom)rooms[currentRoom];

            if (last.getPositionKeyVector().x != current.getPositionKeyVector().x)
            {
                // right expansion
                if (last.getPositionKeyVector().x < current.getPositionKeyVector().x)
                {
                    ExpandRight(last);
                }
                // left expansion
                else
                {
                    ExpandLeft(last);
                }
            }else if (last.getPositionKeyVector().z != current.getPositionKeyVector().z)
            {
                // front expansion
                if (last.getPositionKeyVector().z < current.getPositionKeyVector().z)
                {
                    ExpandFront(last);
                }
                // back expansion
                else
                {
                    ExpandBack(last);
                }
            }
            lastRoom = currentRoom;
        }
    }

    public string getCurrentRoom()
    {
        Vector3 playerPosition = player.transform.position;
        float x = playerPosition.x / (roomDistance + roomSize);
        if (x < 0)
        {
            x = (playerPosition.x + roomDistance) / (roomDistance + roomSize) - 1;
        }

        float y = 0;

        float z = playerPosition.z / (roomDistance + roomSize);
        if (z < 0)
        {
            z = (playerPosition.z + roomDistance) / (roomDistance + roomSize) - 1;
        }
        return "" + (int)x + "" + (int)y + "" + (int)z;
    }
}
