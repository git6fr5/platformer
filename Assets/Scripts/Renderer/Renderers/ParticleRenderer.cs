using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRenderer : Renderer2D
{

    public void Fire() {
        Activate();
        StartCoroutine(IEDeactivate(currAnimation.frames.Length / currAnimation.frameRate));
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
