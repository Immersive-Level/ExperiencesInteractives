using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlotMachine.Core
{
    public class SlotSymbol : MonoBehaviour
    {
        [SerializeField] private int _symbolId;
        [SerializeField] private SpriteRenderer _symbolRenderer;

        public int SymbolId => _symbolId;

        // Configura el símbolo con un ID y un sprite específico
        public void Setup(int id, Sprite symbolSprite)
        {
            _symbolId = id;
            _symbolRenderer.sprite = symbolSprite;
        }

        // Hace visible el símbolo
        public void Show()
        {
            gameObject.SetActive(true);
        }

        // Oculta el símbolo
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        void Start()
        {
            if (_symbolRenderer == null)
            {
                _symbolRenderer = GetComponent<SpriteRenderer>();
                Debug.Log(_symbolRenderer != null ? "Renderer encontrado" : "ERROR: No hay SpriteRenderer");
            }
        }
    }
}
