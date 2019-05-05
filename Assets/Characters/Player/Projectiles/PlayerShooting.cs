using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public GameObject projectile;

    public float CoolDown = 0.33f;
    float m_CoolDownTL;

    void Start()
    {
        m_CoolDownTL = CoolDown;
    }

    [Command]
    void CmdShoot(Vector3 mousePos, Vector3 screenPoint)
    {
        if (m_CoolDownTL < CoolDown * 3 / 4)
        {
            m_CoolDownTL = CoolDown;
           
            var bullet = Instantiate(projectile, transform.position, transform.rotation);
            
            var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.transform.Translate(Vector2.right * 0.5f);

            NetworkServer.Spawn(bullet);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        m_CoolDownTL = Mathf.Max(m_CoolDownTL - Time.deltaTime, 0);
        if (!isLocalPlayer) return;

        if (Input.GetButton("Fire1") && m_CoolDownTL == 0)
        {
            Vector3 mousePos = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            CmdShoot(mousePos, screenPoint);

            m_CoolDownTL = CoolDown;
        }
    }
}
