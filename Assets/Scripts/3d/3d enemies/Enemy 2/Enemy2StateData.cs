using UnityEngine;

public struct Enemy2StateData
{
    public Vector3 Pos;

    public EnemyState2 NewState;

    public int DestPoint;

    public bool RunNewStateSameUpdate;

    public Enemy2StateData(Vector3 pos, EnemyState2 newState, bool runNewStateSameUpdate, int destPoint)
    {
        Pos = pos;
        NewState = newState;
        DestPoint = destPoint;
        RunNewStateSameUpdate = runNewStateSameUpdate;
    }
}