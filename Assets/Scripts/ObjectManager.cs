using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private ObjectBehaviour[] _shapes;

    public async void BeginTest()
    {
        var taskList = new List<Task>();
        for (var i = 0; i < _shapes.Length; i++)
        {
            var task = _shapes[i].RotateForSeconds(1 + 1 * i);
            taskList.Add(task);
        }

        await Task.WhenAll(taskList);

        Debug.LogAssertion("ALL TASKS ARE COMPLETED");

        var rnd = await GetRandomDelay();    

        Debug.Log(rnd);
    }

    private async Task<int> GetRandomDelay()
    {
        var randomNumber = Random.Range(3000, 10000);

        await Task.Delay(randomNumber);

        return randomNumber;
    }
}
