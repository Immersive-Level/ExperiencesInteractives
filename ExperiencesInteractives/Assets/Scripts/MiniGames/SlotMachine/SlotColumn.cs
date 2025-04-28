using System.Collections;
using UnityEngine;

namespace SlotMachine.Core
{
    public class SlotColumn : MonoBehaviour
    {
        [SerializeField] private float _spinSpeed = 10f;
        [SerializeField] private float _stopDelay = 0.5f;
        [SerializeField] private SlotSymbol[] _symbols;

        private bool _isSpinning;
        private int _currentSymbolIndex;
        private Coroutine _spinCoroutine;

        public int CurrentSymbolId => _symbols[_currentSymbolIndex].SymbolId;

        // Inicializa la columna ocultando todos los símbolos excepto el primero
        public void Initialize()
        {
            for (int i = 0; i < _symbols.Length; i++)
            {
                _symbols[i].Hide();
            }
            _symbols[0].Show();
            _currentSymbolIndex = 0;
        }

        // Comienza a girar la columna
        public void Spin()
        {
            if (_isSpinning) return;

            _isSpinning = true;
            _spinCoroutine = StartCoroutine(SpinRoutine());
        }

        // Detiene la columna en un símbolo específico
        public void Stop(int targetSymbolId = -1)
        {
            if (!_isSpinning) return;

            StopCoroutine(_spinCoroutine);
            StartCoroutine(StopRoutine(targetSymbolId));
        }

        // Corrutina para el giro
        private IEnumerator SpinRoutine()
        {
            while (true)
            {
                HideCurrentSymbol();
                MoveToNextSymbol();
                ShowCurrentSymbol();

                yield return new WaitForSeconds(1f / _spinSpeed);
            }
        }

        // Corrutina para detenerse
        private IEnumerator StopRoutine(int targetSymbolId)
        {
            // Si no se especifica símbolo, elegir uno aleatorio
            if (targetSymbolId < 0)
            {
                targetSymbolId = _symbols[Random.Range(0, _symbols.Length)].SymbolId;
            }

            // Continuar girando hasta llegar al símbolo objetivo
            while (_symbols[_currentSymbolIndex].SymbolId != targetSymbolId)
            {
                HideCurrentSymbol();
                MoveToNextSymbol();
                ShowCurrentSymbol();

                yield return new WaitForSeconds(1f / _spinSpeed);
            }

            _isSpinning = false;
            yield return new WaitForSeconds(_stopDelay);
        }

        private void HideCurrentSymbol()
        {
            _symbols[_currentSymbolIndex].Hide();
        }

        private void ShowCurrentSymbol()
        {
            _symbols[_currentSymbolIndex].Show();
        }

        private void MoveToNextSymbol()
        {
            _currentSymbolIndex = (_currentSymbolIndex + 1) % _symbols.Length;
        }
    }
}
