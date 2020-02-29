using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    private Team team;
    private float damage;
    public int speed = 230;
    Vector3 dir;
    Shooting shooting;
    Rigidbody2D rb;

    private string soundImpactEnemy = "enemyHit";
    private string soundImpactFriendly = "friendlyHit";

    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = FindObjectOfType<Shooting>();
    }

    public void Setup(Vector3 direc, Team team, float damage)
    {
        this.team = team;
        this.damage = damage;

        dir = direc;
        dir.Normalize();
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

    void Update () {
        rb.velocity = transform.up * Time.deltaTime * speed;
        //Destroy(gameObject, shooting.currentWeapon.distance / rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destructible")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Explosive")
        {
            BarrelExplode be = collision.GetComponent<BarrelExplode>();
            be.health -= damage;
        }

        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            PlayerController playerHit = collision.GetComponent<PlayerController>();
            bool isSameTeam = this.team == playerHit.TeamPlr;
            bool canDamageTarget = !isSameTeam && (playerHit.PlayerNr == 1 || playerHit.PlayerNr == 3);

            if (!isSameTeam && canDamageTarget)
            {
                Debug.Log("Can damage target: " + canDamageTarget);
            }

            if (!isSameTeam)
            {
                if (canDamageTarget)
                {
                    playerHit.health -= damage;
                    playerHit.UpdatePlayerUIHealth();
                }
            }

            //player.UpdateHealth();


            if (isSameTeam)
            {
                AudioManager.instance.Play(soundImpactFriendly);
            }
            else
            {
                if (canDamageTarget)
                {
                    AudioManager.instance.Play(soundImpactEnemy);
                    //GameObject impact = Instantiate(currentWeapon.enemyImpact.gameObject, collision.transform.position, Quaternion.identity) as GameObject;

                    //if (currentWeapon.parentImpact)
                    //{
                    //    impact.transform.parent = playerHit.gameObject.transform;
                    //}
                    //Destroy(impact, currentWeapon.enemyImpactDuration);
                }
                //else
                //{
                //    AudioManager.instance.Play(currentWeapon.enemyImpactSoundNoDamage);
                //}
            }
            Destroy(gameObject);
        }
    }
}
