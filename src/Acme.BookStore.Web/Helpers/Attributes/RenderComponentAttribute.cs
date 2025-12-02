using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RenderComponentAttribute : Attribute
{
    public string ComponentName { get; }
    public string[] ParamNames { get; }
    public string[] ModelProperties { get; }

    public RenderComponentAttribute(string componentName, string[] paramNames, string[] modelProperties)
    {
        ComponentName = componentName;
        ParamNames = paramNames;
        ModelProperties = modelProperties;
    }
}
