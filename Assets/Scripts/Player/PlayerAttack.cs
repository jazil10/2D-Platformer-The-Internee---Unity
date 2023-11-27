using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool hasPunched;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            // Right mouse button: Perform punch animation
            Punch();
        }
        else if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            // Left mouse button: Launch fireball
            LaunchFireball();
        }

        cooldownTimer += Time.deltaTime;

        if (hasPunched && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            hasPunched = false;
        }
    }

    private void Punch()
    {
        // Perform punch animation
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // Call the TriggerDamage method from the punch animation event
        TriggerDamage();
    }

    private void LaunchFireball()
    {
        // Launch the fireball as before
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    // This method is called from the punch animation event
    public void TriggerDamage()
    {
        hasPunched = true;

        // Debug log to check if the method is called
        Debug.Log("TriggerDamage called");

        // Detect enemy collision and deal damage
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, 1.0f); // Adjust the radius as needed

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                // Debug log to check if an enemy is detected
                Debug.Log("Enemy detected: " + enemyCollider.name);

                Health enemyHealth = enemyCollider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                }
            }
        }
    }

}
