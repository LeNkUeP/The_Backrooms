using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Random;

public interface IRoom
{
    public void Instantiate(float width, float height, float depth, int roomDistance, Vector3 position);

    public void setState(State state);
    public State getState();

    public Vector3 getPosition();
    public Vector3 getPositionKeyVector();
    public string getPositionKeyString();

    public float getWidth();
    public float getHeight();
    public float getDepth();

    public void buildDoorways(Level_0_Generator level_0);

    public void addFrontDoor(Doorway door);
    public void addBackDoor(Doorway door);
    public void addLeftDoor(Doorway door);
    public void addRightDoor(Doorway door);

    public List<Doorway> getFrontDoorways();
    public List<Doorway> getBackDoorways();
    public List<Doorway> getLeftDoorways();
    public List<Doorway> getRightDoorways();

    public void buildRoof(Level_0_Generator level_0);

    public void buildGround(Level_0_Generator level_0);
}
