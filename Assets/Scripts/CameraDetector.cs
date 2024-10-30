using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    // 카메라에 잡히면 빨갛게 물들여줍니다.
    // 인게임 카메라뷰만이 아닌 씬뷰에 잡혀도 작동하니 주의해주세요
    [SerializeField] private Colors hightlightColor;
    MeshRenderer _meshRenderer;

    private float _timeCount = 0f;
    private bool _isHighlighted = false;

    public enum Colors
    {
        Red, Green, Blue
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isHighlighted)
        {
            if (_meshRenderer.isVisible)
            {
                _timeCount += Time.deltaTime;
            }

            if (_timeCount >= 2f)
            {
                _isHighlighted = true;
                switch (hightlightColor)
                {
                    case Colors.Red:
                        _meshRenderer.material.color = Color.red;
                        break;
                    case Colors.Green:
                        _meshRenderer.material.color = Color.green;
                        break;
                    case Colors.Blue:
                        _meshRenderer.material.color = Color.blue;
                        break;
                }
            }
        }

        if (!_meshRenderer.isVisible)
        {
            _timeCount = 0;
            _isHighlighted = false;
            _meshRenderer.material.color = Color.white;
        }
    }
}
