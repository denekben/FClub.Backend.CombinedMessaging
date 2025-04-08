namespace AccessControl.Shared.Logging
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SkipLoggingAttribute : Attribute
    {
    }
}
