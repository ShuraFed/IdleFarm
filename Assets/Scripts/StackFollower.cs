using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFollower : MonoBehaviour
{
    public float followDelay = 0.05f; 
    public List<Transform> children; 
    public Transform followTarget;
    [SerializeField]
    private float yOffset;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (i == 0)
            {
                children[i].DOMove(GetPositionWithOffset(followTarget), followDelay);
            }
            else
            {
                children[i].DOMove(GetPositionWithOffset(children[i - 1].transform), followDelay);
            }
        }
    }

    private Vector3 GetPositionWithOffset(Transform target)
    {
        return new Vector3(target.position.x,target.position.y+yOffset,target.position.z);
    }
}
