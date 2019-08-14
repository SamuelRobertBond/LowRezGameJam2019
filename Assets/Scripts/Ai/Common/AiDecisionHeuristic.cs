
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiDecisionManager : MonoBehaviour
{

    public int decisionLimit = 3;
    public const int VIEW_RANGE = 6;

    protected AiController aiController;

    protected virtual void Start()
    {
        this.aiController = this.GetComponent<AiController>();
    }

    public AiDecision[] getDecisions() {

        AiHeuristicInfo[] situations = this.getAllSituations();
        Queue<MoveInfo> scoredMoves = new Queue<MoveInfo>();

        // Rank All Situations
        foreach (AiHeuristicInfo move in situations) {

            MoveInfo info = new MoveInfo();
            info.move = move;
            info.score = getScore(move);

            scoredMoves.Enqueue(info);
        }

        // Sort All Moves
        List<MoveInfo> movesInfo = new List<MoveInfo>(scoredMoves.ToArray());
        movesInfo.Sort(new ScoreComparer());

        Queue<AiDecision> decisions = new Queue<AiDecision>();

        foreach (MoveInfo moveInfo in movesInfo) {

            if (decisions.Count >= decisionLimit) { break; } // Limit on decisions

            decisions.Enqueue(makeDecision(moveInfo.move));
        }

        return decisions.ToArray();
    }

    protected AiHeuristicInfo[] getAllSituations() {

        Queue<AiHeuristicInfo> possibleMovesInfo = new Queue<AiHeuristicInfo>();

        foreach (CaptureEntity c in this.aiController.capturedEntities) {

            // Check Each Direction
            foreach (Direction d in Enum.GetValues(typeof(Direction))) {

                // Generate Raycast
                Vector2 direction = EntityUtils.getDirectionVector(d) * VIEW_RANGE;
                direction *= BoardManager.TILE_SCALE * VIEW_RANGE;
                Vector2 end = c.rb2D.position + direction;

                // Preform Raycast
                c.boxCollider.enabled = false;
                RaycastHit2D[] hits = Physics2D.LinecastAll(c.rb2D.position, end, LayerMask.GetMask("Blocking")); //Physics2D.Linecast
                c.boxCollider.enabled = true;


                if (hits == null || hits.Length == 0) { continue; } // Continue if nothing was hit

                AiHeuristicInfo moveInfo = new AiHeuristicInfo(c, d);

                // Direction Vision Check
                foreach (RaycastHit2D hit in hits) {

                    CaptureEntity captureEntity = hit.rigidbody.GetComponent<CaptureEntity>();
                    GameUnitEntity gameUnitEntity = hit.collider.GetComponent<GameUnitEntity>();

                    // Unit
                    if (gameUnitEntity != null) {

                        if (gameUnitEntity.owner == c.owner)
                        {
                            moveInfo.srcUnits.AddLast(gameUnitEntity);
                        }
                        else {
                            moveInfo.enemyUnits.AddLast(gameUnitEntity);
                        }

                    } else if (captureEntity != null) {
                        // Enemy Capture Points
                        moveInfo.target = captureEntity;
                    }

                }

                if (moveInfo.target == null) { continue; } // If a target was not found, don't move

                possibleMovesInfo.Enqueue(moveInfo);
            }
            
        }

        return possibleMovesInfo.ToArray();
    }

    protected abstract int getScore(AiHeuristicInfo info);

    protected abstract AiDecision makeDecision(AiHeuristicInfo info);

    // Used during move scoring
    class MoveInfo
    {

        public int score;
        public AiHeuristicInfo move;

    }

    class ScoreComparer : IComparer<MoveInfo>
    {
        public int Compare(MoveInfo a, MoveInfo b)
        {
            return a.score.CompareTo(b.score);
        }

    }

}
