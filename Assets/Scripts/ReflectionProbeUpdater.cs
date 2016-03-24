//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class ReflectionProbeUpdater : MonoBehaviour
{
    private Coroutine updater;
    private ReflectionProbe reflectionProbe;

    private void OnEnable()
    {
        reflectionProbe = GetComponent<ReflectionProbe>();
        updater = StartCoroutine(ReflectionProbeUpdate());
    }

    private IEnumerator ReflectionProbeUpdate()
    {
        var waiter = new WaitForEndOfFrame();
        var currentSlice = -1;
        for (;;)
        {
            for (int i = 0; i < 2; i++)
                yield return waiter;

            currentSlice = reflectionProbe.RenderProbe();

            while (!reflectionProbe.IsFinishedRendering(currentSlice))
                yield return waiter;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(updater);
    }
}