using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coordinates
{
    [SerializeField]
    private int x, z;
    public int X {
        get
        {
            return x;
        }
    }
    public int Z { 
        get
        {
            return z;
        }
    }

    public Coordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static Coordinates FromOffsetCoordinates (int x, int z)
    {
        return new Coordinates(x - z/2, z);
    }

    public static Coordinates FromPosition (Vector3 position)
    {
        float x = position.x / (Metrics.ir * 2f);
        float y = -x;
        float offset = position.z / (Metrics.or * 3f);
        x -= offset;
        y -= offset;
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);
        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);
            if (dX > dY && dX > dZ)
            {
                iX = iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }
        return new Coordinates(iX, iZ);
    }

    public int Y
    {
        get
        {
            return -X - Z;
        }
    }
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n " + Y.ToString() + "\n" + Z.ToString();
    }
}
