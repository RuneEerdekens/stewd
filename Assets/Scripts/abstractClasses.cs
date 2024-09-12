using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AugmentBase : ScriptableObject
{
    [SerializeField] private string id;

    public string ID => id;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
        }
    }
}

public abstract class DashAugmentBase : AugmentBase
{
    public abstract void StartDashEffect(Vector3 pos);
}

public abstract class PrimaryAttackAugmentBase : AugmentBase
{
    public abstract void StartPrimaryEffect(PlayerImpact info);
}

public abstract class SecondaryAttackAugmentBase : AugmentBase
{
    public abstract void StartSecondaryAttack(PlayerImpact info);
}


// slots

public abstract class ISlot : MonoBehaviour, IDropHandler
{
    public abstract void OnDrop(PointerEventData eventData);
    public abstract void ApplyAugment(Augment aug);
}

public struct PlayerImpact
{
    public Vector3 impactPos { get; }
    public GameObject hitObj { get; }

    public PlayerImpact(Vector3 impactPos, GameObject hitObj)
    {
        this.impactPos = impactPos;
        this.hitObj = hitObj;
    }
}

public struct Node<T>
{
    public T data;  // The actual data of the node (e.g., a room, value, etc.)
    public int level;  // The level of the node in the graph
    public List<Node<T>> children;  // List of child nodes (branches)
    public List<Node<T>> parents;   // List of parent nodes (for shared nodes)

    public Node(T data, int level)
    {
        this.data = data;
        this.level = level;
        this.children = new List<Node<T>>();
        this.parents = new List<Node<T>>();
    }

    // Add a child connection
    public void AddChild(Node<T> child)
    {
        if (!children.Contains(child))
        {
            children.Add(child);
            child.parents.Add(this); // Add this node as a parent of the child
        }
    }
}

public class Graph<T>
{
    public Node<T> startNode;  // The start node of the graph
    public Node<T> endNode;    // The end node of the graph
    public List<List<Node<T>>> levels;  // List of levels, where each level contains nodes

    public Graph(T startData, T endData, int numLevels)
    {
        // Initialize the levels
        levels = new List<List<Node<T>>>();

        // Create start node at level 0
        startNode = new Node<T>(startData, 0);
        levels.Add(new List<Node<T>> { startNode });

        // Initialize levels up to numLevels - 1 (since level 0 is the start)
        for (int i = 1; i < numLevels; i++)
        {
            levels.Add(new List<Node<T>>());
        }

        // Create end node at the last level
        endNode = new Node<T>(endData, numLevels);
        levels.Add(new List<Node<T>> { endNode });
    }

    // Add a node to a specific level
    public Node<T> AddNodeToLevel(T data, int level)
    {
        if (level < 0 || level >= levels.Count)
            throw new System.ArgumentException("Invalid level");

        Node<T> newNode = new Node<T>(data, level);
        levels[level].Add(newNode);
        return newNode;
    }

    // Connect two nodes, parent to child
    public void ConnectNodes(Node<T> parent, Node<T> child)
    {
        if (child.level != parent.level + 1)
            throw new System.ArgumentException("Child must be exactly one level below the parent");

        parent.AddChild(child);
    }

    // Print the graph (for debugging purposes)
    public void PrintGraph()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            Debug.Log($"Level {i}: ");
            foreach (var node in levels[i])
            {
                Debug.Log($"Node: {node.data}, Children: {node.children.Count}");
            }
        }
    }
}




