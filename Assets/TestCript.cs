using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCript : MonoBehaviour
{
    private Coroutine _routine;

    private void Start()
    {
        _routine = StartCoroutine(SomeCoroutine());
    }

    [ContextMenu("Kill routine")]
    public void KillCorutine()
    {
        StopCoroutine(_routine);
    }

    private IEnumerator SomeCoroutine()
    {
        var some = "34";

        Debug.Log("Start corr");
        some += 42;
        while(enabled)
        {
            Debug.Log("w8 for another corrutine");
            yield return AnotherCoroutine();
            Debug.Log("done");
            yield return new WaitForSeconds(1f);
        }

    }

    private IEnumerator AnotherCoroutine()
    {
        Debug.Log("Start Another corrutine");
        yield return new WaitForSeconds(2f);
    }
}
