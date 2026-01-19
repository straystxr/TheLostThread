using System;
using UnityEngine;
using UnityEngine.Splines;

namespace Scenes.Scripts
{
    public class SplineFollow : MonoBehaviour
    {
        public SplineContainer spline;
        public float time = 10f;
        private float currentTime = 0f;

        private void Update()
        {
            currentTime += Time.deltaTime;
            var d = currentTime / time;

            var pos = spline.EvaluatePosition(d);
            transform.position = pos;
        }
    }
}