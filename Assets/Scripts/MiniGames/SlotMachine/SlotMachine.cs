using UnityEngine;
using System;
using System.Collections;

namespace SlotMachine.Core
{
    public class SlotMachine : MonoBehaviour, ISlotMachine
    {
        [SerializeField] private SlotColumn[] _columns;
        [SerializeField] private int _initialCredits = 100;
        [SerializeField] private int _spinCost = 10;
        [SerializeField] private int _winReward = 50;

        private int _currentCredits;
        private bool _isSpinning;
        private Action<bool, int> _onSpinComplete;

        public int CurrentCredits => _currentCredits;

        // Inicializa la máquina
        public void Initialize()
        {
            _currentCredits = _initialCredits;

            foreach (var column in _columns)
            {
                column.Initialize();
            }
        }

        // Gira las columnas
        public void Spin()
        {
            if (_isSpinning || _currentCredits < _spinCost) return;

            _currentCredits -= _spinCost;
            _isSpinning = true;

            foreach (var column in _columns)
            {
                column.Spin();
            }

            // Detener después de un tiempo aleatorio
            float stopDelay = UnityEngine.Random.Range(2f, 4f);
            Invoke("Stop", stopDelay);
        }

        // Detiene las columnas
        public void Stop()
        {
            if (!_isSpinning) return;

            // Para cada columna con un pequeño retraso entre ellas para efecto visual
            for (int i = 0; i < _columns.Length; i++)
            {
                float delay = i * 0.3f;
                StartCoroutine(StopColumnWithDelay(_columns[i], delay));
            }

            // Verificar si hay ganador después de que todas se detengan
            Invoke("CheckForWin", _columns.Length * 0.3f + 0.5f);
        }

        // Verifica si hay combinación ganadora
        public bool CheckWin()
        {
            if (_columns.Length == 0) return false;

            int firstSymbol = _columns[0].CurrentSymbolId;

            foreach (var column in _columns)
            {
                if (column.CurrentSymbolId != firstSymbol)
                    return false;
            }

            return true;
        }

        // Añade créditos
        public void AddCredits(int amount)
        {
            _currentCredits += amount;
        }

        private IEnumerator StopColumnWithDelay(SlotColumn column, float delay)
        {
            yield return new WaitForSeconds(delay);
            column.Stop();
        }

        private void CheckForWin()
        {
            _isSpinning = false;

            bool isWin = CheckWin();
            if (isWin)
            {
                AddCredits(_winReward);
                _onSpinComplete?.Invoke(true, _winReward);
            }
            else
            {
                _onSpinComplete?.Invoke(false, 0);
            }
        }

        // Registra callback para cuando termine el giro
        public void RegisterSpinCompleteCallback(Action<bool, int> callback)
        {
            _onSpinComplete += callback;
        }

        // Elimina callback
        public void UnregisterSpinCompleteCallback(Action<bool, int> callback)
        {
            _onSpinComplete -= callback;
        }
    }
}