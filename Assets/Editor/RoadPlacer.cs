using UnityEngine;
using UnityEditor;

public class RoadPlacer : EditorWindow
{
    public GameObject roadPrefab;
    public int numberOfSegments = 10;
    public float segmentLength = 10f;
    public Vector3 startPosition = Vector3.zero;

    [MenuItem("Tools/Road Placer")]
    public static void ShowWindow()
    {
        GetWindow<RoadPlacer>("Road Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Automatic Road Placer",
            EditorStyles.boldLabel);

        roadPrefab = (GameObject)EditorGUILayout.ObjectField(
            "Road Prefab",
            roadPrefab,
            typeof(GameObject),
            false);

        numberOfSegments = EditorGUILayout.IntField(
            "Number of Segments", numberOfSegments);

        segmentLength = EditorGUILayout.FloatField(
            "Segment Length", segmentLength);

        startPosition = EditorGUILayout.Vector3Field(
            "Start Position", startPosition);

        if (GUILayout.Button("Place Road"))
        {
            PlaceRoad();
        }
    }

    private void PlaceRoad()
    {
        if (roadPrefab == null)
        {
            Debug.LogWarning("Please assign a road prefab!");
            return;
        }

        if (numberOfSegments < 1 || segmentLength <= 0)
        {
            Debug.LogWarning("Enter valid road settings!");
            return;
        }

        GameObject roadParent = new GameObject("Generated Road");

        Undo.RegisterCreatedObjectUndo(
            roadParent, "Create Road");

        for (int i = 0; i < numberOfSegments; i++)
        {
            Vector3 position = startPosition
                + Vector3.forward * (i * segmentLength);

            GameObject road = (GameObject)PrefabUtility.InstantiatePrefab(
                roadPrefab);

            Undo.RegisterCreatedObjectUndo(
                road, "Place Road Segment");

            road.transform.position = position;
            road.transform.SetParent(roadParent.transform);
        }

        Debug.Log("Road placed successfully!");
    }
}
