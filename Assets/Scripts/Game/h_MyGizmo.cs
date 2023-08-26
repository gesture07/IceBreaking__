using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_MyGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    public Color gizmoColor = Color.red;
    public float gizmoRadius = 1.0f;

    // Called by Unity when drawing Gizmos in the scene view
    //런타임 시 실행되는 기즈모를 그릴 수 있는 함수이다.
    //
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoRadius); //구 형태의 기즈모
    }
}
