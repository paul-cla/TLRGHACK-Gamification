namespace QuestionEngine.Domain
{
    public class Answer
    {
        public Answer(int id, string text, bool isCorrect)
        {
            Id = id;
            Text = text;
            IsCorrect = isCorrect;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}