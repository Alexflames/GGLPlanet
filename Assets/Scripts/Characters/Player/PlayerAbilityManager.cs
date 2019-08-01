using UnityEngine;
using Mirror;

public class PlayerAbilityManager : NetworkBehaviour
{
    private PlayerAbility ability;
    
    private PlayerStatsManager statsManager;

    private void Start()
    {
        statsManager = GetComponent<PlayerStatsManager>();
        ability = new MedkitAbility();
        ability.Init();
        ability.PassUser(statsManager);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ability.UserPress();
        }
    }

    private void FixedUpdate()
    {
        ability.FixedUpdateCall();
    }

    private void OnDisable()
    {
        ability.ForceBreak();
    }
    
}
