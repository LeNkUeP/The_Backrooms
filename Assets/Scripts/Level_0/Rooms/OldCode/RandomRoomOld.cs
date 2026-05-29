using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomRoomOld : DefaultRoomImpl
{
    public override void buildDoorways(Level_0_Generator Level_0)
    {
        float width = getWidth();
        float height = getHeight();
        float depth = getDepth();
        Vector3 position = getPosition();
        Vector3 positionKeys = getPositionKeyVector();

        int numberOfDoors;

        bool hasOneDoor;
        bool hasNoWall = false;

        float availableWidth;
        float currentPosition;

        GameObject doors = new GameObject("Doors");
        GameObject door;
        Doorway doorInstance;

        string frontNeighbourPosition = "" + positionKeys.x + "" + positionKeys.y + "" + (positionKeys.z + 1);
        string rightNeighbourPosition = "" + (positionKeys.x + 1) + "" + positionKeys.y + "" + positionKeys.z;
        string bottomNeighbourPosition = "" + positionKeys.x + "" + positionKeys.y + "" + (positionKeys.z - 1);
        string leftNeighbourPosition = "" + (positionKeys.x - 1) + "" + positionKeys.y + "" + positionKeys.z;

        int maximalnumberofdoors = (int)((width + Level_0.minimalDoorDistance) / (Level_0.minimalDoorWidth + Level_0.minimalDoorDistance));
        if (maximalnumberofdoors == 0)
        {
            return;
        }

        for (int side = 0; side < 4; side++)
        {
            hasOneDoor = Random.value <= Level_0.wallHasOneDoor;
            if (hasOneDoor || maximalnumberofdoors == 1)
            {
                numberOfDoors = 1;
                hasNoWall = Random.value <= Level_0.noWall;
            }
            else
            {
                numberOfDoors = Random.Range(2, maximalnumberofdoors);
            }

            if (!hasNoWall)
            {
                availableWidth = width;
                currentPosition = 0;
                for (int i = 0; i < numberOfDoors; i++)
                {
                    float maxPossibleDoorWidth = availableWidth - ((numberOfDoors - i - 1) * Level_0.minimalDoorWidth) - ((numberOfDoors - i + 1) * Level_0.minimalDoorDistance);
                    float doorWidth = Random.Range(Level_0.minimalDoorWidth, maxPossibleDoorWidth);
                    float maxPossibleDoorPosition = width - doorWidth - ((numberOfDoors - i - 1) * Level_0.minimalDoorWidth) - ((numberOfDoors - i) * Level_0.minimalDoorDistance);
                    float doorPosition = Random.Range(currentPosition + Level_0.minimalDoorDistance, maxPossibleDoorPosition);

                    if (doorPosition != 0)
                    {
                        // FRONT
                        if (side == 0)
                        {
                            // if no neighbour -> normal creation
                            if (!Level_0.rooms.ContainsKey(frontNeighbourPosition))
                            {
                                door = Level_0_Generator.meshCreator.CreateBackRectangle(doorPosition - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                                door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                doorInstance = new Doorway(true, false, false, false, new Vector3(doorPosition, 0, 0), doorWidth);
                                addFrontDoor(doorInstance);
                            }
                            // if neighbour -> get doorways and build symmetrical
                            else
                            {
                                IRoom neighbour = (IRoom)Level_0.rooms[frontNeighbourPosition];
                                List<Doorway> backdoors = neighbour.getBackDoorways();
                                foreach (Doorway neighbourDoor in backdoors)
                                {
                                    door = Level_0_Generator.meshCreator.CreateBackRectangle(neighbourDoor.position.x - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                                    door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    doorInstance = new Doorway(true, false, false, false, new Vector3(neighbourDoor.position.x, 0, 0), neighbourDoor.width);
                                    door.transform.SetParent(doors.transform);
                                    currentPosition = neighbourDoor.position.x + neighbourDoor.width;
                                    addFrontDoor(doorInstance);

                                    // fill gaps if room distance is set
                                    if (Level_0.roomDistance > 0)
                                    {
                                        door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, Level_0.roomDistance, position + new Vector3(neighbourDoor.position.x, 0, Level_0.roomSize));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                        door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, Level_0.roomDistance, position + new Vector3(currentPosition - Level_0.roomSize, 0, Level_0.roomSize));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    }
                                }
                                break;
                            }
                        }
                        // BACK
                        else if (side == 1)
                        {
                            // if no neighbour -> normal creation
                            if (!Level_0.rooms.ContainsKey(bottomNeighbourPosition))
                            {
                                door = Level_0_Generator.meshCreator.CreateFrontRectangle(doorPosition - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                                doorInstance = new Doorway(false, true, false, false, new Vector3(doorPosition, 0, 0), doorWidth);
                                addBackDoor(doorInstance);
                            }
                            // if neighbour -> get doorways and build symmetrical
                            else
                            {
                                IRoom neighbour = (IRoom)Level_0.rooms[bottomNeighbourPosition];
                                List<Doorway> frontdoors = neighbour.getFrontDoorways();
                                foreach (Doorway neighbourDoor in frontdoors)
                                {
                                    door = Level_0_Generator.meshCreator.CreateFrontRectangle(neighbourDoor.position.x - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                                    door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    door.transform.SetParent(doors.transform);
                                    currentPosition = neighbourDoor.position.x + neighbourDoor.width;
                                    doorInstance = new Doorway(false, true, false, false, new Vector3(neighbourDoor.position.x, 0, 0), neighbourDoor.width);
                                    addBackDoor(doorInstance);

                                    // fill gaps if room distance is set
                                    if (Level_0.roomDistance > 0)
                                    {
                                        door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, Level_0.roomDistance, position + new Vector3(neighbourDoor.position.x, 0, -Level_0.roomDistance));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                        door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, Level_0.roomDistance, position + new Vector3(currentPosition - Level_0.roomSize, 0, -Level_0.roomDistance));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    }
                                }
                                break;
                            }
                        }
                        // RIGHT
                        else if (side == 2)
                        {
                            // if no neighbour -> normal creation
                            if (!Level_0.rooms.ContainsKey(rightNeighbourPosition))
                            {
                                door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, doorPosition - currentPosition, position + new Vector3(0, 0, currentPosition));
                                doorInstance = new Doorway(false, false, false, true, new Vector3(0, 0, doorPosition), doorWidth);
                                addRightDoor(doorInstance);
                            }
                            // if neighbour -> get doorways and build symmetrical
                            else
                            {
                                IRoom neighbour = (IRoom)Level_0.rooms[rightNeighbourPosition];
                                List<Doorway> leftdoors = neighbour.getLeftDoorways();
                                foreach (Doorway neighbourDoor in leftdoors)
                                {
                                    door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, neighbourDoor.position.z - currentPosition, position + new Vector3(0, 0, currentPosition));
                                    door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    door.transform.SetParent(doors.transform);
                                    currentPosition = neighbourDoor.position.z + neighbourDoor.width;
                                    doorInstance = new Doorway(false, false, false, true, new Vector3(0, 0, neighbourDoor.position.z), neighbourDoor.width);
                                    addRightDoor(doorInstance);

                                    // fill gaps if room distance is set
                                    if (Level_0.roomDistance > 0)
                                    {
                                        door = Level_0_Generator.meshCreator.CreateFrontRectangle(Level_0.roomDistance, height, depth, position + new Vector3(Level_0.roomSize, 0, neighbourDoor.position.z));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                        door = Level_0_Generator.meshCreator.CreateBackRectangle(Level_0.roomDistance, height, depth, position + new Vector3(Level_0.roomSize, 0, currentPosition - Level_0.roomSize));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    }
                                }
                                break;
                            }
                        }
                        // LEFT
                        else
                        {
                            // if no neighbour -> normal creation
                            if (!Level_0.rooms.ContainsKey(leftNeighbourPosition))
                            {
                                door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, doorPosition - currentPosition, position + new Vector3(0, 0, currentPosition));
                                doorInstance = new Doorway(false, false, true, false, new Vector3(0, 0, doorPosition), doorWidth);
                                addLeftDoor(doorInstance);
                            }
                            // if neighbour -> get doorways and build symmetrical
                            else
                            {
                                IRoom neighbour = (IRoom)Level_0.rooms[leftNeighbourPosition];
                                List<Doorway> rightdoors = neighbour.getRightDoorways();
                                foreach (Doorway neighbourDoor in rightdoors)
                                {
                                    door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, neighbourDoor.position.z - currentPosition, position + new Vector3(0, 0, currentPosition));
                                    door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                    door.transform.SetParent(doors.transform);
                                    currentPosition = neighbourDoor.position.z + neighbourDoor.width;
                                    doorInstance = new Doorway(false, false, true, false, new Vector3(0, 0, neighbourDoor.position.z), neighbourDoor.width);
                                    addLeftDoor(doorInstance);

                                    // fill gaps if room distance is set
                                    if (Level_0.roomDistance > 0)
                                    {
                                        door = Level_0_Generator.meshCreator.CreateFrontRectangle(Level_0.roomDistance, height, depth, position + new Vector3(-Level_0.roomDistance, 0, neighbourDoor.position.z));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
                                        door = Level_0_Generator.meshCreator.CreateBackRectangle(Level_0.roomDistance, height, depth, position + new Vector3(-Level_0.roomDistance, 0, currentPosition - Level_0.roomSize));
                                        door.transform.SetParent(doors.transform);
                                        door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;

                                    }
                                }
                                break;
                            }
                        }
                        door.transform.SetParent(doors.transform);
                    }

                    // update current position and available width
                    currentPosition = doorPosition + doorWidth;
                    availableWidth = width - currentPosition;
                }

                // Last piece of wall
                if (side == 0)
                {
                    door = Level_0_Generator.meshCreator.CreateBackRectangle(width - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                }
                else if (side == 1)
                {
                    door = Level_0_Generator.meshCreator.CreateFrontRectangle(width - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                }
                else if (side == 2)
                {
                    door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, depth - currentPosition, position + new Vector3(0, 0, currentPosition));
                }
                else
                {
                    door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, depth - currentPosition, position + new Vector3(0, 0, currentPosition));
                }
                door.transform.SetParent(doors.transform);
                door.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;
            }
        }

        doors.transform.SetParent(transform);
        Level_0.CombineMeshes(doors.transform);
    }

    public override void buildGround(Level_0_Generator Level_0)
    {
        Vector3 position = getPosition();
        float width = getWidth();
        float depth = getDepth();

        GameObject ground = new GameObject("Ground");
        ground.transform.SetParent(transform);
        ground.AddComponent<MeshRenderer>().material = Level_0.groundMaterial;

        bool isHoleGround = Random.value <= Level_0.groundHasHole;
        int numberOfHoles = Random.Range(2, Level_0.maxNumberOfHolesInGround);

        if (isHoleGround)
        {
            CreateGroundWithHoles(width, 100, depth, numberOfHoles, position, Level_0).transform.SetParent(ground.transform);
        }
        else
        {
            CreateCubicGround(width, 100, depth, position, Level_0).transform.SetParent(ground.transform);
        }

        // fill surrounding
        if (Level_0.roomDistance > 0)
        {
            CreateCubicGround(width + Level_0.roomDistance, 100, Level_0.roomDistance / 2f, position + new Vector3(-Level_0.roomDistance / 2f, 0, -Level_0.roomDistance / 2f), Level_0).transform.SetParent(ground.transform);
            CreateCubicGround(Level_0.roomDistance / 2f, 100, depth, position + new Vector3(width, 0, 0), Level_0).transform.SetParent(ground.transform);
            CreateCubicGround(width + Level_0.roomDistance, 100, Level_0.roomDistance / 2f, position + new Vector3(-Level_0.roomDistance / 2f, 0, depth), Level_0).transform.SetParent(ground.transform);
            CreateCubicGround(Level_0.roomDistance / 2f, 100, depth, position + new Vector3(-Level_0.roomDistance / 2f, 0, 0), Level_0).transform.SetParent(ground.transform);
        }
        Level_0.CombineMeshes(ground.transform);
    }

    GameObject CreatePlaneGround(float width, float height, float depth, Vector3 position, Level_0_Generator Level_0)
    {
        GameObject ground = Level_0_Generator.meshCreator.CreateBottomRectangle(width, height, depth, position);
        ground.GetComponent<MeshRenderer>().material = Level_0.groundMaterial;
        ground.name = "ground";

        return ground;
    }

    GameObject CreateCubicGround(float width, float height, float depth, Vector3 position, Level_0_Generator Level_0)
    {
        GameObject ground = Level_0_Generator.meshCreator.CreateCube(width, height, depth, position + new Vector3(0, -height, 0));
        ground.GetComponent<MeshRenderer>().material = Level_0.groundMaterial;
        ground.name = "ground";

        return ground;
    }

    GameObject CreateGroundWithHoles(float width, float height, float depth, int numberofHoles, Vector3 position, Level_0_Generator Level_0)
    {
        GameObject ground = new GameObject(numberofHoles + "holeground");
        ground.AddComponent<MeshRenderer>().material = Level_0.groundMaterial;
        ground.AddComponent<MeshFilter>();
        ground.AddComponent<MeshCollider>();

        float widthOfHole = width / numberofHoles;
        float depthOfHole = depth / numberofHoles;

        bool switchHole = Random.value >= 0.5;

        if (numberofHoles > 1)
        {
            for (int z = 0; z < numberofHoles; z++)
            {
                for (int x = 0; x < numberofHoles; x++)
                {
                    if (!switchHole)
                    {
                        GameObject current = Level_0_Generator.meshCreator.CreateCube(widthOfHole, height, depthOfHole, position + new Vector3(x * widthOfHole, -height, 0));
                        current.transform.SetParent(ground.transform);
                        current.GetComponent<MeshRenderer>().material = Level_0.groundMaterial;
                    }
                    switchHole = !switchHole;
                }
                position = position + new Vector3(0, 0, depthOfHole);
                if (numberofHoles % 2 == 0)
                {
                    switchHole = !switchHole;
                }
            }
        }

        return ground;
    }

    public override void buildRoof(Level_0_Generator Level_0)
    {
        Vector3 position = getPosition();
        float width = getWidth();
        float depth = getDepth();

        GameObject roofContainer = new GameObject("Roof");
        roofContainer.transform.SetParent(transform);

        GameObject roof = Level_0_Generator.meshCreator.CreateTopRectangle(width + Level_0.roomDistance, 0.1f, depth + Level_0.roomDistance, position + new Vector3(-Level_0.roomDistance / 2f, Level_0.roomHeight, -Level_0.roomDistance / 2f));
        roof.GetComponent<MeshRenderer>().material = Level_0.roofMaterial;
        roof.transform.SetParent(roofContainer.transform);
        roof.name = "roof";
    }

    GameObject CreateWall(float width, float height, float depth, Vector3 position, Level_0_Generator Level_0)
    {
        GameObject wall = Level_0_Generator.meshCreator.CreateCube(width, height, depth, position);
        wall.GetComponent<MeshRenderer>().material = Level_0.wallMaterial;

        return wall;
    }

    GameObject CreateRandomLamps(Vector3 position, int numberoflights, Level_0_Generator Level_0)
    {
        GameObject lamps = new GameObject("Lamps");

        float increment = Level_0.roomSize / numberoflights;

        for (int i = 1; i < numberoflights; i++)
        {
            for (int r = 1; r < numberoflights; r++)
            {
                bool isLighted = Random.value <= Level_0.lightIsOn;
                GameObject current = Instantiate(Level_0.lamp, new Vector3(position.x + r * increment, position.y, position.z + i * increment), Quaternion.identity);
                current.transform.SetParent(lamps.transform);
                current.GetComponentInChildren<Light>().spotAngle = Level_0.roomSize * 3;
                if (!isLighted)
                {
                    current.GetComponentInChildren<Light>().enabled = false;
                    current.GetComponentsInChildren<MeshRenderer>()[1].material = Level_0.disabledLightMaterial;
                }
            }
        }

        return lamps;
    }

    GameObject CreateCurve(float width, float height, float depth, Vector3 position, Level_0_Generator Level_0)
    {
        GameObject curve = Level_0_Generator.meshCreator.CreateFrontCurve(width, height, depth, 0, 0, 10, position);
        curve.GetComponent<MeshRenderer>().material = Level_0.curveMaterial;

        return curve;
    }
}
