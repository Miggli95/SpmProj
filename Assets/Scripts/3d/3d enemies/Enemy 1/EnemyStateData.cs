using UnityEngine;

public struct EnemyStateData
{
    public Vector3 Pos;

    public EnemyState NewState;

    public int DestPoint;

    public bool RunNewStateSameUpdate;

    public EnemyStateData(Vector3 pos, EnemyState newState, bool runNewStateSameUpdate, int destPoint)
    {
        Pos = pos;
        NewState = newState;
        DestPoint = destPoint;
        RunNewStateSameUpdate = runNewStateSameUpdate;
    }
}