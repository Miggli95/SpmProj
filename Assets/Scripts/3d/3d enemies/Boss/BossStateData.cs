using UnityEngine;

public struct BossStateData
{
    public Vector3 Pos;

    public BossState NewState;

    public int DestPoint;

    public bool RunNewStateSameUpdate;

    public BossStateData(Vector3 pos, BossState newState, bool runNewStateSameUpdate, int destPoint)
    {
        Pos = pos;
        NewState = newState;
        DestPoint = destPoint;
        RunNewStateSameUpdate = runNewStateSameUpdate;
    }
}