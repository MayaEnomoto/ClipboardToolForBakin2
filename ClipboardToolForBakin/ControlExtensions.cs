using System.Reflection;

namespace ClipboardToolForBakin2
{
    public static class ControlExtensions
    {
        public static void EnableDoubleBuffering(this Control control)
        {
            // Get a reference to the "DoubleBuffered" property by its name
            var propertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(control, true, null);
            }
        }
    }
}
