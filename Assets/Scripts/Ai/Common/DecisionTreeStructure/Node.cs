using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{

    protected LinkedList<Node> children = new LinkedList<Node>();

    public abstract bool check(GameUnitEntity entity, BoardManager board);

    public abstract void step(GameUnitEntity entity, BoardManager board);

    public void addChild(Node node) {
        children.AddLast(node);
    }

}
