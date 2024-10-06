public interface IActivatable : IInteractable
{
    public string ActivationText {  get; }
    void Activate();
}
