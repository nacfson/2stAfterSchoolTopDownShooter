using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono{
    public Transform target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;

    public UnityEvent OnAttackButtonPress= null;

    public UnityEvent OnResetPool = null;

    public Transform basePosition; //거리 측정을 몬스터의 바닥에서 함
    public AIState currentState;

    private EnemyRenderer _enemyRenderer;

    [SerializeField]
    private bool _isActive = false;
    public bool IsActive => _isActive;

    private void Awake() {
        _enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();
    }

    private void Start(){
        target = GameManager.Instance.PlayerTransform;
        currentState.SetUp(transform);
    }

    public void ChangeState(AIState nextState){
        currentState = nextState;
        currentState?.SetUp(transform);
    }

    public void Update(){
        if(_isActive == false){ //업데이트 수행 안 함.
            return;
        }
        if(target == null){
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else{
            currentState.UpdateState();
        }
    }
    public void ShowEnemy(){
        _isActive = false;
        _enemyRenderer.ShowProgress(1f, () => _isActive = true);
    }

    public void Move(Vector2 moveDirection,Vector3 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public override void Init(){
        _isActive = false;
        _enemyRenderer.Reset();
        OnResetPool?.Invoke();
    }

    public void Attack(){
        //공격 수행
        OnAttackButtonPress?.Invoke();
    }

    public void Dead(){
        _isActive = false;
    }

    public void GoToPool(){
        PoolManager.Instance.Push(this);
    }
}
