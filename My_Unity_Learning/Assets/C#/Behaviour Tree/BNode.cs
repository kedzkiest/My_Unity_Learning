using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BNode
{
    public enum Status
    {
        SUCCESS,
        RUNNING,
        FAILURE
    };

    public Status status;
    public List<BNode> children = new List<BNode>();
    public int currentChild = 0;
    public string name;

    public BNode() { }

    public BNode(string n)
    {
        name = n;
    }

    public virtual Status Process()
    {
        return children[currentChild].Process();
    }

    public void AddChild(BNode n)
    {
        children.Add(n);
    }
}
