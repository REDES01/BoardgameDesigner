namespace BoardgameDesigner;
using System.ComponentModel;
using Microsoft.SemanticKernel;

public static class FileSystem
{
    public const string AbsoluteFolderPath = @"C:\Users\47659\Desktop\AI\BoardgameDesigner";
    [KernelFunction("save_file")]
    [Description("Save file to storage.")]

    public static void SaveFile(string path,object content)
    {
        
    }
    
    [KernelFunction("read_file")]
    [Description("Read file from storage.")]

    public static void ReadFile(string filename)
    {
        
    }
}