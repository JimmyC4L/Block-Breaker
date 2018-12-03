using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Paddle : MonoBehaviour
{
    // configuration parameters
    [FormerlySerializedAs("screenWidthInUnits")] [SerializeField]
    private float _screenWidthInUnits = 16f;

    [FormerlySerializedAs("mousePositionInUnitsMin")] [SerializeField]
    private float _mousePositionInUnitsMin = 0f;

    [FormerlySerializedAs("mousePositionInUnitsMax")] [SerializeField]
    private float _mousePositionInUnitsMax = 16f;

    // state
    
    // cached reference
    private GameSession _myGameSession;
    private Ball _myBall;

    private void Start()
    {
        _myGameSession = FindObjectOfType<GameSession>();
        _myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    private void Update()
    {
        ControlPaddleWithMousePosition();
    }

    private void ControlPaddleWithMousePosition()
    {
        var paddlePosition = new Vector2(GetXPosition(), transform.position.y)
        {
            x = Mathf.Clamp(GetXPosition(), _mousePositionInUnitsMin, _mousePositionInUnitsMax)
        };
        transform.position = paddlePosition;
    }

    private float GetXPosition()
    {
        if (_myGameSession.IsAutoPlayEnabled())
        {
            return _myBall.transform.position.x;
        }
        else
        {
            return  Input.mousePosition.x / Screen.width * _screenWidthInUnits;
        }
    }
}