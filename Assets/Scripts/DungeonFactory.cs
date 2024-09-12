using System.Collections.Generic;
using UnityEngine;

public class DungeonFactory : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public int maxLevels;
    public int maxChildrenPerNode;
    public float nodeSpacing; // Horizontal spacing between nodes
    public float levelSpacing; // Vertical spacing between levels
    public int maxWidthPerLevel; // Maximum number of rooms per level
    public float widthVariance; // Variability in number of rooms per level

    private Dungeon dungeon;
    private Dictionary<Node<GameObject>, GameObject> nodeObjects = new Dictionary<Node<GameObject>, GameObject>();
    private Dictionary<int, int> levelNodeCounts = new Dictionary<int, int>();

    private void Start()
    {
        dungeon = new Dungeon(roomPrefabs, maxLevels, maxChildrenPerNode, maxWidthPerLevel, widthVariance);

        for (int i = 0; i < maxLevels; i++)
        {
            levelNodeCounts[i] = 0;
        }

        InstantiateNodes();
        DrawConnections();
    }

    private void InstantiateNodes()
    {
        foreach (var level in dungeon.graph.levels)
        {
            foreach (var node in level)
            {
                GameObject nodeObject = Instantiate(node.data, GetNodePosition(node) + transform.position, Quaternion.identity);
                nodeObjects[node] = nodeObject;
            }
        }
    }

    private Vector3 GetNodePosition(Node<GameObject> node)
    {
        // Ensure we have a count for the node's level
        if (!levelNodeCounts.ContainsKey(node.level))
        {
            levelNodeCounts[node.level] = 0;
        }

        // Retrieve the total number of nodes at this level
        float totalNodesAtLevel = dungeon.graph.levels[node.level].Count;
        float spacing = nodeSpacing; // Distance between nodes on the X-axis

        // Calculate the X position to evenly distribute nodes
        float x = (levelNodeCounts[node.level] - (totalNodesAtLevel - 1) / 2f) * spacing;
        levelNodeCounts[node.level]++;

        // Y position can remain as 0 or be used for vertical adjustments if needed
        float y = 0;

        // Z position is based on the level and spacing
        float z = node.level * levelSpacing;

        return new Vector3(x, y, z);
    }


    private void DrawConnections()
    {
        foreach (var level in dungeon.graph.levels)
        {
            foreach (var node in level)
            {
                foreach (var child in node.children)
                {
                    if (nodeObjects.ContainsKey(child))
                    {
                        GameObject nodeObject = nodeObjects[node];
                        GameObject childObject = nodeObjects[child];

                        DrawLine(nodeObject.transform.position, childObject.transform.position);
                    }
                }
            }
        }

        // Draw connections to the end node
        foreach (var node in dungeon.graph.levels[dungeon.graph.levels.Count - 1])
        {
            if (nodeObjects.ContainsKey(node))
            {
                DrawLine(nodeObjects[node].transform.position, nodeObjects[dungeon.graph.endNode].transform.position);
            }
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("ConnectionLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.green };
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
