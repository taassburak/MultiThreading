using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class UnitaskExperimental : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;

    //private void OnEnable()
    //{
    //    if (_cancellationTokenSource != null)
    //    {
    //        _cancellationTokenSource.Dispose();
    //    }

    //    _cancellationTokenSource = new CancellationTokenSource();
    //}

    //private void OnDisable()
    //{
    //    _cancellationTokenSource.Cancel();
    //}

    private void OnDestroy()
    {
        _cancellationTokenSource.Dispose();
    }
    private async UniTaskVoid Start()
    {
        //await TimeOut();
        
        List<UniTask> uniTasks = new List<UniTask>();

        var cts = new CancellationTokenSource();

        _cancellationTokenSource = cts;
        
        //var realLocal = GetClean();
        uniTasks.Add(FirstUniTask());
        uniTasks.Add(SecondUniTask());

        //await FirstUniTask();
        //await SecondUniTask();
        await UniTask.WhenAll(uniTasks);
        Debug.Log("all tasks are completed");

        _cancellationTokenSource.Dispose();
    }

    async UniTask FirstUniTask()
    {
        Debug.Log("FirstTask");
        await UniTask.Delay(1000);
        Debug.Log("FirstTaskCompleted");
        await UniTask.Delay(5000);
    }

    async UniTask SecondUniTask()
    {
        Debug.Log("SecondTask");
        await UniTask.Delay(1000);
    }

    private async UniTaskVoid Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            await TimeOut();
        }
    }

    async UniTask TimeOut()
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfterSlim(TimeSpan.FromSeconds(5)); // 5sec timeout.

        try
        {
            await UnityWebRequest.Get("http://google.com").SendWebRequest().WithCancellation(cts.Token);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == cts.Token)
            {
                Debug.Log("Timeout");
                await UniTask.Yield();
            }
        }
    }
}
