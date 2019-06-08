using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerShooting : NetworkBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float CoolDown = 0.15f;
    float m_CoolDownTL;

    // Weapon reload properties
    public int weaponMagazine = 6;
    private int ammoLeft;
    private Slider ammoSlider;
    public float reloadTime = 1.5f;
    private IEnumerator reloadCoroutine;

    void Start()
    {
        m_CoolDownTL = CoolDown;

        var slider = GameObject.Find("AmmoSlider");
        ammoSlider = slider == null ? null : slider.GetComponent<Slider>();
        ammoLeft = weaponMagazine;
    }

    [Command]
    void CmdShoot(Vector3 mousePos, Vector3 screenPoint, Vector3 selfEulerAngles)
    {
        if (m_CoolDownTL < CoolDown * 3 / 4)
        {
            m_CoolDownTL = CoolDown;
           
            var bullet = Instantiate(projectile, transform.position, new Quaternion());
            
            var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            angle += selfEulerAngles.z;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.transform.Translate(Vector2.right * 0.5f);

            NetworkServer.Spawn(bullet);
        }
    }

    bool UseAmmo(int ammoCount)
    {
        bool toReturn = (ammoLeft -= ammoCount) >= 0;
        if (ammoSlider != null)
        {
            ammoSlider.value = ammoLeft;
        }
        return toReturn;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime - 0.1f);
        ammoLeft = weaponMagazine;
        if (ammoSlider != null)
        {
            ammoSlider.value = weaponMagazine;
        }
        yield return null;
    }
    
    // Update is called once per frame
    void Update()
    {
        m_CoolDownTL = Mathf.Max(m_CoolDownTL - Time.deltaTime, 0);
        if (!isLocalPlayer) return;

        if (Input.GetButton("Fire1") && m_CoolDownTL == 0)
        {
            if (UseAmmo(1))
            {
                Vector3 mousePos = Input.mousePosition;
                var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
                CmdShoot(mousePos, screenPoint, transform.rotation.eulerAngles);

                m_CoolDownTL = CoolDown;
            }
            else
            {
                m_CoolDownTL = reloadTime;
                reloadCoroutine = Reload();
                StartCoroutine(reloadCoroutine);
                Reload();
            }
        }
    }
}
