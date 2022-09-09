using System;
using System.IO;
using UnityEngine;

namespace Tools {
    public static class ScreenshotTool {
        private const string DefaultFolder = "ScreenShots";
        private const string FileExtension = ".png";
        
        public static void TakeScreenshot(string directory = DefaultFolder, string name = "") {
            CreateDirectoryIfNeeded(directory);
            
            var fileName = GetFileName(name);
            
            var path = Path.Combine(directory, fileName);
            ScreenCapture.CaptureScreenshot(path);

            ShowDebugInfo(path);
        }
        
        private static void CreateDirectoryIfNeeded(string directory) {
            if (Directory.Exists(directory)) {
                return;
            }

            Directory.CreateDirectory(directory);
        }
        
        private static string GetFileName(string fileName) => string.IsNullOrEmpty(fileName) ? CreateDefaultFileName() : fileName;
        
        private static string CreateDefaultFileName() {
            var fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var fullFileName = $"{fileName}{FileExtension}";

            return fullFileName;
        }
        
        private static void ShowDebugInfo(string path) {
            var fullPath = Path.Combine(Application.dataPath, "../", path);
            fullPath = Path.GetFullPath(fullPath);
            
            Debug.Log($"Take screenshot ({fullPath})");
        }
    }
}