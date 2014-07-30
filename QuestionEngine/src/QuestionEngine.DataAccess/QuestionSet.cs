using System.Collections.Generic;
using QuestionEngine.Domain;

namespace QuestionEngine.DataAccess
{
    public class QuestionSet
    {
        public QuestionSet()
        {
            Questions = QuestionFactory.GenerateQuestions();
        }
        public List<Question> Questions { get; set; }
    }

    public static class QuestionFactory
    {
        public static List<Question> GenerateQuestions()
        {
            var questions = new List<Question>();
            var questionCounter = 1;
            var answerCounter = 1;

            questions.Add(new Question(questionCounter++,
                "A colleague needs to finish an urgent project before they leave",
                new List<Answer> {
                    new Answer(answerCounter++, "Suggest they email their manager to say the project won’t be completed on time", false),
                    new Answer(answerCounter++, "Stay and help complete the task together", true),
                    new Answer(answerCounter++, "Leave it to them, they can always catch a later train", false),
                    new Answer(answerCounter++, "Suggest they put the information on a USB stick and complete the work on the journey", false)
                },
                new List<Media>
                    {
                        new Media("image", "Peninsula.jpg"),
                        new Media("video", "https://www.youtube.com/watch?v=AlQvFI-ezoc") 
                    },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "The train to Euston has been cancelled with no notification of the next train",
                new List<Answer> {
                    new Answer(answerCounter++, "Ask a guard for their advice on the quickest route", true),
                    new Answer(answerCounter++, "Hang around and see if the board gets updated with new times", false),
                    new Answer(answerCounter++, "Rearrange your flights for the next day", false),
                    new Answer(answerCounter++, "There’s a train on the next platform – jump on it and take it from there", false)
                            },
                new List<Media>
                    {
                        new Media("image", "Manchester.jpg"),
                        new Media("video", "https://www.youtube.com/watch?v=AU-PA8Kqov4") 
                    },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "You bump into a couple that have booked to go to Singapore but haven’t got a hotel yet",
                new List<Answer> {
                                new Answer(answerCounter++, "Tell them where you’re staying and suggest they go there", false),
                                new Answer(answerCounter++, "I’m too busy to help – I need to check in my bags", false),
                                new Answer(answerCounter++, "Suggest they search on the internet", false),
                                new Answer(answerCounter++, "Show them how to download the AsiaRooms App", true)
                                        },
                new List<Media>
                                {
                                    new Media("image", "London.jpg"),
                                    new Media("video", "https://www.youtube.com/watch?v=EOttM05W3b4") 
                                },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "You bump into a couple that have booked to go to Singapore but haven’t got a hotel yet",
                new List<Answer> {
                                            new Answer(answerCounter++, "Tell them where you’re staying and suggest they go there", false),
                                            new Answer(answerCounter++, "I’m too busy to help – I need to check in my bags", false),
                                            new Answer(answerCounter++, "Suggest they search on the internet", false),
                                            new Answer(answerCounter++, "Show them how to download the AsiaRooms App", true)
                                                    },
                new List<Media>
                                            {
                                                new Media("image", "London.jpg"),
                                                new Media("video", "https://www.youtube.com/watch?v=EOttM05W3b4") 
                                            },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "You are only in Singapore for 1 day and you and your friend have a long list of things to see but might not have enough time. Do you?",
                new List<Answer> {
                                                        new Answer(answerCounter++, "Cram them all in a day. At least you can tell people back home that you’ve done everything", false),
                                                        new Answer(answerCounter++, "Write a list of everything you want to see and then pick your top ones to make the most of", true),
                                                        new Answer(answerCounter++, "Do what the concierge says", false),
                                                        new Answer(answerCounter++, "Can’t decide so stay at the hotel. You’ll just have to go to Singapore another time", false)
                                                                },
                new List<Media>
                                                        {
                                                            new Media("image", "Singapore.jpg"),
                                                        },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "The first thing on your list was the Botanic Gardens.  You spot 500SDG lying on the floor",
                new List<Answer> {
                                                                    new Answer(answerCounter++, "Keep it for yourself, but don’t tell your friend.  Finders Keepers!", false),
                                                                    new Answer(answerCounter++, "Keep it and share it with your friend – what a great piece of luck", false),
                                                                    new Answer(answerCounter++, "Hand it to the nearest member of staff at the gardens", true),
                                                                    new Answer(answerCounter++, "Leave it, I’m sure they’ll come back to look for it", false)
                                                                            },
                new List<Media>
                                                                    {
                                                                        new Media("image", "Singapore.jpg"),
                                                                    },
                questionCounter));

            questions.Add(new Question(questionCounter++,
                "You’ve been financially rewarded for your honesty and have decided to fly to Bangkok!  On Khao San Road you’re propositioned by a man who’d like you to join him for a “late night show”.  You are reluctant but your friend is eager to get involved",
                new List<Answer> {
                    new Answer(answerCounter++, "Let your friend go on their own", false),
                    new Answer(answerCounter++, "Join her even though you have your suspicions – what’s the worst that can happen", false),
                    new Answer(answerCounter++, "Convince your friend that you will have a better night if you do something else", true),
                    new Answer(answerCounter++, "Flip a coin and leave it to fate", false)
                    },
                new List<Media>
                    {
                        new Media("image", "Bangkok.jpg"),
                    },
                -1));


            return questions;
        }
    }
}