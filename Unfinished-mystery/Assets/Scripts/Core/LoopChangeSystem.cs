using System.Collections.Generic;
using UnityEngine;

public class LoopChangeSystem : MonoBehaviour
{
    [System.Serializable]
    public class LoopObject
    {
        public GameObject target;

        [Header("Position per Loop")]
        public Vector3[] positions;

        [Header("Active per Loop")]
        public bool[] activeState;
    }

    [Header("Current Loop")]
    public int currentLoop = 1;
    public int maxLoops = 10;

    [Header("Objects in Scene")]
    public List<LoopObject> loopObjects = new List<LoopObject>();

    private void Start()
    {
        ApplyLoopChanges();
    }

    // Call this when loop resets
    public void NextLoop()
    {
        currentLoop++;

        if (currentLoop > maxLoops)
            currentLoop = maxLoops;

        ApplyLoopChanges();
    }

    public void SetLoop(int loop)
    {
        currentLoop = Mathf.Clamp(loop, 1, maxLoops);
        ApplyLoopChanges();
    }

    private void ApplyLoopChanges()
    {
        int index = currentLoop - 1;

        foreach (var obj in loopObjects)
        {
            if (obj.target == null) continue;

            // POSITION CHANGE
            if (obj.positions != null && obj.positions.Length > index)
            {
                obj.target.transform.position = obj.positions[index];
            }

            // ACTIVE / INACTIVE CHANGE
            if (obj.activeState != null && obj.activeState.Length > index)
            {
                obj.target.SetActive(obj.activeState[index]);
            }
        }
    }
}