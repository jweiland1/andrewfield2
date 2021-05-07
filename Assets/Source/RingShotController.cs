using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingShotController : ScoreTrigger
{
    Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public void Reset()
    {
        anim.SetTrigger("Reset");
        isActivated = true;
    }

    protected override void TriggerScoreEvent(int value, Team team)
    {
        anim.SetTrigger("Deactivate");
        isActivated = false;
        base.TriggerScoreEvent(value, team);
    }
}
