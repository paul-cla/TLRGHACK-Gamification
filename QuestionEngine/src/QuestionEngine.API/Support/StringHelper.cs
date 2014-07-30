namespace QuestionEngine.API.Support
{
    public static class StringHelper
    {
        public static string DecodeAmpersands(string input)
        {
            return input.Replace("~~AMP~~", "&");
        }
    }
}
