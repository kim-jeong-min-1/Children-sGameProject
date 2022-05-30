using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public Sprite BackGround;
    public Sprite BlockObj_1;
    public Sprite BlockObj_2;
    public Sprite BlockObj_3;
    public Sprite BlocHallkObj_1;
    public Sprite BlocHallkObj_2;
    public Sprite BlockHallObj_3;
    public int StarCount = 0;
}

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Object/StageSO")]
public class StageSO : ScriptableObject
{
    public StageData[] stageDatas;
}
