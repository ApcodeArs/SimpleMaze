using Tools;
using UnityEditor;

namespace Helpers.Editor {
    public static class ScreenshotMenuItem {
        [MenuItem("Custom Tools/Take Screenshot")]
        public static void TakeScreenshot() {
            ScreenshotTool.TakeScreenshot();
        }
    }
}