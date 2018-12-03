using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
	// I can not find out why there is already a block on the scene when there is not
	[FormerlySerializedAs("_blocksCount")] [SerializeField] private int _blocksCount;

	private SceneLoader _sceneLoader;

	private void Start()
	{
		_sceneLoader = FindObjectOfType<SceneLoader>();
	}

	public void IncreaseBlocksCount()
	{
		_blocksCount++;
	}

	public void BlockDestroyed()
	{
		_blocksCount--;
		if (_blocksCount <= 0)
		{
			_sceneLoader.LoadNextScene();
		}
	}
}
