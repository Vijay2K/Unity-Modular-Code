using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static GridPosition operator+(GridPosition a, GridPosition b)
    {
        int x = a.x + b.x;
        int z = a.z + b.z;

        return new GridPosition(x, z);
    }  

    public static GridPosition operator-(GridPosition a, GridPosition b)
    {
        int x = a.x - b.x;
        int z = a.z - b.z;

        return new GridPosition(x, z);
    }

    public static bool operator==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator!=(GridPosition a, GridPosition b)
    {
        return a.x != b.x && a.z != b.z;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

}
