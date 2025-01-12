using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class GridPlacer : MonoBehaviour
{
    [Header("Grid Settings")] public int gridWidth = 5;
    public int gridHeight = 5;
    public float cellSize = 1.0f;

    [Header("List GameObjects")] public List<GameObject> objectsToPlace;

    [Header("Grid Started Position")] public Vector3 startPosition = Vector3.zero;

    [Button]
    public void PlaceObjectsInGrid()
    {
        if (objectsToPlace == null || objectsToPlace.Count == 0)
        {
            Debug.LogWarning("List GameObjects is Empty!");

            return;
        }

        int totalObjects = gridWidth * gridHeight;

        for (int i = 0; i < totalObjects && i < objectsToPlace.Count; i++)
        {
            int x = i % gridWidth;
            int z = i / gridWidth;

            Vector3 position = startPosition + new Vector3(x * cellSize, 0, z * cellSize) + transform.position;

            GameObject obj = objectsToPlace[i];
            if (obj != null)
            {
                obj.transform.position = position;
            }
        }

        Debug.Log("Success.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 position = startPosition + new Vector3(x * cellSize, 0, z * cellSize) + transform.position;
                Gizmos.DrawWireSphere(position, 0.1f);
            }
        }
    }

    [Button]
    private void CollectChildObjects()
    {
        objectsToPlace.Clear();
        foreach (Transform child in transform)
            objectsToPlace.Add(child.gameObject);
    }
}