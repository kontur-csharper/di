using System;
using System.IO;

namespace TagCloud
{
    public class PathCreater : IPathCreater
    {
        public string GetCurrentPath()
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var index = workingDirectory.IndexOf("TagCloud");
            return workingDirectory.Substring(0, index);
        }

        public string GetNewPngPath()
        {
            return GetCurrentPath() + "MyPng" +  DateTime.Now.Millisecond + ".png";
        }

        public string GetPathToFile(string fileName)
        {
            return GetCurrentPath() + fileName;
        }
    }
}