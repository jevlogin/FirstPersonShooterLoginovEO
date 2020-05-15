using System;
using UnityEngine;
using UnityEngine.AI;


namespace JevLogin
{
    public class Bot : BaseObjectScene, IExecute
    {
        #region Fields

        public Weapon Weapon;   //todo с разным оружием
        public Vision Vision;
        private StateBot _stateBot;
        private Vector3 _point;
        private ITimeRemaining _timeRemaining;

        private float _waytTime = 3;
        private float _stoppingDistance = 2.0f;

        public float HealthPoint = 100;

        public event Action<Bot> OnDieChange;

        #endregion


        #region Properties

        public Transform Target { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        private StateBot StateBot
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case StateBot.None:
                        Color = Color.white;
                        break;
                    case StateBot.Patrol:
                        Color = Color.green;
                        break;
                    case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
                    case StateBot.Detected:
                        Color = Color.yellow;
                        break;
                    case StateBot.Died:
                        Color = Color.yellow;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }
        #endregion


        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            _timeRemaining = new TimeRemaining(ResetStateBot, _waytTime);
        }



        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange += SetDamage;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange += SetDamage;
            }
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange -= SetDamage;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange -= SetDamage;
            }
        }

        #region IExecute

        public void Execute()
        {
            if (StateBot == StateBot.Died) return;

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = 0;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                StateBot = StateBot.Inspection;
                                _timeRemaining.AddTimeRemaining();
                            }
                        }
                    }
                }
                if (Vision.VisionM(transform, Target))
                {
                    StateBot = StateBot.Detected;
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    Agent.stoppingDistance = _stoppingDistance;
                }
                if (Vision.VisionM(transform, Target))
                {
                    Weapon.Fire();
                }
                else
                {
                    MovePoint(Target.position);
                }
                //todo потеря персонажа
            }
        }

        #endregion

        #region Methods

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        private void SetDamage(InfoCollision info)
        {
            //todo реакция на попадание
            if (HealthPoint > 0)
            {
                HealthPoint -= info.Damage;
            }

            if (HealthPoint <= 0)
            {
                StateBot = StateBot.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRigidBodyChild = child.GetComponent<Rigidbody>();
                    if (!tempRigidBodyChild)
                    {
                        tempRigidBodyChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                    tempRigidBodyChild.AddForce(info.Direction * UnityEngine.Random.Range(10, 300));

                    Destroy(child.gameObject, 5);
                }

                OnDieChange?.Invoke(this);
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        #endregion
    }
}
