using System;
using UnityEngine;

public class WeaponAssembler : MonoBehaviour {

    private void Start() {

        SmgFactory smgFactory = new ();
        IWeapon smg = smgFactory.CreateWeapon();
        var silencer = new Silencer();
        silencer.AssembleAttachment("Silencer 1");
        smg.Attachments.Add(silencer);

        // using the decorator pattern
        SniperFactory sniperFactory = new ();
        IWeapon sniper = sniperFactory.CreateWeapon();
        var decoratedSniper = new ScopeDecorator(new SilencerDecorator(sniper));
        //Debug.Log(decoratedSniper.Attachments.Count);
    }
}
