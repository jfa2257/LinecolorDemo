using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum outcome
{
    undecided, victory, defeat
}
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [Header("Regular Solution")]
    public CameraFollow cFollow;
    public bool isDestroyed = false;
    public bool isGoalReached = false;
    public outcome outcome = outcome.undecided;
    public TrailRenderer trail;
    public Animator chest;
    //public Vector3 chestInitPos;
    //public Quaternion chestInitRot;
    public bool collided = false;
    public float cachecSpeed;
    public bool isAnimated;
    public Animator boxAni;
    public int coinsCollected;
    public bool autoStart = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //chestInitPos = chest.transform.position;
        //chestInitRot = chest.transform.rotation;
        boxAni.speed = autoStart ? cachecSpeed : 0; 
    }

    private void Update()
    {
        if (!collided)
        {
            UIManager.instance.progressBar.fillAmount = boxAni.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        if (Input.GetMouseButtonDown(0) && !collided)
        {
            boxAni.speed = boxAni.speed == cachecSpeed ? 0 : cachecSpeed;
        }
    }

    public void RestartGameState()
    {
        Debug.Log("RESTARTING GAME");
        isDestroyed = false;
        isGoalReached = false;
        trail.Clear();
        collided = false;
        trail.enabled = false;
        UIManager.instance.RestartGameUI();
        chest.SetInteger("openclose", 0);
        //chest.transform.position = chestInitPos;
        //chest.transform.rotation = chestInitRot;
        ResetPoles();
        outcome = outcome.undecided;
        cFollow.RestartFollow();
    }

    public void ResetPoles()
    {
        PoleAi[] poles = FindObjectsOfType<PoleAi>();
        foreach (PoleAi ai in poles)
        {
            ai.GetComponent<Rigidbody>().isKinematic = false;
            if (ai.gameObject.tag != "playerGoal")
            {
                ai.transform.parent.GetComponent<RotatePole>().collisioned = false;
            }
        }
    }
}
