
using UnityEngine;

[System.Serializable]
public class TargetScanner
{
    public float heightOffset = 0.0f;
    public float detectionRadius = 10;
    [Range(0.0f, 360.0f)]
    public float detectionAngle = 270;
    // public float maxHeightDifference = 1.0f;
    public float attackRange;

    public GameObject DetectPlayer(Transform detector, GameObject _player)
    {
        Vector3 eyePos = detector.position + Vector3.up * heightOffset;
        Vector3 toPlayer = _player.transform.position - eyePos;
        Vector3 toPlayerTop = _player.transform.position + Vector3.up * 1.5f - eyePos;

        Vector3 toPlayerFlat = toPlayer;
        toPlayerFlat.y = 0;

        if (toPlayerFlat.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            if (Vector3.Dot(toPlayerFlat.normalized, detector.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {

                Debug.DrawRay(eyePos, toPlayer, Color.blue);
                Debug.DrawRay(eyePos, toPlayerTop, Color.blue);

                return _player;
            }
        }
        return null;
    }

    public M2_PlayerControllerV2 DetectPlayer(Transform detector, bool useHeightDifference = true)
    {
        if (M2_PlayerControllerV2.instance == null) return null;

        Vector3 eyePos = detector.position + Vector3.up * heightOffset;
        Vector3 toPlayer = M2_PlayerControllerV2.instance.transform.position - eyePos;
        Vector3 toPlayerTop = M2_PlayerControllerV2.instance.transform.position + Vector3.up * 1.5f - eyePos;

        Vector3 toPlayerFlat = toPlayer;
        toPlayerFlat.y = 0;

        if (toPlayerFlat.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            if (Vector3.Dot(toPlayerFlat.normalized, detector.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {

                Debug.DrawRay(eyePos, toPlayer, Color.blue);
                Debug.DrawRay(eyePos, toPlayerTop, Color.blue);

                return M2_PlayerControllerV2.instance;
            }
        }

        return null;
    }


#if UNITY_EDITOR

    public void EditorGizmo(Transform transform)
    {
        Color c = new Color(0, 0, 0.7f, 0.4f);

        UnityEditor.Handles.color = c;
        Vector3 rotatedForward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, detectionAngle, detectionRadius);

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * heightOffset, 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

#endif
}


