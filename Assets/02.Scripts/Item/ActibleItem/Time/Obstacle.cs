using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FragData { public Vector3 pos; public Quaternion rot; public Vector3 scale; }

public class Obstacle : MonoBehaviour
{
    public GameObject beforFrefab;
    public GameObject afterFrefab;
    public GameObject obstacleobj;

    ObstaclePos beforPos;
    ObstaclePos afterPos;

    private void Start()
    {
        beforPos = beforFrefab.GetComponent<ObstaclePos>();
        afterPos = afterFrefab.GetComponent <ObstaclePos>();
    }
    public void Getup()
    {
        obstacleobj.GetComponent<BreakObstacle>().Move(beforPos.originalData);
        obstacleobj.GetComponent<BreakObstacle>().onRestoreComplete += wakeUp;
    }

    public void wakeUp()
    {        
        beforFrefab.SetActive(true);
        afterFrefab.SetActive(false);
    }
    public void Breaks()
    {
        obstacleobj.GetComponent<BreakObstacle>().Move(afterPos.originalData);
        obstacleobj.GetComponent<BreakObstacle>().onRestoreComplete += Break;
    }
    public void Break()
    {
        beforFrefab.SetActive(false);
        afterFrefab.SetActive(true);    
    }
}
