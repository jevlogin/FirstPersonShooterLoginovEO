using System;
using UnityEngine;
using UnityEngine.UI;

namespace JevLogin
{
    public class AimUiText : MonoBehaviour
    {
        private Aim[] _aims;
        private Text _text;
        private int _countPoint;

        private void Awake()
        {
            _aims = FindObjectsOfType<Aim>();
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange += UpdatePoint;
            }
        }

        private void OnDisable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange -= UpdatePoint;
            }
        }

        private void UpdatePoint()
        {
            var pointText = "очков";
            ++_countPoint;
            if (_countPoint >= 5)
            {
                pointText = "очков";
            }
            else if (_countPoint == 1)
            {
                pointText = "очко";
            }
            else if (_countPoint < 5)
            {
                pointText = "очка";
            }
            _text.text = $"Вы заработали {_countPoint} {pointText}";
        }
    }
}