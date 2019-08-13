using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeuristicInfo : MonoBehaviour
{
    public GameUnitEntity[] units;
    public CaptureEntity srcCaptureEntity; // The Ai's entity, friendly
    public CaptureEntity targetCaptureEntity; // The Ai's target, hostile
}
