using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : PoolableMono
{
    public bool isEnemy; //적의 총알인가

    [SerializeField]
    private BulletDataSO _bulletData;
    private float _timeToLive; //몇초동안 살아남을것인가
    private Rigidbody2D _rigid;
    private bool _isDead = false;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;
        _rigid.MovePosition(transform.position + transform.right * _bulletData.bulletSpeed * Time.fixedDeltaTime);

        if (_timeToLive > _bulletData.lifeTime)
        {
            _isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            HitObstacle(collision);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }
    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstacle.name) as ImpactScript;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot);
        }
        _isDead = true;
        PoolManager.Instance.Push(this);
    }

    private void HitEnemy(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f,1 << LayerMask.NameToLayer("Enemy"));

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

            PopupText text =PoolManager.Instance.Pop("PopupText") as PopupText;
            text.SetUp(_bulletData.damage.ToString(), hit.point, Color.white);
            
            damageable?.GetHit(_bulletData.damage, this.transform, hit.point, hit.normal);
            ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactEnemyPrefab.name) as ImpactScript;
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            impact.SetPositionAndRotation(hit.point + randomOffset,rot);
            PoolManager.Instance.Push(this);
        }
    }


    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }



    public override void Init()
    {
        _isDead = false;
        _timeToLive = 0;
    }
}
