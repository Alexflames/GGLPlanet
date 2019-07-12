using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WispLogic : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private float Speed = 1;
    private float OrbitSpeed = 10;
    public float timeleft = 5;
    public float attackduration = 5;

    [Header("Add your wisp (RoPoArPi)")]
    public GameObject wisp1;

    [Header("Add your pivot (RoPoArPi)")]
    public GameObject pivot1;

    [Header("Add your axis (RoPoArPi)")]
    public Vector3 axis;  

    [Header("Add your cube")]
    public GameObject obj; //to get the position in worldspace to which this gameObject will rotate around.

    [Header("The axis by which it will rotate around")]
    public Vector3 axisrotate;//by which axis it will rotate. x,y or z.

    [Header("Angle covered per update")]
    public float angle; //or the speed of rotation.

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

    public void SetOrbitSpeed(float OrbitspeedToSet)
    {
        OrbitSpeed = OrbitspeedToSet;
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
                orbita();
            }
            //Invoke("orbita", 1);
            Invoke("RotatePointAroundPivot", 1);
        }
    }

    void orbita()
    {
        transform.RotateAround(obj.transform.position, axisrotate, angle);
    }

    void OrbitArround()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, OrbitSpeed * Time.deltaTime);
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
