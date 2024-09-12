using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public Graph<GameObject> graph;

    public Dungeon(List<GameObject> roomPrefabs, int maxLevels, int maxChildrenPerNode, int maxWidthPerLevel, float widthVariance)
    {
        graph = new Graph<GameObject>(roomPrefabs[0], roomPrefabs[roomPrefabs.Count - 1], maxLevels);
        GenerateGraph(roomPrefabs, maxLevels, maxChildrenPerNode, maxWidthPerLevel, widthVariance);
    }

    private void GenerateGraph(List<GameObject> roomPrefabs, int maxLevels, int maxChildrenPerNode, int maxWidthPerLevel, float widthVariance)
    {
        Dictionary<int, List<Node<GameObject>>> nodesAtLevels = new Dictionary<int, List<Node<GameObject>>>();

        for (int i = 0; i < maxLevels; i++)
        {
            nodesAtLevels[i] = new List<Node<GameObject>>();
        }

        nodesAtLevels[0].Add(graph.startNode);

        for (int i = 0; i < maxLevels - 1; i++)
        {
            int currentLevelNodeCount = 0;
            int levelWidth = Mathf.RoundToInt(maxWidthPerLevel + Random.Range(-widthVariance, widthVariance));
            levelWidth = Mathf.Clamp(levelWidth, 1, maxWidthPerLevel); // Ensure at least 1 and at most maxWidthPerLevel nodes

            foreach (var node in nodesAtLevels[i])
            {
                int childrenCount = Random.Range(1, maxChildrenPerNode + 1);
                bool hasChild = false;

                for (int j = 0; j < childrenCount; j++)
                {
                    if (currentLevelNodeCount >= levelWidth)
                        break;

                    GameObject childPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
                    Node<GameObject> childNode = graph.AddNodeToLevel(childPrefab, i + 1);

                    if (Random.value < 0.3f && i > 0 && nodesAtLevels[i].Count > 1)
                    {
                        Node<GameObject> randomParent = nodesAtLevels[i][Random.Range(0, nodesAtLevels[i].Count)];
                        graph.ConnectNodes(randomParent, childNode);
                    }

                    graph.ConnectNodes(node, childNode);

                    if (!nodesAtLevels.ContainsKey(i + 1))
                    {
                        nodesAtLevels[i + 1] = new List<Node<GameObject>>();
                    }

                    nodesAtLevels[i + 1].Add(childNode);
                    currentLevelNodeCount++;
                    hasChild = true;
                }

                // Ensure each node has at least one child
                if (!hasChild && nodesAtLevels[i + 1].Count > 0)
                {
                    Node<GameObject> fallbackChild = nodesAtLevels[i + 1][Random.Range(0, nodesAtLevels[i + 1].Count)];
                    graph.ConnectNodes(node, fallbackChild);
                }
            }
        }

        // Ensure the last level nodes are connected to the end node
        foreach (var node in nodesAtLevels[maxLevels - 1])
        {
            graph.ConnectNodes(node, graph.endNode);
        }
    }
}
