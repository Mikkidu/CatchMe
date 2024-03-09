using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace AlexDev.CatchMe
{
    public class UnitDatabase : MonoBehaviour
    {
        public TextMeshProUGUI[] debugTexts;
        public static UnitDataElement[] unitsBase { get; private set; }


        private int _unitsCount;
        private int _currentindex = 0;
        private float _distanceDelay = 2f;

        public static event Action<Transform> OnTargedChanged;

        private void Start()
        {
            _unitsCount = transform.childCount;
            unitsBase = new UnitDataElement[_unitsCount];

            for (int i = 0; i < _unitsCount; i++)
            {
                var unitTransform = transform.GetChild(i).transform;
                unitsBase[i] = new UnitDataElement(unitTransform, unitTransform.GetComponent<UnitController>() != null);
            }
        }


        void Update()
        {
            float distance = GetDistanceTo(unitsBase[_currentindex].unitTransform.position);
            if (distance < 100f)
            {
                unitsBase[_currentindex].SetDistance(distance);
            }
            else
            {
                unitsBase[_currentindex].SetDistance(100f);
            }
            Debug.Log("Update index: " + _currentindex);
            UpdateUI();
            UpdateUnitsArray(_currentindex);
            if (++_currentindex >= _unitsCount)
            {
                _currentindex = 1;
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < unitsBase.Length; i++)
            {
                debugTexts[i]?.SetText($"{unitsBase[i].unitTransform.gameObject.name} distance: {unitsBase[i].distanceToTagger : 0.0}");
            }
        }

        private float GetDistanceTo(Vector3 targetPosition)
        {
            float distance = 0;
            Vector3 taggerPosition = unitsBase[0].unitTransform.position;
            NavMeshPath pathToTarget = new NavMeshPath();
            if (NavMesh.CalculatePath(taggerPosition, targetPosition, NavMesh.AllAreas, pathToTarget))
            {
                Vector3[] corners = pathToTarget.corners;
                distance = Vector3.Distance(taggerPosition, corners[0]);
                for (int i = 2; i < corners.Length; i++)
                {
                    distance += Vector3.Distance(corners[i - 1], corners[i]);
                }
            }
            else
            {
                distance = Mathf.Infinity;
            }
            return distance;
        }

        public static Transform GetTarget()
        {
            if (unitsBase != null && unitsBase.Length > 1)
            {
                return unitsBase[1].unitTransform;
            }
            return null;
        }

        public static void SetTagger(Transform taggerTransform)
        {
            for (int i = 1; i < unitsBase.Length; i++)
            {
                if (unitsBase[i].unitTransform == taggerTransform)
                {
                    ChangeUnitsInBase(0, i);
                    return;
                }
            }
        }

        private void UpdateUnitsArray(int unitToCheckIndex)
        {
            if (unitToCheckIndex > 1)
            {
                if (unitsBase[unitToCheckIndex].distanceToTagger + _distanceDelay < unitsBase[unitToCheckIndex - 1].distanceToTagger)
                {
                    ChangeUnitsInBase(unitToCheckIndex, unitToCheckIndex - 1);
                    if (unitToCheckIndex == 3) InvokeNewTarget();
                    return;
                }
            }
            if (unitToCheckIndex < unitsBase.Length - 1)
            {
                if (unitsBase[unitToCheckIndex].distanceToTagger - _distanceDelay > unitsBase[unitToCheckIndex + 1].distanceToTagger)
                {
                    ChangeUnitsInBase(unitToCheckIndex, unitToCheckIndex + 1);
                    if (unitToCheckIndex == 2) InvokeNewTarget();
                    _currentindex--;
                    return;
                }
            }
        }

        private static void ChangeUnitsInBase(int firstIndex, int secondIndex)
        {
            UnitDataElement tempUnit = unitsBase[firstIndex];
            unitsBase[firstIndex] = unitsBase[secondIndex];
            unitsBase[secondIndex] = tempUnit;
        }

        private void InvokeNewTarget()
        {
            OnTargedChanged?.Invoke(unitsBase[2].unitTransform);
        }

        //public static bool GetPath(NavMeshPath path, Vector3 fromPos, Vector3 toPos, int passableMask)
        //{
        //    path.ClearCorners();

        //    if (NavMesh.CalculatePath(fromPos, toPos, passableMask, path) == false)
        //        return false;

        //    return true;
        //}

        //public static float GetPathLength(NavMeshPath path)
        //{
        //    float lng = 0.0f;

        //    if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.SafeLength() > 1))
        //    {
        //        for (int i = 1; i < path.corners.Length; ++i)
        //        {
        //            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        //        }
        //    }

        //    return lng;
        //}
    }

    public class UnitDataElement
    {
        public Transform unitTransform { get; private set; }
        public bool isBot { get; private set; }
        public float distanceToTagger { get; private set; }

        public event Action<float> OnDistanceChangEvent;


        public void SetDistance(float distance)
        {
            if (distance > 0)
            {
                distanceToTagger = distance;
                OnDistanceChangEvent?.Invoke(distance);
            }
        }

        public UnitDataElement(Transform unitTransform, bool isBot)
        {
            this.unitTransform = unitTransform;
            this.isBot = isBot;
            distanceToTagger = 0f;
            if (unitTransform.TryGetComponent<UnitController>(out var controller))
                OnDistanceChangEvent += controller.UpdateDistance;

            Debug.Log("Created unit " + this.unitTransform.gameObject.name + " is bot = " + isBot);
        }
    }
}
