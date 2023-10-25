using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    // private Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();

    public bool TweenExists(Transform target) {
        foreach (Tween tween in activeTweens) {
            if (tween.Target == target) {
                return true;
            }
        }
        return false;
    }

    // Changing AddTween to bool so InputManager can perform the add if Tween does not exist
    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration) {
        if (!TweenExists(targetObject)) {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            // addTween(object, startpos, endpos, start time, duration)
            return true;
        }
        return false;
    }

    private float EaseInCubic(float f) { return f * f * f; }

    void Start() {}

    void Update() {
        for (int i = 0; i < activeTweens.Count; i++) {
            Tween tween = activeTweens[i];
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);

            if (distance > 0.1f) {
                // essentialy Current time - Time when tween added
                float elapsedTime = Time.time - tween.StartTime; 
                float interpFraction = elapsedTime / tween.Duration; // tween Duration either 1.5f or 0.5f for our exercise
                // interpFraction = EaseInCubic(interpFraction);
                Vector3 newPos = Vector3.Lerp(tween.StartPos, tween.EndPos, interpFraction);
                tween.Target.position = newPos;
            // Essentially, towards the end of the tween, if the distance is < 0.1f,
            // which basically is at its endPos, sets, the position to endPos thus ending the tween
            // then removes it from the list
            } else {
                tween.Target.position = tween.EndPos;
                activeTweens.RemoveAt(i);
            }
        }
    }
}
