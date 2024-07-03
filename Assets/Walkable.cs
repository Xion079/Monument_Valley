using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    public List<WalkPath> possiblePaths = new List<WalkPath>();

    [Space]

    public Transform previousBlock;

    [Space]

    [Header("Booleans")]
    public bool isStair = false;
    public bool movingGround = false;
    public bool isButton;
    public bool dontRotate;

    [Space]

    [Header("Offsets")] //Gizmo나타내는데 좀 위로 나와있어야 보이니까 그거 계산하려고 만든 함수임
    public float walkPointOffset = .5f;
    public float stairOffset = .4f;

    public Vector3 GetWalkPoint()
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
        //계단인지 아닌지 따져서 이동하는 점을 찍어주는 함수임 평지에서는 정중앙에서 0.5위로
        //계단이면 0.1위로 찍는거임 계단이라 중앙에서 0.1위로만 가면됨
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);

        if (possiblePaths == null)
            return;

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
            //현재 gizmo위치랑 타겟의 gizmo위치를 이어주는 선인데 타겟의 getwalkpoint를 가져와야되니까
            //p.target.getComponent<Walkable>를 사용해서 가져옴
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
}