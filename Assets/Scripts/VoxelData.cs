using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData {


    public static readonly int ChunkWidth = 8;
    public static readonly int ChunkHeight = 16;

    // Lookup for the vectors of a square
    public static readonly Vector3[] voxelVerts = new Vector3[8]{
        new Vector3(1.0f, 1.0f, 1.0f), // 0
        new Vector3(1.0f, 1.0f, 0.0f), // 1
        new Vector3(1.0f, 0.0f, 1.0f), // 2
        new Vector3(1.0f, 0.0f, 0.0f), // 3
        new Vector3(0.0f, 1.0f, 1.0f), // 4
        new Vector3(0.0f, 1.0f, 0.0f), // 5
        new Vector3(0.0f, 0.0f, 1.0f), // 6
        new Vector3(0.0f, 0.0f, 0.0f), // 7
    };

    public static readonly Vector3[] faceChecks = new Vector3[6]{
        new Vector3(0.0f, 1.0f, 0.0f),  // Front
        new Vector3(0.0f, -1.0f, 0.0f), // Back
        new Vector3(1.0f, 0.0f, 0.0f),  // Top
        new Vector3(-1.0f, 0.0f, 0.0f), // Bottom
        new Vector3(0.0f, 0.0f, -1.0f), // Left
        new Vector3(0.0f, 0.0f, 1.0f),  // Right
    };

    // Lookup for the draw patterns on each face
    public static readonly int[,] voxelTriangles = new int[6, 4] {
        {0, 1, 4, 5}, // Top face
        {6, 7, 2, 3}, // Bottom face
        {0, 2, 1, 3}, // Front face
        {5, 7, 4, 6}, // Back face
        {1, 3, 5, 7}, // Left face
        {4, 6, 0, 2} // Right face
    };


    // Lookup for UV mappings
    public static readonly Vector2[] voxelUvs = new Vector2[4] {
        new Vector2(1.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(0.0f, 0.0f)
    };
}
