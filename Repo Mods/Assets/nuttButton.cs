using System;
using UnityEngine;
using Photon.Pun;

public class NutButtonTrap : Trap
{
    [Header("Nut Button Settings")]
    public Sound nutSound;

    private bool hasPlayedSound = false;

    protected override void Start()
    {
        base.Start();
        // No flicker/light setup needed
    }

    protected override void Update()
    {
        base.Update();

        // Only the local player should trigger the sound
        if (!isLocal) return;

        if (!hasPlayedSound && physGrabObject != null && physGrabObject.grabbed)
        {
            hasPlayedSound = true;

            if (PhotonNetwork.InRoom)
            {
                photonView.RPC("PlayNutSound", RpcTarget.All);
            }
            else
            {
                PlayNutSound();
            }
        }

        // Optional reset if dropped (comment out if you don’t want reset)
        if (hasPlayedSound && physGrabObject != null && !physGrabObject.grabbed)
        {
            hasPlayedSound = false;
        }
    }

    [PunRPC]
    public void PlayNutSound()
    {
        if (nutSound != null)
        {
            nutSound.Play(transform.position);
        }
    }
}
