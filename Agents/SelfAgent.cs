namespace BoardgameDesigner;
using Microsoft.SemanticKernel;
/// <summary>
/// All SelfAgent should only have access to function defined locally 
/// </summary>
public class SelfAgent:Agent
{
    public SelfAgent() : base()
    {
        kernel.Plugins.AddFromObject(this);
    }
}