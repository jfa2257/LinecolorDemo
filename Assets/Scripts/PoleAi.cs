using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAi : MonoBehaviour
{
    private void PlayParticles()
    {
        ParticleSystem[] particlesOnScene = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particlesOnScene) {
            ps.Play();
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "playerBox") {
            if (gameObject.tag == "playerGoal") {
                GameManager.instance.isGoalReached = true;
            } else {
                GetComponent<Rigidbody>().isKinematic = true;
                GameManager.instance.isDestroyed = true;
                GameManager.instance.collided = true;
                GameManager.instance.boxAni.speed = 0;
                transform.parent.GetComponent<RotatePole>().collisioned = true;
                UIManager.instance.dumpValue = (Mathf.RoundToInt(GameManager.instance.boxAni.GetCurrentAnimatorStateInfo(0).normalizedTime * 100)).ToString() + "%";
                UIManager.instance.ShowHideEndGameUI(true);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "playerBox") {
            if (gameObject.tag == "playerGoal") {
                

                GameManager.instance.isGoalReached = true;
                GameManager.instance.outcome = outcome.victory;
                GameManager.instance.chest.SetInteger("openclose", 1);
                UIManager.instance.ShowHideEndGameUI(true);
                PlayParticles();
            }
        }
    }
}
