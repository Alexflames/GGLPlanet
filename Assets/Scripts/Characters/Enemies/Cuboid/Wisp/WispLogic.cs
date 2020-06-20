using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WispLogic : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private float Speed = 1;
    public float timeleft = 4;
    public float attackduration = 4;

    [Header("Add your wisp (RoPoArPi)")]
    public GameObject wisp1;

    [Header("Add your pivot (RoPoArPi)")]
    public GameObject pivot1;

    [Header("Add your axis (RoPoArPi)")]
    public Vector3 axis;  

    public void SetSpeed(float speedToSet)
    {
        Speed = speedToSet;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void SetTimeLeft(float timeleftToSet)
    {
        timeleft = timeleftToSet;
    }

    void Update()
    {
        if (timeleft > attackduration * 0.3)
        {
            timeleft -= Time.deltaTime;
            if (timeleft > attackduration * 0.65)
            {
                transform.Translate(direction * Time.deltaTime * Speed);
            }
            else 
            {
                wisp1.transform.position = RotatePointAroundPivot();
            }
            
        }
    }

    public Vector3 RotatePointAroundPivot()
    {
        Vector3  point = wisp1.transform.position;
        Vector3  pivot = pivot1.transform.position;
        Vector3 angles = axis;
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (gameObject == null) return;

        if (coll.name.ToLower() == "heart")
        {
            StatsManager stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(gameObject, 1);
            stats.DealDamage(attack);
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }

}
