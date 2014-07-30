
var questions = [
{
	questionText: 'I want to work for LateRooms because...',
	questionImage: '2.jpg',
	questionAnswers: [
		{
			isCorrect: true,
			text: 'AmazeBalls!'
		},
		{
			isCorrect: false,
			text: 'Potato'
		},
		{
			isCorrect: false,
			text: 'Benefits'
		}
	]
},
{
	questionText: 'Which of these is a LateRooms brand...',
	questionImage: '3.jpg',
	questionAnswers: [
		{
			isCorrect: false,
			text: 'AmazeBalls!'
		},
		{
			isCorrect: true,
			text: 'MalaPronta',
		},
		{
			isCorrect: false,
			text: 'Potato'
		},
		{
			isCorrect: false,
			text: 'Benefits'
		}
	]
}
];

var AlertWrongAnswerHandler = function() {

	var handleWrongAnswer = function() {

		alert('WRONG!');
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var StaticQuestionProvider = function() {

	var getQuestion = function(questionIndex) {

		return questions[questionIndex];
	}

	var testAnswer = function(questionIndex, answerIndex) {
		return questions[questionIndex].questionAnswers[answerIndex].isCorrect;
	}

	var endOfQuiz = function(index) {
		return (index == questions.length);
	}

	return {
		GetQuestion: getQuestion, 
		TestAnswer: testAnswer,
		EndOfQuiz: endOfQuiz
	}
}

var Quiz = function() {

	var startQuiz;
	var imageContainer;
	var questionHeader;
	var questionText;
	var answerContainer;
	var questionProvider;
	var wrongAnswerHandler;
	var currentQuestionIndex;
	var dataAnswerIndex = 'data-answer-index';

	var init = function(options) {

		startQuiz = $(options.startQuizSelector);
		imageContainer = $(options.imageContainerSelector);
		questionHeader = $(options.questionHeaderSelector);
		questionText = $(options.questionTextSelector);
		answerContainer = $(options.questionAnswerContainerSelector);
		questionProvider = options.questionProvider();
		wrongAnswerHandler = options.wrongAnswerHandler();
		currentQuestionIndex = 0;	

		startQuiz.on('click', answerClicked);
	}

	var answerClicked = function() {

		var answerIndex = $(this).attr(dataAnswerIndex);

		if(answerIndex != undefined) {

			var answerIsCorrect = questionProvider.TestAnswer(currentQuestionIndex-1, answerIndex);
			if(!answerIsCorrect) {
				wrongAnswerHandler.HandleWrongAnswer();
				return;
			}
		}

		if(questionProvider.EndOfQuiz(currentQuestionIndex)) {
			quizCompleted();
			return;
		}

		var question = questionProvider.GetQuestion(currentQuestionIndex);

		prepareNextQuestion(question);
		
		nextQuestion();
	}	

	var prepareNextQuestion = function(question) {

		questionHeader.text('Question ' + (currentQuestionIndex+1));

		var nextImage = 'url("img/' + question.questionImage + '")';

		imageContainer.css('background-image', nextImage);

		questionText.text(question.questionText);

		setQuestions(question);
	}

	var setQuestions = function(question) {

		answerContainer.html('');

		for(var i in question.questionAnswers) {
			
			var answer = question.questionAnswers[i];

			createAnswerButton(answerContainer, i, answer);
		}
	}

	var createAnswerButton = function(answerContainer, answerIndex, answer) {

		var questionButton = $('<a href="#" class="btn btn-lg btn-default start-quiz ' + answer.isCorrect + '">' + answer.text + '</a>');

		answerContainer.append(questionButton);
		answerContainer.append('&nbsp;');

		questionButton.attr(dataAnswerIndex, answerIndex);
		questionButton.on('click', answerClicked);
	}

	var quizCompleted = function() {

		questionHeader.text('CONGRATULATIONS!!!');

		answerContainer.html('');

		questionText.text('Well done! You are now a LateRooms employee!');
	}

	var nextQuestion = function() {
		currentQuestionIndex++;
	}

	return {
		Init: init
	}

}();

$(function () {
	
	Quiz.Init({
		startQuizSelector: '.start-quiz',
		imageContainerSelector: 'body',
		questionHeaderSelector: '.question-header',
		questionTextSelector: '.question-text',
		questionAnswerContainerSelector: '.question-answer-container',
		questionProvider: StaticQuestionProvider,
		wrongAnswerHandler: AlertWrongAnswerHandler
	});

});
