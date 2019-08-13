using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    public override bool check(GameUnitEntity entity, BoardManager board)
    {
        return true;
    }

    public override void step(GameUnitEntity entity, BoardManager board)
    {

        foreach (Node node in this.children) {

            if (node.check(entity, board)) {
                node.step(entity, board);
                return;
            }

        }

    }

    
}
