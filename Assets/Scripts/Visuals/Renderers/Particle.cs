using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Renderer2D
{

    public void Fire() {
        Activate();
        StartCoroutine(IEDeactivate(currAnimation.frames.Length / currAnimation.frameRate));
    }

    public void ActivateForDuration(float duration) {
        Activate();
        StartCoroutine(IEDeactivate(duration));
    }

    public void Activate() {
        currAnimation.Play();
        render = true;
    }

    public void Deactivate() {
        currAnimation.Stop();
        render = false;
    }

    IEnumerator IEDeactivate(float delay) {
        yield return new WaitForSeconds(delay);
        currAnimation.Stop();
        render = false;
        yield return null;
    }
}
