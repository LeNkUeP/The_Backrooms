using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public abstract class DefaultRoomImpl : MonoBehaviour, IRoom
{
    private int x;
    private int y;
    private int z;

    [SerializeField]
    private List<Doorway> frontDoors = new List<Doorway>();
    [SerializeField]
    private List<Doorway> backDoors = new List<Doorway>();
    [SerializeField]
    private List<Doorway> leftDoors = new List<Doorway>();
    [SerializeField]
    private List<Doorway> rightDoors = new List<Doorway>();

    private Vector3 position;

    private float width;
    private float height;
    private float depth;

    private State state;

    public void Instantiate(float width, float height, float depth, int roomDistance, Vector3 position)
    {
        this.position = position;

        this.x = (int)(position.x / (width+roomDistance));
        this.y = (int)(position.y / height);
        this.z = (int)(position.z / (width+roomDistance));

        this.width = width;
        this.height = height;
        this.depth = depth;
    }

    public void setState(State state)
    {
        this.state = state;
    }

    public State getState()
    {
        return this.state;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }

    public Vector3 getPositionKeyVector()
    {
        return new Vector3(x,y,z);
    }

    public string getPositionKeyString()
    {
        return "" + x + "" + y + "" + z;
    }

    public float getWidth()
    {
        return this.width;
    }

    public float getHeight()
    {
        return this.height;
    }

    public float getDepth()
    {
        return this.depth;
    }

    public void addFrontDoor(Doorway door)
    {
        this.frontDoors.Add(door);
    }

    public void addBackDoor(Doorway door)
    {
        this.backDoors.Add(door);
    }

    public void addLeftDoor(Doorway door)
    {
        this.leftDoors.Add(door);
    }

    public void addRightDoor(Doorway door)
    {
        this.rightDoors.Add(door);
    }

    public List<Doorway> getFrontDoorways()
    {
        return this.frontDoors;
    }

    public List<Doorway> getBackDoorways()
    {
        return this.backDoors;
    }

    public List<Doorway> getLeftDoorways()
    {
        return this.leftDoors;
    }

    public List<Doorway> getRightDoorways()
    {
        return this.rightDoors;
    }

    public abstract void buildDoorways(Level_0_Generator level_0);
    public abstract void buildRoof(Level_0_Generator level_0);
    public abstract void buildGround(Level_0_Generator level_0);
}
