using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    byte[,,] voxelMap = new byte[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];

    void Start () {
        // world = GameObject.Find("World").GetComponent<World>();

        // Define the chunk content
        voxelMap = PopulateVoxelMap(voxelMap);
        
        // Create the chunk
        Mesh mesh = CreateChunkMesh(voxelMap);
        meshFilter.mesh = mesh;
    }

    byte[,,] PopulateVoxelMap(byte[,,] vMap) {
        // Loop through each voxel in the chunk
        for (int y = 0; y < VoxelData.ChunkHeight; y++){
            for (int x = 0; x < VoxelData.ChunkWidth; x++){
                for (int z = 0; z < VoxelData.ChunkWidth; z++){
                    vMap[x, y, z] = 0;
                }
            }
        }
        return vMap;
    }

    bool checkVoxel(Vector3 position, byte[,,] vMap){
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);

        bool xCheck = x < 0 || x > VoxelData.ChunkWidth - 1;
        bool yCheck = y < 0 || y > VoxelData.ChunkHeight - 1;
        bool zCheck = z < 0 || z > VoxelData.ChunkWidth - 1;

        // Check for out of bounds
        if (xCheck || yCheck || zCheck){
            return false;
        } else {
            return true; //vMap[x, y, z];
        }
    }

    Mesh CreateChunkMesh(byte[,,] vMap){
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        // Loop through each voxel in the chunk
        for (int y = 0; y < VoxelData.ChunkHeight; y++){
            for (int x = 0; x < VoxelData.ChunkWidth; x++){
                for (int z = 0; z < VoxelData.ChunkWidth; z++){
                    Vector3 offset = new Vector3(x, y, z);
                    // Looping though each face on a voxel
                    for (int i = 0; i < 6; i++){
                        bool outsideFace = !checkVoxel(offset + VoxelData.faceChecks[i], vMap);
                        if (outsideFace){
                            vertices.Add(offset + VoxelData.voxelVerts[VoxelData.voxelTriangles[i, 0]]);
                            vertices.Add(offset + VoxelData.voxelVerts[VoxelData.voxelTriangles[i, 1]]);
                            vertices.Add(offset + VoxelData.voxelVerts[VoxelData.voxelTriangles[i, 2]]);
                            vertices.Add(offset + VoxelData.voxelVerts[VoxelData.voxelTriangles[i, 3]]);
                            uvs.Add(VoxelData.voxelUvs[0]);
                            uvs.Add(VoxelData.voxelUvs[1]);
                            uvs.Add(VoxelData.voxelUvs[2]);
                            uvs.Add(VoxelData.voxelUvs[3]);
                            triangles.Add(vertexIndex);
                            triangles.Add(vertexIndex + 1);
                            triangles.Add(vertexIndex + 2);
                            triangles.Add(vertexIndex + 2);
                            triangles.Add(vertexIndex + 1);
                            triangles.Add(vertexIndex + 3);
                            vertexIndex += 4;
                        }
                    }
                }
            }
        }
        Mesh chunkMesh = new Mesh();
        chunkMesh.vertices = vertices.ToArray();
        chunkMesh.triangles = triangles.ToArray();
        chunkMesh.uv = uvs.ToArray();
        chunkMesh.RecalculateNormals();
        return chunkMesh;
    }

    void AddVoxelDataToChunk(Vector3 position) {
    }
}
