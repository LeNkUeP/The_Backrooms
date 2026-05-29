using System;
using UnityEngine;

[Serializable]
public class Doorway
{
    public bool front;
    public bool back;
    public bool left;
    public bool right;

    public Vector3 position;
    public float width;

    public Doorway(bool front, bool back, bool left, bool right, Vector3 position, float width)
    {
        this.front = front;
        this.back = back;
        this.left = left;
        this.right = right;
        this.position = position;
        this.width = width;
    }
}
