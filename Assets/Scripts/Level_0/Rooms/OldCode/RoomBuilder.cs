using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomBuilder
{
    Level_0_Generator generator;

    public RoomBuilder(Level_0_Generator generator)
    {
        this.generator = generator;
    }

    public void CreateRoom(DefaultRoomImpl room)
    {
        // RANDOM DOORWAYS
        room.buildDoorways(generator);
        CreateDoorways(room);

        // GROUND
       // room.buildGround(this);
        //CreateGround(room);

        // ROOF
        //room.buildRoof(this);
        //CreateRoof(room);
    }

    private void CreateDoorways(DefaultRoomImpl room)
    {
        float width = room.getWidth();
        float height = room.getHeight();
        float depth = room.getDepth();
        Vector3 position = room.getPosition();
        Vector3 positionKeys = room.getPositionKeyVector();
        float currentPosition = 0;

        GameObject doors = new GameObject("Doors");
        GameObject door;

        string frontNeighbourPosition = "" + positionKeys.x + "" + positionKeys.y + "" + (positionKeys.z + 1);
        string rightNeighbourPosition = "" + (positionKeys.x + 1) + "" + positionKeys.y + "" + positionKeys.z;
        string bottomNeighbourPosition = "" + positionKeys.x + "" + positionKeys.y + "" + (positionKeys.z - 1);
        string leftNeighbourPosition = "" + (positionKeys.x - 1) + "" + positionKeys.y + "" + positionKeys.z;

        List<Doorway> frontDoorways = room.getFrontDoorways();
        List<Doorway> backDoorways = room.getBackDoorways();
        List<Doorway> rightDoorways = room.getRightDoorways();
        List<Doorway> leftDoorways = room.getLeftDoorways();

        // FRONT
        if (frontDoorways.Count != 0)
        {
            for (int i = 0; i < frontDoorways.Count; i++)
            {
                Doorway currentDoorway = frontDoorways[i];
                door = Level_0_Generator.meshCreator.CreateBackRectangle(currentDoorway.position.x - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                door.transform.SetParent(doors.transform);
                currentPosition = currentDoorway.position.x + currentDoorway.width;
                // if neighbour -> fill gap between rooms
                if (generator.rooms.ContainsKey(frontNeighbourPosition) && generator.roomDistance > 0)
                {
                    door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, generator.roomDistance, position + new Vector3(currentDoorway.position.x, 0, width));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                    door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, generator.roomDistance, position + new Vector3(currentPosition - width, 0, width));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                }
            }
            door = Level_0_Generator.meshCreator.CreateBackRectangle(width - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
            door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
            door.transform.SetParent(doors.transform);
        }

        // BACK
        if (backDoorways.Count != 0)
        {
            currentPosition = 0;
            for (int i = 0; i < backDoorways.Count; i++)
            {
                Doorway currentDoorway = backDoorways[i];
                door = Level_0_Generator.meshCreator.CreateFrontRectangle(currentDoorway.position.x - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
                door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                door.transform.SetParent(doors.transform);
                currentPosition = currentDoorway.position.x + currentDoorway.width;
                // if neighbour -> fill gap between rooms
                if (generator.rooms.ContainsKey(bottomNeighbourPosition) && generator.roomDistance > 0)
                {
                    door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, generator.roomDistance, position + new Vector3(currentDoorway.position.x, 0, -generator.roomDistance));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                    door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, generator.roomDistance, position + new Vector3(currentPosition - width, 0, -generator.roomDistance));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                }
            }
            door = Level_0_Generator.meshCreator.CreateFrontRectangle(width - currentPosition, height, depth, position + new Vector3(currentPosition, 0, 0));
            door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
            door.transform.SetParent(doors.transform);
        }

        // RIGHT
        if (rightDoorways.Count != 0)
        {
            currentPosition = 0;
            for (int i = 0; i < rightDoorways.Count; i++)
            {
                Doorway currentDoorway = rightDoorways[i];
                door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, currentDoorway.position.z - currentPosition, position + new Vector3(0, 0, currentPosition));
                door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                door.transform.SetParent(doors.transform);
                currentPosition = currentDoorway.position.z + currentDoorway.width;
                // if neighbour -> fill gap between rooms
                if (generator.rooms.ContainsKey(rightNeighbourPosition) && generator.roomDistance > 0)
                {
                    door = Level_0_Generator.meshCreator.CreateFrontRectangle(generator.roomDistance, height, depth, position + new Vector3(width, 0, currentDoorway.position.z));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                    door = Level_0_Generator.meshCreator.CreateBackRectangle(generator.roomDistance, height, depth, position + new Vector3(width, 0, currentPosition - width));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                }
            }
            door = Level_0_Generator.meshCreator.CreateRightRectangle(width, height, depth - currentPosition, position + new Vector3(0, 0, currentPosition));
            door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
            door.transform.SetParent(doors.transform);
        }

        // LEFT
        if (leftDoorways.Count != 0)
        {
            currentPosition = 0;
            for (int i = 0; i < leftDoorways.Count; i++)
            {
                Doorway currentDoorway = leftDoorways[i];
                door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, currentDoorway.position.z - currentPosition, position + new Vector3(0, 0, currentPosition));
                door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                door.transform.SetParent(doors.transform);
                currentPosition = currentDoorway.position.z + currentDoorway.width;
                // if neighbour -> fill gap between rooms
                if (generator.rooms.ContainsKey(leftNeighbourPosition) && generator.roomDistance > 0)
                {
                    door = Level_0_Generator.meshCreator.CreateFrontRectangle(generator.roomDistance, height, depth, position + new Vector3(-generator.roomDistance, 0, currentDoorway.position.z));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                    door = Level_0_Generator.meshCreator.CreateBackRectangle(generator.roomDistance, height, depth, position + new Vector3(-generator.roomDistance, 0, currentPosition - width));
                    door.transform.SetParent(doors.transform);
                    door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
                }
            }
            door = Level_0_Generator.meshCreator.CreateLeftRectangle(width, height, depth - currentPosition, position + new Vector3(0, 0, currentPosition));
            door.GetComponent<MeshRenderer>().material = generator.wallMaterial;
            door.transform.SetParent(doors.transform);
        }

        doors.transform.SetParent(room.transform);
        generator.CombineMeshes(doors.transform);
    }

    private void CreateGround(IRoom room)
    {

    }

    private void CreateRoof(IRoom room)
    {

    }
}
