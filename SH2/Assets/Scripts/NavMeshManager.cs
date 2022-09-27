using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-103)]
public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface _surface;

    private void Start()
    {
        _surface = gameObject.GetComponent<NavMeshSurface>();
    }

    public void BakeNavMesh()
    {
        _surface.BuildNavMesh();
    }
}
