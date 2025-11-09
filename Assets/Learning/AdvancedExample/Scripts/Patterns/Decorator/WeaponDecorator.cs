using System.Collections.Generic;

public abstract class WeaponDecorator : IWeapon {
    protected WeaponDecorator(IWeapon weapon) { }
    public string ID { get; set; }
    public List<IAttachment> Attachments { get; } = new ();
    public void AssembleWeapon(string id) { }
}

public sealed class ScopeDecorator : WeaponDecorator {
    public ScopeDecorator(IWeapon weapon) : base(weapon) {
        Attachments.AddRange(weapon.Attachments);
        Scope scope = new ();
        scope.AssembleAttachment("Sniper Scope 2");
        Attachments.Add(scope);
    }
}

public sealed class SilencerDecorator : WeaponDecorator {
    public SilencerDecorator(IWeapon weapon) : base(weapon) {
        Attachments.AddRange(weapon.Attachments);
        Silencer silencer = new ();
        silencer.AssembleAttachment("SMG Silencer 2");
        Attachments.Add(silencer);
    }
}
