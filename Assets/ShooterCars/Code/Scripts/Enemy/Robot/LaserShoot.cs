using UnityEngine;

using ShooterCar.Enemy;

public class LaserShoot : BossShoot
{
    [SerializeField] private LineRenderer laserLine;

    private bool fire;

    protected override void Shoot()
    {
        if(IsWeaponDestroyed())
        {
            laserLine.gameObject.SetActive(false);
            return;
        }

        if (fire && m_FireInterval <= 0)
            m_Weapon.Shoot(m_Player.position, gameObject.tag, laserLine);

        if (m_FireInterval >= 0)
        {
            m_FireInterval -= Time.deltaTime;
            return;
        }

        fire = !fire;
        laserLine.enabled = fire;
        m_FireInterval = m_Cooldown;
    }
}