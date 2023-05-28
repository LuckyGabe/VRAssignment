using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public NavMeshAgent agent;
    [SerializeField]
    private GameObject targetTower;
    public bool bCanMove = false;
    private bool bExplode = true;
    [SerializeField]
    private float rangeToExplode = 20f;
    private float time = 0f;
    [SerializeField]
    private float explosionRadius = 20f;
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    public float movementSpeed = 5f;
    [SerializeField]
    private LayerMask turretLayerMask;
    [SerializeField]
    private ParticleSystem shield, explosion;
    private Target targetScript;
    private Vector3 objectPoolPos;
    private Animator animator;
    private GameManager gameManager;
    private Collider collider;
    private void Awake()
    {
        targetTower = GameObject.Find("Tower");
        targetScript = GetComponent<Target>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

        objectPoolPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedTime;
        if (time > 1f)
        {
            time = 0f;
            if (TowerInRange() && !targetScript.bIsDead)
            {
                bCanMove = false;
                Explode();
            }
        }

        if (targetScript.bIsDead && bExplode) { Explode(); }
        if (!bCanMove) { if (agent.isOnNavMesh) { agent.ResetPath(); } return; }
        if (agent.isOnNavMesh && bCanMove)
        {
            MoveToTower();
            agent.speed = movementSpeed;
        }

    }

    public void MoveToTower()
    {
        agent.SetDestination(targetTower.transform.position);
    }

    private bool TowerInRange()
    {
        return Vector3.Distance(targetTower.transform.position, transform.position) <= rangeToExplode;
    }

    private void Explode()
    {

        Collider[] hitColliders = new Collider[10];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hitColliders, turretLayerMask);
        bExplode = false;
        bCanMove = false;
        agent.isStopped = true;
        collider.enabled = false;
        for (int i = 0; i < numColliders; i++)
        {
            TurretHealth turretHealthScript = hitColliders[i].GetComponent<TurretHealth>();

            if (turretHealthScript != null)
            {
                turretHealthScript.DealDamage(damage);
            }
        }
        explosion.Play();
        targetScript.bIsDead = true;
        animator.SetTrigger("Die");
        gameManager.audioManager.Play("EnemyExplosion");
        gameManager.enemiesToClear--;
        agent.enabled = false;
        StartCoroutine(BackToPool(0.7f));

    }

    private IEnumerator BackToPool(float afterTime)
    {
        yield return new WaitForSeconds(afterTime);
        transform.position = objectPoolPos;
        targetScript.bIsDead = false;
        shield.Play();
        targetScript.fHealth = targetScript.iMaxHealth;
        animator.SetTrigger("Walk");
        bExplode = true;
        collider.enabled = true;
    }

    public void MoveEnemy(Vector3 newPosition)
    {
        agent.Warp(newPosition);
    }
}
