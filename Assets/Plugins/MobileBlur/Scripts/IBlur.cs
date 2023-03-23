namespace Plugins.MobileBlur
{
    public interface IBlur
    {
        bool Enabled { get; }
        void Enable();
        void Disable();
    }
}