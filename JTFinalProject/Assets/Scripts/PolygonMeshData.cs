﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PolygonMeshData
{
    public static Vector3[] verticies = {
            new Vector3( 1, 1, 1),
            new Vector3(-1, 1, 1),
            new Vector3(-1,-1, 1),
            new Vector3( 1,-1, 1),
            new Vector3(-1, 1,-1),
            new Vector3( 1, 1,-1),
            new Vector3( 1,-1,-1),
            new Vector3(-1,-1,-1)
        };
    public static int[][] faceTriangles = {
        new int[]{ 0,1,2,3 },
        new int[]{ 5,0,3,6 },
        new int[]{ 4,5,6,7 },
        new int[]{ 1,4,7,2 },
        new int[]{ 5,4,1,0 },
        new int[]{ 3,2,7,6 }
    };

    public static Vector3[] faceVerticies(int dir)
    {
        Vector3[] fv = new Vector3[3];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticies[faceTriangles[dir][i]];
        }
        return fv;
    }
}
