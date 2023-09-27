using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MultiThreadManager : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private Vector3 _objectPosition;
    Thread thread1;
    Thread thread2;
    void Start()
    {
        //Thread mainThread = Thread.CurrentThread;

        thread1 = new Thread(MoveObject);
        thread2 = new Thread(CreateObject);

        thread1.Start();
        thread2.Start();

        //CreateObject();


        //CountDown();
        //CountUp();
    }

    private void Update()
    {
        _object.transform.position = _objectPosition;
    }

    private void OnDestroy()
    {
        thread1.Abort();
        thread2.Abort();
    }

    private void CreateObject()
    {
        var obj = new GameObject();
        obj.name = "new object";
    }

    private void MoveObject()
    {
        for (int i = 0; i < 10000; i++)
        {
            _objectPosition = new Vector3(i, 0, 0);
            Debug.Log($"Object Count: {i}");
            Thread.Sleep(200);
        }
    }

    private static void CountDown()
    {
        for (int i = 10; i >= 0; i--)
        {
            Debug.Log($"TIMER 1# : {i}");
            Thread.Sleep(500);
        }
    }

    private static void CountUp()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"TIMER 2# : {i}");
            Thread.Sleep(500);
        }
    }
}
