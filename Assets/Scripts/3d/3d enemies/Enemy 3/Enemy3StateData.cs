using UnityEngine;

public struct Enemy3StateData
{
    public Vector3 Pos;

    public EnemyState3 NewState;

    public int DestPoint;

    public bool RunNewStateSameUpdate;

    public Enemy3StateData(Vector3 pos, EnemyState3 newState, bool runNewStateSameUpdate, int destPoint)
    {
        Pos = pos;
        NewState = newState;
        DestPoint = destPoint;
        RunNewStateSameUpdate = runNewStateSameUpdate;
    }
}