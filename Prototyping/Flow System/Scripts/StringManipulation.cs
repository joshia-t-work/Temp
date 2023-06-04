namespace DKP.SaveSystem.Data
{
    public static class StringManipulation
    {
        public static string ReplaceFirst(string originalString, string searchString, string replaceString)
        {
            int startIndex = originalString.IndexOf(searchString);

            if (startIndex != -1)
            {
                return originalString.Substring(0, startIndex) + replaceString + originalString.Substring(startIndex + searchString.Length);
            }
            else
            {
                return originalString;
            }
        }
    }
}
