using System;
using System.Runtime.CompilerServices;
using UnityEngine;
public class FireRateButton : MonoBehaviour
{
    [SerializeField] private playerShoot playerShoot;
    public void ButtonPressed()
    {
        playerShoot.increaseShotsPerSecondMaximum();
    }
}
