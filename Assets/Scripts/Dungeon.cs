using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon
{
    int maxLevels;
    int maxChildCount;

    DungeonNode rootNode;

    List<GameObject> roomPrefabs;

    List<DungeonNode> dungeonTree = new List<DungeonNode>();

    public Dungeon(int maxLevels, int maxChildCount, List<GameObject> rooms)
    {
        this.maxLevels = maxLevels;
        this.maxChildCount = maxChildCount;
        this.roomPrefabs = rooms;

        // Generate the root node and start the tree generation
        this.rootNode = GenerateRootNode(0);
        dungeonTree.Add(rootNode);

        // Start generating the tree
        GenerateTree(rootNode, 1);  // Start from level 1, root is level 0
        dungeonTree = dungeonTree.OrderBy(node => node.GetCurrLevel()).ToList();
    }

    private DungeonNode GenerateRootNode(int level)
    {
        Node<GameObject> currNode = new Node<GameObject>(null, level, true);
        return new DungeonNode(currNode, GenerateChildNotes(level + 1));
    }

    private List<Node<GameObject>> GenerateChildNotes(int currLevel)
    {
        // Stop generating further child nodes if we reached maxLevels
        if (currLevel >= maxLevels) return new List<Node<GameObject>>();

        List<Node<GameObject>> childNodes = new List<Node<GameObject>>();

        // Always generate at least one child
        int childNodesCount = UnityEngine.Random.Range(1, maxChildCount + 1);

        for (int i = 0; i < childNodesCount; i++)
        {
            Node<GameObject> newNode = new Node<GameObject>(null, currLevel);
            childNodes.Add(newNode);
        }

        return childNodes;
    }

    // Recursive function to generate tree down to maxLevels
    private void GenerateTree(DungeonNode parentNode, int currLevel)
    {
        // Stop if we reached the max level
        if (currLevel >= maxLevels) return;

        foreach (Node<GameObject> childNode in parentNode.childNodes)
        {
            // Create a new DungeonNode for the child
            DungeonNode newDungeonNode = new DungeonNode(childNode, GenerateChildNotes(currLevel + 1));
            dungeonTree.Add(newDungeonNode);

            Debug.Log($"new dungeonNode L:{childNode.level}");

            // Recursively generate the tree for each child node
            GenerateTree(newDungeonNode, currLevel + 1);
        }
    }

    public void PrintTree()
    {
        foreach (DungeonNode node in dungeonTree)
        {
            Debug.Log($"Node Level: {node.GetCurrLevel()}, ROOT?: {node.dNode.isRoot}");
        }
    }
}
