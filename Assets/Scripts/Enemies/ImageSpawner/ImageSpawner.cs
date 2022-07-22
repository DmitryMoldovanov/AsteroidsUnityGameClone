using System;
using UnityEngine;

public class ImageSpawner : MonoBehaviour
{
    [SerializeField] private StateImage _alertImage;
    [SerializeField] private StateImage _readyToAttackImage;
    [SerializeField] private StateImage _reloadingImage;

    public void SpawnAlertImage(Color color, Transform parent)
    {
        StateImage image = Instantiate(_alertImage, parent.position, Quaternion.identity);
        image.SetColor(color);
    }

    public void SpawnReadyToAttackImage(Color color, Transform parent)
    {
        StateImage image = Instantiate(_readyToAttackImage, parent.position, Quaternion.identity);
        image.SetColor(color);
    }

    public void SpawnReloadingImage(Color color, Transform parent)
    {
        StateImage image = Instantiate(_reloadingImage, parent.position, Quaternion.identity);
        image.SetColor(color);
    }
}