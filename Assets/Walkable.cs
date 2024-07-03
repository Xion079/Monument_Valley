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

    [Header("Offsets")] //Gizmo��Ÿ���µ� �� ���� �����־�� ���̴ϱ� �װ� ����Ϸ��� ���� �Լ���
    public float walkPointOffset = .5f;
    public float stairOffset = .4f;

    public Vector3 GetWalkPoint()
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
        //������� �ƴ��� ������ �̵��ϴ� ���� ����ִ� �Լ��� ���������� ���߾ӿ��� 0.5����
        //����̸� 0.1���� ��°��� ����̶� �߾ӿ��� 0.1���θ� �����
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
            //���� gizmo��ġ�� Ÿ���� gizmo��ġ�� �̾��ִ� ���ε� Ÿ���� getwalkpoint�� �����;ߵǴϱ�
            //p.target.getComponent<Walkable>�� ����ؼ� ������
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
}