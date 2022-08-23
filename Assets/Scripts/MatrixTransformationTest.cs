using System;
using UnityEngine;

public class MatrixTransformationTest : MonoBehaviour
{
    [Header("Transformation Matrix XYZ: col01 - col03")]
    public Vector4 col01 = new Vector4(1, 0, 0, 0);
    public Vector4 col02 = new Vector4(0, 1, 0, 0);
    public Vector4 col03 = new Vector4(0, 0, 1, 0);
    [Tooltip("Modify col04 to see the effect")]
    public Vector4 col04 = new Vector4(1, 1, 1, 1);

    private MeshFilter _meshFilter;
    private Vector3[] _originalVerts;
    private Vector3[] _newVerts;
    private Matrix4x4 _matrix4X4;
    private Vector4 _originalVector = Vector4.zero;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _originalVerts = _meshFilter.mesh.vertices;
        _newVerts = new Vector3[_originalVerts.Length];
    }

    private void Update()
    {
        // Matrix transformation copied from https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Matrix4x4.cs
        _matrix4X4.m00 = col01.x; _matrix4X4.m01 = col02.x; _matrix4X4.m02 = col03.x; _matrix4X4.m03 = col04.x; 
        _matrix4X4.m10 = col01.y; _matrix4X4.m11 = col02.y; _matrix4X4.m12 = col03.y; _matrix4X4.m13 = col04.y; 
        _matrix4X4.m20 = col01.z; _matrix4X4.m21 = col02.z; _matrix4X4.m22 = col03.z; _matrix4X4.m23 = col04.z; 
        _matrix4X4.m30 = col01.w; _matrix4X4.m31 = col02.w; _matrix4X4.m32 = col03.w; _matrix4X4.m33 = col04.w;

        for (int i = 0; i < _originalVerts.Length; i++) {
            _originalVector = new Vector4(_originalVerts[i].x, _originalVerts[i].y, _originalVerts[i].z, 1);
            _newVerts[i] = _matrix4X4 * _originalVector;
        }
        _meshFilter.mesh.vertices = _newVerts;
    }
}
