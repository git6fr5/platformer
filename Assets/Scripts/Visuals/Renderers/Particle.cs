using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Renderer2D
{

    public Animation2D[] animationSeeds;
    Coroutine deactivate = null;

    public void Fire() {
        if (deactivate != null) { return; }
        RandomSeed();
        Activate();
        deactivate = StartCoroutine(IEDeactivate(currAnimation.frames.Length / currAnimation.frameRate));
    }

    public void FireAdditively(Vector3 position) {
        Particle particle = Instantiate(gameObject).GetComponent<Particle>();
        particle.transform.position = position;
        particle.Fire();
        StartCoroutine(IEDestroy(2f, particle));
    }

    public void ActivateForDuration(float duration) {
        Activate();
        StartCoroutine(IEDeactivate(duration));
    }

    public void RandomSeed() {
        if (animationSeeds.Length == 0) { return; }
        currAnimation = animationSeeds[Random.Range(0, animationSeeds.Length)];
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
        Deactivate();
        deactivate = null;
        yield return null;
    }

    IEnumerator IEDestroy(float delay, Particle particle) {
        yield return new WaitForSeconds(delay);
        particle.gameObject.SetActive(false);
        Destroy(particle.gameObject);
        yield return null;
    }
}
