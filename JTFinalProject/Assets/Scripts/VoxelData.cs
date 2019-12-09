using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    public int[,,] data = new int[,,]//Columns = x, Rows= y, Each Point = z 
    {
        {
            { 1,1,1 },{ 0,0,0 },{ 0,0,0 }
         },
         {
            { 1,1,1 },{ 0,1,0 },{ 0,0,0 }
         },
         {
            { 1,1,1 },{ 0, 0, 0 }, { 0,0,0 }
         },
    };
    public int Width
    {
        get { return data.GetLength(0); }
    }
    public void GenerateCone(int radius, int height)
    {
        float slope = height / radius;
        data = new int[radius * 2, height, radius * 2];
        Vector3 center = new Vector3(radius-1, 0, radius-1);
        for (int z = 0; z < radius*2; z++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < (radius * 2); x++)
                {
                    if (y == 0)
                    {
                        data[x, y, z] = 1;
                    }
                    else if (x + z > ((x - center.x) * (x - center.x)) + ((z - center.z) * (z - center.z)))
                    {
                        data[x, y, z] = 0;
                    } else {
                        if (x >= radius)
                        {
                            if (y==radius*2-x)
                            {
                                data[x, y, z] = 1;
                                Debug.Log(x + " " + z);
                            }
                        }
                        else
                        {
                            if (y == x)
                            {
                                data[x, y, z] = 1;
                                Debug.Log(x + " " + z);
                            }
                        }
                    }
                }
            }
        }
    }
    public int Height
    {
        get { return data.GetLength(1); }
    }

    public int Depth
    {
        get { return data.GetLength(2); }
    }
    public int GetCell(int x, int y, int z)
    {
        return data[x, y, z];
    }
    public int GetNeighbor(int x, int y, int z, Direction dir)
    {
        DataCoordinate offsetToCheck = offsets[(int)dir];
        DataCoordinate neighborCoord = new DataCoordinate(x + offsetToCheck.x, y + offsetToCheck.y, z + offsetToCheck.z);
        if (neighborCoord.x < 0 || neighborCoord.x >= Width || neighborCoord.y < 0 || neighborCoord.y > Height || neighborCoord.z < 0 || neighborCoord.z >= Depth)
        {
            return 0;
        } else
        {
            return GetCell(neighborCoord.x, neighborCoord.y, neighborCoord.z);
        }
    }

    struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;
        public DataCoordinate(int x,int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    DataCoordinate[] offsets =
    {
        new DataCoordinate(0,0,1),
        new DataCoordinate(1,0,0),
        new DataCoordinate(0,0,-1),
        new DataCoordinate(-1,0,0),
        new DataCoordinate(0,1,0),
        new DataCoordinate(0,-1,0),
    };
}


public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    Down
}