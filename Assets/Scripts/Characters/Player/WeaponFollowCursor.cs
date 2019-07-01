using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponFollowCursor : NetworkBehaviour
{
    [SerializeField]
    private GameObject WeaponObject = null;
    private SpriteRenderer WeaponSprite;
    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
        WeaponSprite = WeaponObject.GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (!isLocalPlayer || !WeaponObject) return;
        Vector3 mousePos = Input.mousePosition;
        var screenPoint = cameraMain.WorldToScreenPoint(transform.localPosition);

        var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 90)
        {
            angle += 180;
            WeaponSprite.flipX = true;
        }
        else
        {
            WeaponSprite.flipX = false;
        }

        angle += transform.rotation.eulerAngles.z;
        WeaponObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
